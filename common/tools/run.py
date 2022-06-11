# !/usr/bin/python
# encoding=utf-8
import sys
import os
import threading
import time
import shutil
import traceback
from queue import Queue
from logger import Logger
from myTable import MyTable, MyTableTool
from config import Config
more_path = ['./../jpy']
for x in more_path:
    p = os.path.join(os.path.dirname(sys.argv[0]), x)
    if os.path.exists(p):
        sys.path.append(p)
from jutil import JUtil

config = Config()
logger = ''

DEBUG_MODE = False # Debug模式
table_list = [] # 表格列表
que_table_path = Queue() # table excel file
que_to_data = Queue()
que_to_py = Queue()
que_common_py = Queue() # common proto file to gen python file

error_flag = False  # 是否有错误
error_info = ''  # 错误信息

myutil = JUtil()

def set_error(info, to_log=True):
    """
    设置错误
    """
    logger.error(info)
    global error_info
    error_info = info
    global error_flag
    error_flag = True
    if to_log is True:
        logger.error(error_info)

def check_error():
    """
    检查是否发生过错误
    """
    return error_flag

class TaskInfo():
    def __init__(self, table, cur_use_type):
        self.table = table
        self.use_type = cur_use_type

def usage():
    """
    使用说明，参数不对的时候会提示
    """
    print('this is usage()')
    print('run_type-client.py')
    print('run.py type-client')

def delete_file(path, pattern1, pattern2=''):
    """
    做了路径处理
    """
    path = myutil.get_exe_path(path)
    file_list = os.listdir(path)
    for line in file_list:
        file_path = os.path.join(path, line)
        if os.path.isdir(file_path):
            continue
        if line.find(pattern1) != -1:
            if pattern2 != '':
                if line.find(pattern2) != -1:
                    os.remove(file_path)
            else:
                os.remove(file_path)

def clean_proto(cur_use_type):
    """
    删除上次打过的proto文件
    """
    delete_file(config.proto_path, config.common_prefix, '.py')
    delete_file(config.proto_path, config.common_prefix, '.pyc')
    if cur_use_type == MyTableTool.USE_TYPE_CLIENT:
        delete_file(config.proto_path, config.client_table_prefix, '.proto')
        delete_file(config.proto_path, config.client_table_prefix, '.py')
        delete_file(config.proto_path, config.client_table_prefix, '.pyc')
    elif cur_use_type == MyTableTool.USE_TYPE_SERVER:
        delete_file(config.proto_path, config.server_table_prefix, '.proto')
        delete_file(config.proto_path, config.server_table_prefix, '.py')
        delete_file(config.proto_path, config.server_table_prefix, '.pyc')

def clean_output(use_type):
    """
    清理输出
    """
    if use_type == MyTableTool.USE_TYPE_CLIENT:
        delete_file(config.table_data_path, '.bytes', config.client_table_prefix)
    elif use_type == MyTableTool.USE_TYPE_SERVER:
        delete_file(config.table_data_path, '.bytes', config.server_table_prefix)

def find_common():
    """
    查找common的proto
    """
    file_list = os.listdir(myutil.get_exe_path(config.proto_path))
    for filename in file_list:
        if filename.find('.proto') != -1 and filename.startswith(config.common_prefix):
            filename_without_extension = os.path.splitext(filename)[0]
            que_common_py.put(filename_without_extension)

def find_tables():
    """
    查找表格
    """
    table_list[:] = [] # 清空
    file_list = os.listdir(myutil.get_exe_path(config.table_path))
    for filename in file_list:
        if filename.find('.xlsx') != -1:
            if filename.startswith('~$') is True:  # 开着的临时文件
                logger.warn('表格[' + filename.split('$', 1)[1] + ']是打开的, 做了修改不要忘记保存')
                continue
            if filename.find(' - ') != -1:
                # 有时候会复制一个表格前后对比，直接复制的名字是“XXX - 副本.xlsx”，做一下粗略过滤
                continue
            filepath = os.path.join(myutil.get_exe_path(config.table_path), filename)
            table = MyTable(filepath, myutil.get_exe_path('./'))
            table_list.append(table)
            que_table_path.put(table)
            if DEBUG_MODE:
                logger.info('pack:' + filename)

class ThreadHandleCommon(threading.Thread):
    F_GEN_PROTO_PY = 0  # 生成python文件

    def __init__(self, que, flag):
        threading.Thread.__init__(self)
        self.que = que
        self.flag = flag

    def run(self):
        try:
            self.run2()
        except Exception as e:
            logger.error("ThreadHandleCommon: {}, {}".format(e, traceback.format_exc()))
            raise

    def run2(self):
        while True:
            if self.flag == self.F_GEN_PROTO_PY:
                data = self.que.get()
                ret = Config.generate_proto_py_file(data)
                self.que.task_done()
                if ret.returncode != 0:
                    set_error('generate_proto_py_file:\n' + ret.stderr)

class ThreadHandleTable(threading.Thread):
    F_PROTO = 0  # 生成proto文件
    F_PYTHON = 1  # 生成python文件
    F_BYTE = 2  # 生成byte文件

    def __init__(self, cur_use_type, flag, que, only_byte):
        threading.Thread.__init__(self)
        self.use_type = cur_use_type
        self.flag = flag
        self.que = que
        self.only_byte = only_byte

    def run(self):
        try:
            self.run2()
        except Exception as e:
            logger.error("ThreadHandleTable: {}, {}".format(e, traceback.format_exc()))

    def run2(self):
        while True:
            if self.flag == self.F_PROTO:
                data = self.que.get()
                table = data
                if not table.is_empty_excel():
                    if not self.only_byte:
                        if self.use_type == MyTableTool.USE_TYPE_CLIENT:
                            table.to_proto(MyTableTool.USE_TYPE_CLIENT)
                        elif self.use_type == MyTableTool.USE_TYPE_SERVER:
                            table.to_proto(MyTableTool.USE_TYPE_SERVER)

                        if self.use_type == MyTableTool.USE_TYPE_CLIENT:
                            que_to_py.put(TaskInfo(table, MyTableTool.USE_TYPE_CLIENT))
                        elif self.use_type == MyTableTool.USE_TYPE_SERVER:
                            que_to_py.put(TaskInfo(table, MyTableTool.USE_TYPE_SERVER))
                    if self.use_type == MyTableTool.USE_TYPE_CLIENT:
                        que_to_data.put(TaskInfo(table, MyTableTool.USE_TYPE_CLIENT))
                    elif self.use_type == MyTableTool.USE_TYPE_SERVER:
                        que_to_data.put(TaskInfo(table, MyTableTool.USE_TYPE_SERVER))
                self.que.task_done()
            elif self.flag == self.F_PYTHON:
                data = self.que.get()
                table_prefix = ''
                if data.use_type == MyTableTool.USE_TYPE_CLIENT:
                    table_prefix = config.client_table_prefix
                else:
                    table_prefix = config.server_table_prefix
                ret = Config.generate_proto_py_file('{}{}'.format(table_prefix, data.table.name))
                self.que.task_done()
                if ret.returncode != 0:
                    set_error('generate_proto_py_file:\n' + str(ret.stderr))
            elif self.flag == self.F_BYTE:
                if self.que.empty() is False:
                    data = self.que.get()
                    ret = data.table.to_data(data.use_type)
                    self.que.task_done()
                    if ret is False:
                        set_error('error', False)
            else:
                continue

def run_async(cur_use_type, only_byte):
    """
    运行
    """
    global enter_cwd_dir
    work_path = os.getcwd()

    if not only_byte:
        find_common()
    find_tables()

    thread_cnt = int(que_table_path.qsize() / 3)
    if thread_cnt < 2:
        thread_cnt = 2
    if thread_cnt > 20:
        thread_cnt = 20

    if not only_byte:
        thread_cnt2 = que_common_py.qsize() / 2
        if thread_cnt2 < 2:
            thread_cnt2 = 2
        if thread_cnt2 > 20:
            thread_cnt2 = 20

    # gen common py
    if not only_byte:
        os.chdir(myutil.get_exe_path(config.proto_path))
        enter_cwd_dir = os.getcwd()

        for i in range(thread_cnt2):
            t = ThreadHandleCommon(que_common_py, ThreadHandleCommon.F_GEN_PROTO_PY)
            t.daemon = True
            t.start()

        logger.info("before que_common_py.join")
        que_common_py.join()
        logger.info("after que_common_py.join")
        if check_error():
            os.chdir(work_path)
            enter_cwd_dir = os.getcwd()
            return

    # gen table proto
    os.chdir(work_path)
    enter_cwd_dir = os.getcwd()
    for i in range(thread_cnt):
        t = ThreadHandleTable(cur_use_type, ThreadHandleTable.F_PROTO, que_table_path, only_byte)
        t.daemon = True
        t.start()
    logger.info("before que_table_path.join")
    que_table_path.join()
    logger.info("after que_table_path.join")
    if check_error():
        return

    # gen table py, gen cs
    logger.warn('======== 设置路径:\n' + myutil.get_exe_path(config.proto_path))
    os.chdir(myutil.get_exe_path(config.proto_path))
    if only_byte is False:
        for i in range(thread_cnt):
            t = ThreadHandleTable(cur_use_type, ThreadHandleTable.F_PYTHON, que_to_py, only_byte)
            t.daemon = True
            t.start()
        logger.info("before que_to_py.join")
        que_to_py.join()
        logger.info("after que_to_py.join")
        if check_error():
            logger.warn('error\n')
            return
        logger.warn('cs 输出路径:\n' + config.table_cs_path + " " + config.client_table_prefix)
        
        args1 = '--csharp_out={}'.format(config.table_cs_path)
        args2 = '{}*.proto'.format(config.client_table_prefix)
        logger.warn('arg1 = ' + args1)
        logger.warn('arg2 = ' + args2)
        ret = Config.generate_cs_file(config.table_cs_path, config.client_table_prefix)
        if ret.returncode != 0:
            set_error('generate_cs_file:\n' + str(ret.stderr))
        if check_error():
            return

    # gen table data
    os.chdir(work_path)
    enter_cwd_dir = os.getcwd()
    for i in range(thread_cnt):
        t = ThreadHandleTable(cur_use_type, ThreadHandleTable.F_BYTE, que_to_data, only_byte)
        t.daemon = True
        t.start()

    logger.info("before que_to_data.join")
    que_to_data.join()
    logger.info("after que_to_data.join")
    if check_error():
        return

def out_time(des):
    """
    输出运行时间
    """
    logger.info('{} 用时:{}'.format(des, time.time() - start_time))

if __name__ == '__main__':
    start_time = time.time()  # 运行开始时间，用来计算耗时

    arg = myutil.parse_argv()
    if 'type' not in arg:
        usage()
        exit(-1)

    use_type = MyTableTool.USE_TYPE_CLIENT
    if arg['type'] == 'client':
        use_type = MyTableTool.USE_TYPE_CLIENT
    elif arg['type'] == 'server':
        use_type = MyTableTool.USE_TYPE_SERVER

    only_byte = False  # 是否仅仅打二进制文件
    if 'only_byte' in arg and arg['only_byte'] == '1':
        only_byte = True

    log_path = myutil.get_exe_path('./table_tools')
    logger = Logger(Logger.LEVEL_INFO, log_path)
    logger.reset()

    error_flag = False
    error_info = ''

    logger.info('ready type=' + MyTableTool.use_type_to_str(use_type))

    path_dir = myutil.get_exe_path('./')
    config.table_path = os.path.join(path_dir, config.table_path)
    config.proto_path = os.path.join(path_dir, config.proto_path)
    config.table_data_path = os.path.join(path_dir, config.table_data_path)
    config.table_cs_path = os.path.join(path_dir, config.table_cs_path)

    if not only_byte:
        clean_proto(use_type)
    clean_output(use_type)
    run_async(use_type, only_byte)

    out_time('完成')
