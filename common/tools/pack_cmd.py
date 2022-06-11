# !/usr/bin/python
# encoding=utf-8
"""
打协议
"""

import sys
import os
import Queue
import threading
import time
import shutil
from logger import Logger
from config import Config
from svn_helper_tortoise import SvnHelperTortoise

config = Config()  # 配置
que_py = Queue.Queue()  # 生成Python文件
que_cs = Queue.Queue()  # 生成cs文件

class MainClass(object):
    """
    主类
    """
    def __init__(self):
        """
        初始化
        """
        self.enter_cwd_dir = ''  # 执行路径
        self.python_file_dir = ''  # python文件路径

        self.logger = ''  # 日志

        self.error_flag = False  # 是否有错误
        self.error_info = ''  # 错误信息
    
    def parse_arg(self, argv):
        """
        解析参数\n
        返回是否成功
        """
        if len(argv) < 1:
            return False, None
        return True, None
    
    def usage(self):
        """
        使用说明，参数不对的时候会提示
        """
        print('==这里是使用帮助==')
        print('pack_cmd.py')
    
    def __init_data__(self):
        """
        初始化数据，解析参数之后
        """
        self.logger = Logger(Logger.LEVEL_INFO, self.get_exe_path('./table_tools'))
        
    def get_exe_path(self, simple_path):
        """
        相对路径转绝对路径
        """
        return os.path.join(self.enter_cwd_dir, self.python_file_dir, simple_path)

    @staticmethod
    def set_error(info, to_log=True):
        """
        设置错误\n
        info 错误信息\n
        to_log 是否打印日志
        """
        self.error_info = info
        self.error_flag = True
        if to_log is True:
            self.logger.error(self.error_info)

    def check_error(self):
        """
        检查是否发生过错误
        """
        return self.error_flag
    
    def delete_file(self, path, pattern1, pattern2=''):
        """
        做了路径处理
        """
        path = self.get_exe_path(path)
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

    def clean_proto(self):
        self.delete_file(config.proto_path, config.common_prefix, '.py')
        self.delete_file(config.proto_path, config.common_prefix, '.pyc')
        self.delete_file(config.proto_path, config.command_prefix, '.py')
        self.delete_file(config.proto_path, config.command_prefix, '.pyc')

    def clean_output(self):
        self.delete_file(config.common_cs_path, '.cs')
        self.delete_file(config.command_cs_path, '.cs')

    def find_common_and_command(self):
        file_list = os.listdir(config.proto_path)
        for filename in file_list:
            if filename.find('.proto') != -1 and (filename.startswith('common') or filename.startswith('command_')):
                opath = config.command_cs_path
                if filename.startswith('common'):
                    opath = config.common_cs_path
                filenameWithoutExtension = os.path.splitext(filename)[0]    
                que_py.put(TaskInfo(filenameWithoutExtension, opath))

    def copy_dir(self, s, t, pattern=''):
        """
        拷贝s目录中包含pattern的文件到t目录
        """
        if os.path.exists(s) is False:
            return
        if os.path.exists(t) is False:
            os.mkdir(t)

        file_list = os.listdir(s)
        for filename in file_list:
            if pattern != '' and filename.find(pattern) == -1:
                continue
            # Unity中，不做删除的话，同名文件，只是大小写不一样，文件名不会替换
            if os.path.exists('{}/{}'.format(t, filename)):
                os.remove('{}/{}'.format(t, filename))
            shutil.copy('{}/{}'.format(s, filename), '{}/{}'.format(t, filename))

    def run_async(self):
        """
        执行
        """
        work_path = os.getcwd()
        self.find_common_and_command()
        
        thread_cnt = que_py.qsize() / 2
        if thread_cnt < 2:
            thread_cnt = 2
        if thread_cnt > 20:
            thread_cnt = 20
            
        # gen py
        os.chdir(config.proto_path)
        for i in range(thread_cnt):
            t = ThreadHandleCommonAndCommand(que_py, 0)
            t.setDaemon(True)
            t.start()
            
        que_py.join()
        if self.check_error():
            return
        
        # gen cs
        os.chdir(config.proto_path)
        for i in range(thread_cnt):
            t = ThreadHandleCommonAndCommand(que_cs, 1)
            t.setDaemon(True)
            t.start()
            
        que_cs.join()
        if self.check_error():
            return
        
        os.chdir(work_path)
    
    def run(self):
        """
        类入口
        """
        success, args = self.parse_arg(sys.argv)
        if not success:
            self.usage()
            exit(-1)
        self.enter_cwd_dir = os.getcwd()
        self.python_file_dir = os.path.dirname(sys.argv[0])
        self.__init_data__()
        self.work()
    
    def work(self):
        """
        实际工作
        """
        start_time = time.time()  # 运行开始时间，用来计算耗时
        self.logger.reset()

        path_dir = self.get_exe_path('./')
        config.proto_path = os.path.join(path_dir, config.proto_path)
        config.common_cs_path = os.path.join(path_dir, config.common_cs_path)
        config.command_cs_path = os.path.join(path_dir, config.command_cs_path)

        if config.use_tortoise_svn is True:
            config.tortoise_svn_update_path = os.path.join(path_dir, config.tortoise_svn_update_path)
        if config.copy_to_unity is True:
            config.unity_common_cs_path = os.path.join(path_dir, config.unity_common_cs_path)
            config.unity_command_cs_path = os.path.join(path_dir, config.unity_command_cs_path)

        if config.use_tortoise_svn is True:
            SvnHelperTortoise.update(config.tortoise_svn_update_path)

        self.clean_proto()
        self.clean_output()
        self.run_async()

        if config.copy_to_unity is True:
            if self.check_error() is False:
                self.copy_dir(config.common_cs_path, config.unity_common_cs_path, '.cs')
                self.copy_dir(config.command_cs_path, config.unity_command_cs_path, '.cs')

        self.logger.info('完成 用时:' + str(time.time() - start_time))

class TaskInfo():
    def __init__(self, val, opath):
        self.val = val
        self.opath = opath  # 输出路径

class ThreadHandleCommonAndCommand(threading.Thread):
    def __init__(self, que, flag):
        threading.Thread.__init__(self)
        self.que = que
        self.flag = flag
    def run(self):
        while True:
            if self.flag == 0:
                data = self.que.get()
                ret = Config.generate_proto_py_file(data.val)
                if ret.returncode == 0:
                    que_cs.put(TaskInfo(data.val, data.opath))
                self.que.task_done()
                if ret.returncode != 0:
                    MainClass.set_error('generate_proto_py_file:\n' + ret.stderr)
            elif self.flag == 1:
                data = self.que.get()
                proto_name = '{}.proto'.format(data.val)
                cs_path = '{}{}.cs'.format(data.opath + '/', data.val)
                Config.proto_to_cs(proto_name, cs_path)
                Config.delete_xml(cs_path)
                if config.enable_add_des is True:
                    Config.add_des_from_proto_to_cs('{}/{}'.format(config.proto_path, proto_name), cs_path)
                self.que.task_done()

if __name__ == '__main__':
    reload(sys)
    sys.setdefaultencoding('utf-8')

    MAIN_CLASS = MainClass()
    MAIN_CLASS.run()
