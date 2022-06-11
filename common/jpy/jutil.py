#!python3
# encoding=utf-8
# version: 2022-01-21-00
"""
基础工具\n
路径转换、本地缓存、参数解析\n
支持 Python2 和 Python3
"""

import sys
import os
import codecs
import json

class JUtil:
    """
    基础工具\n
    路径转换、本地缓存、参数解析
    """
    def __init__(self, changeStdout=True):
        """
        初始化\n
        changeStdout - 是否修改标准输出，用来解决中文乱码
        """
        self.enter_cwd_dir = os.getcwd()
        self.python_file_dir = os.path.dirname(sys.argv[0])
        if self.is_py3():
            if changeStdout and hasattr(sys.stdout, 'buffer'):
                sys.stdout = codecs.getwriter('utf8')(sys.stdout.buffer)
        else:
            reload(sys)
            sys.setdefaultencoding('utf-8')

    def get_exe_path(self, simple_path):
        """
        相对路径转绝对路径\n
        simple_path - 简单路径
        """
        return os.path.join(self.enter_cwd_dir, self.python_file_dir, simple_path)

    def clear_data(self, data_path='./local_data.log'):
        """
        清理缓存\n
        data_path - 文件路径
        """
        data_path = self.get_exe_path(data_path)
        if os.path.exists(data_path):
            os.remove(data_path)

    def get_data(self, key, default_v='', data_path='./local_data.log'):
        """
        获取本地缓存\n
        key - 键值\n
        default_v - 默认值\n
        data_path - 文件路径
        """
        data_path = self.get_exe_path(data_path)
        if not os.path.exists(data_path):
            return default_v
        with codecs.open(data_path, 'r', 'utf-8') as file_handler:
            try:
                datas = json.load(file_handler)
                if key in datas:
                    return datas[key]
            except:
                pass
        return default_v
    
    def set_data(self, key, val, data_path='./local_data.log'):
        """
        设置本地缓存\n
        key - 键值\n
        val - 内容\n
        data_path - 文件路径
        """
        data_path = self.get_exe_path(data_path)
        datas = {}
        if os.path.exists(data_path):
            with codecs.open(data_path, 'r', 'utf-8') as file_handler:
                try:
                    datas = json.load(file_handler)
                except:
                    pass
        datas[key] = val
        with codecs.open(data_path, 'w', 'utf-8') as file_handler:
            datas_str = json.dumps(datas)
            file_handler.write(datas_str)

    def parse_argv(self):
        """
        解析当前参数，传入或来自文件名，返回参数字典\n
        work^k1--v1^k2--v2.py\n
        work^k1-v1^k2-v2.py\n
        work.py k1--v1^k2--v2\n
        work.py k1-v1^k2-v2
        """
        if len(sys.argv) < 1:
            return {}
        # 解析文件名参数
        if len(sys.argv) == 1:
            file_name = sys.argv[0]
            file_name = os.path.split(file_name)[1]  # 去掉目录
            file_name = file_name.split('.')[0]  # 去掉后缀
            if file_name.count('^') > 0:
                pars = file_name.split('^', 1)[1]  # 去掉文件名
                return self.parse_give_argv(pars)
            else:
                return {}
        else:
            return self.parse_give_argv(sys.argv[1])
    
    @staticmethod
    def parse_give_argv(argvs):
        """
        解析参数，返回参数字典\n
        argvs - 参数串，形如：k1--v1^k2--v2 或 k1-v1^k2-v2
        """
        ret = {}
        pars = argvs.split('^')  # 分离参数
        for par in pars:
            par_arr = par.split('--')
            if len(par_arr) == 2:
                ret[par_arr[0]] = par_arr[1]
                continue

            par_arr = par.split('-')
            if len(par_arr) == 2:
                ret[par_arr[0]] = par_arr[1]
        return ret

    @staticmethod
    def is_py3():
        """
        是否是 Python 3.x
        """
        return sys.version.startswith('3.')
    
    @staticmethod
    def has_sys_cmd(cmd_name):
        """
        是否存在系统指令，返回 True/False\n
        cmd_name - 指令名称，如：notepad、Python3
        """
        return JUtil.has_sys_cmd2(cmd_name)[0]
    
    @staticmethod
    def has_sys_cmd2(cmd_name):
        """
        是否存在系统指令，返回 [True/False, 'cmd_path']\n
        cmd_name - 指令名称，如：notepad、Python3
        """
        os_cmds = os.getenv('path', '').split(';')
        for cmd in os_cmds:
            if cmd == '':
                continue
            cmd_path = os.path.join(cmd, './{}.exe'.format(cmd_name))
            if os.path.exists('{}'.format(cmd_path)):
                return [True, cmd_path]
        return [False, '']
    
    @staticmethod
    def change_python_version(cmd='Python3'):
        """
        改变 Python 版本\n
        cmd - Python 命令
        """
        if JUtil.has_sys_cmd(cmd):
            argv_str = ''
            for x in sys.argv:
                argv_str = '{} "{}"'.format(argv_str, x)
            os.system('{}{}'.format(cmd, argv_str))