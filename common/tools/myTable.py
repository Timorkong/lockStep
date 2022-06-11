# !/usr/bin/python
# encoding=utf-8

# from __future__ imports must occur at the beginning of the file
from __future__ import annotations
import os
import hashlib
import codecs
from logger import Logger
from config import Config
from util import Util
import xlrd

logger = Logger()
config = Config()
run_path_dir = ''  # 运行目录


class MyType(object):
    """
    类型
    """

    # 普通类型列表
    NORMAL_TYPE_ARR = [
        'sint32', 'uint32',
        'string', 'float',
        'double', 'bool',
        'sint64', 'uint64',
        'sfixed64', 'bytes'
    ]

    TYPE_NORAML = 0  # 普通类型
    TYPE_USER = 1  # 用户自定义类型
    TYPE_UNKNOW = 2  # 未知类型

    def __init__(self):
        self.package = ''  # 包名
        self.prefix = 'optional'  # 前缀，repeated或optional
        self.type = MyType.TYPE_UNKNOW  # 类型
        self.name = 'UNKNOW'  # 名称
        self.default_val = ''  # 默认值，枚举需要

    @staticmethod
    def judge_type(data) -> MyType:
        """
        判断类型
        """
        ret = MyType()
        if data.find('List.') != -1:  # 列表
            ret.prefix = 'repeated'
            data = data.split('.', 1)[1]
        else:
            ret.prefix = 'optional'
        ret.name = data
        if MyType.NORMAL_TYPE_ARR.count(data) > 0:
            ret.type = MyType.TYPE_NORAML
            return ret
        user_type = MyType.find_user_type(data)
        if user_type.type != MyType.TYPE_UNKNOW:
            ret.type = MyType.TYPE_USER
            ret.package = user_type.package
            if ret.prefix == 'repeated':  # repeated类型不需要默认值
                ret.default_val = ''
            else:
                ret.default_val = user_type.default_val
            return ret
        return ret

    @staticmethod
    def find_user_type(data) -> MyType:
        """
        查找用户自定义类型
        """
        ret = MyType()
        if data.find('.') == -1:  # 自定义的一定有包名
            return ret
        package_name = data.split('.', 1)[0]
        type_name = data.split('.', 1)[1]
        list_common_proto = os.listdir(config.proto_path)
        for filename in list_common_proto:
            if filename.find('.proto') == -1 or filename.startswith(config.common_prefix) is False:
                continue
            #print("finding", type_name, "in", filename)
            file_path = os.path.join(config.proto_path, filename)
            text = []
            with codecs.open(file_path, 'r', 'utf-8') as f:
                text = f.readlines()
            text_len = len(text)
            if text_len < 1:
                continue

            found = False
            for i in range(text_len):
                s = text[i].strip()
                if len(s) == 0 or s.startswith("syntax"):
                    continue
                found = s.startswith('package {};'.format(package_name))
                break
            if not found:
                continue

            for i in range(text_len):
                line = Util.strip_notes_spaces_tab(text[i])
                if line == 'message {}'.format(type_name):
                    ret.type = MyType.TYPE_USER
                    ret.package = filename
                    return ret
                elif line == 'enum {}'.format(type_name):
                    for j in range(i + 1, text_len):
                        if text[j].find('=') != -1:
                            ret.default_val = Util.strip_notes_spaces_tab(text[j].split('=')[0])
                            break
                    ret.type = MyType.TYPE_USER
                    ret.package = filename
                    return ret
                else:
                    continue
        return ret


class MyTableTool(object):
    """
    表格工具
    """

    USE_TYPE_NONE = 0  # 使用域_不使用
    USE_TYPE_CLIENT = 1  # 使用域_仅客户端使用
    USE_TYPE_SERVER = 2  # 使用域_仅服务端使用
    USE_TYPE_ALL = 3  # 使用域_客户端和服务端都使用

    def __init__(self):
        pass

    @staticmethod
    def to_unicode(data):
        """
        数据转unicode，并且去除换行
        """
        #if type(data) == str:
        #    data = data.decode('utf8')
        # 部分Excel会让字段带有换行，这里不判是否字符串类型
        data = data.replace(chr(13), '')
        data = data.replace(chr(10), '')
        return data

    @staticmethod
    def get_use_type(data):
        """
        识别使用域类型
        """
        if data.find('_') != -1:
            data = data.split('_', 1)[0]
        # 发现有些Excel表读出来的会带有一个回车，直接判相等就不通过了，改判包含
        ret = MyTableTool.USE_TYPE_NONE
        if data.find('all') != -1:
            ret = MyTableTool.USE_TYPE_ALL
        elif data.find('client') != -1:
            ret = MyTableTool.USE_TYPE_CLIENT
        elif data.find('server') != -1:
            ret = MyTableTool.USE_TYPE_SERVER
        elif data.find('none') != -1:
            ret = MyTableTool.USE_TYPE_NONE
        else:
            pass
            # logger.info('no use type : ' + str(data))
        return ret

    @staticmethod
    def use_type_to_str(use_type):
        """
        使用域类型转字符串
        """
        if use_type == MyTableTool.USE_TYPE_CLIENT:
            return 'Client'
        elif use_type == MyTableTool.USE_TYPE_ALL:
            return 'All'
        elif use_type == MyTableTool.USE_TYPE_SERVER:
            return 'Server'
        elif use_type == MyTableTool.USE_TYPE_NONE:
            return 'None'
        return 'None'

class MyTableColumn(object):
    def __init__(self, column_data, column_idx):
        self.use_type = MyTableTool.USE_TYPE_NONE
        if len(column_data) < 4:
            return
        self.use_type = MyTableTool.get_use_type(MyTableTool.to_unicode(column_data[2]))
        if self.use_type == MyTableTool.USE_TYPE_NONE:
            return
        self.idx = column_idx  # 在第几列，从1开始计数
        self.name = MyTableTool.to_unicode(column_data[0])
        self.des = MyTableTool.to_unicode(column_data[3])

        self.type_name = MyTableTool.to_unicode(column_data[1])
        self.type = MyType.judge_type(self.type_name)

    def is_type(self, use_type):
        """
        类型是否匹配
        """
        ret = False
        if use_type == self.use_type or self.use_type == MyTableTool.USE_TYPE_ALL:
            ret = True
        return ret

    def get_cs_des(self):
        """
        注释信息
        """
        return '/// <summary>\n    /// {}\n    /// </summary>'.format(self.des)

    def get_cs_type(self):
        """
        获取在c#的数据类型\n
        TODO:没在用？
        """
        tname = self.type.name
        if tname == 'sint32':
            tname = 'int'
        elif tname == 'uint32':
            tname = 'uint'

        if self.type.prefix == 'repeated':
            # 可能更长
            return 'List<{}>'.format(tname)
        else:
            return tname

    def to_proto(self, use_type, file_handler):
        if self.is_type(use_type) is False:
            return
        if self.type.type == MyType.TYPE_USER and self.type.default_val != '':
            file_handler.write('    {} {} {} = {} [default = {}]; // {}\n'.
                               format(self.type.prefix,
                                      self.type.name,
                                      self.name,
                                      self.idx,
                                      self.type.default_val,
                                      self.des))
        else:
            file_handler.write('    {} {} {} = {}; // {}\n'.
                               format(self.type.prefix,
                                      self.type.name,
                                      self.name,
                                      self.idx,
                                      self.des))


class MyTableSheet(object):
    def __init__(self, excel_sheet, sheet_idx):
        self.use_type = MyTableTool.get_use_type(excel_sheet.name)
        if self.use_type == MyTableTool.USE_TYPE_NONE:
            return
        self.idx = sheet_idx
        self.name = excel_sheet.name.split('_', 1)[1]
        self.columns : list[MyTableColumn] = []

        idx = 0  # none的列也要给索引值，不然打成二进制的时候对不上
        for i in range(excel_sheet.ncols):
            idx = idx + 1
            column = MyTableColumn(excel_sheet.col_values(i), idx)
            if column.use_type != MyTableTool.USE_TYPE_NONE:
                self.columns.append(column)

    def is_type(self, use_type):
        ret = False
        if use_type != self.use_type and self.use_type != MyTableTool.USE_TYPE_ALL:
            return ret
        for column in self.columns:
            if column.is_type(use_type) is True:
                ret = True
                break
        return ret

    def get_package(self, use_type):
        """
        获取引用的包名
        """
        ret = []
        if use_type != self.use_type and self.use_type != MyTableTool.USE_TYPE_ALL:
            return ret
        if self.is_type(use_type) is False:
            return ret
        for column in self.columns:
            if column.type.type == MyType.TYPE_USER and column.type.package != '' and ret.count(column.type.package) <= 0:
                ret.append(column.type.package)
        return ret

    def to_proto(self, use_type, file_handler):
        if use_type != self.use_type and self.use_type != MyTableTool.USE_TYPE_ALL:
            return
        if self.is_type(use_type) is False:
            return
        file_handler.write('message {}\n'.format(self.name))
        file_handler.write('{\n')
        for column in self.columns:
            column.to_proto(use_type, file_handler)
        file_handler.write('}\n\n')

        file_handler.write('message {}_ARRAY\n'.format(self.name))
        file_handler.write('{\n')
        file_handler.write('    repeated {} rows = 1;\n'.format(self.name))
        file_handler.write('}\n')

class MyTable(object):
    def __init__(self, excel_path, path_dir1):
        global run_path_dir
        run_path_dir = path_dir1
        logger.set_config(Logger.LEVEL_INFO, os.path.join(run_path_dir, './table_tools'))
        config.table_path = os.path.join(run_path_dir, config.table_path)
        config.proto_path = os.path.join(run_path_dir, config.proto_path)
        config.table_data_path = os.path.join(run_path_dir, config.table_data_path)

        self.excel_path = excel_path
        self.name = os.path.splitext(os.path.basename(excel_path))[0]
        self.sheets : list[MyTableSheet] = []

        data = xlrd.open_workbook(excel_path)
        for i in range(len(data.sheets())):
            excel_sheet = data.sheets()[i]
            sheet = MyTableSheet(excel_sheet, i)
            if sheet.use_type != MyTableTool.USE_TYPE_NONE and len(sheet.columns) > 0:
                self.sheets.append(sheet)

    def proto_name(self):
        """
        proto名称
        """
        return '{}{}'.format(config.client_table_prefix, self.name)

    def is_empty_excel(self):
        """
        是否是空的Excel表
        """
        if len(self.sheets) > 0:
            return False
        return True

    def is_type(self, use_type):
        ret = False
        if use_type != MyTableTool.USE_TYPE_CLIENT and use_type != MyTableTool.USE_TYPE_SERVER:
            return ret

        for sheet in self.sheets:
            if sheet.is_type(use_type) is True:
                ret = True
                break
        return ret

    def get_package(self, use_type):
        """
        获取引用的包名
        """
        spaces = []

        if use_type != MyTableTool.USE_TYPE_CLIENT and use_type != MyTableTool.USE_TYPE_SERVER:
            return spaces
        if self.is_type(use_type) is False:
            return spaces

        for sheet in self.sheets:
            rets = sheet.get_package(use_type)
            for ret in rets:
                if spaces.count(ret) <= 0:
                    spaces.append(ret)

        return spaces

    def to_proto(self, use_type=MyTableTool.USE_TYPE_CLIENT):
        """
        生成proto文件
        """
        if use_type != MyTableTool.USE_TYPE_CLIENT and use_type != MyTableTool.USE_TYPE_SERVER:
            return
        if self.is_type(use_type) is False:
            return

        table_prefix = ''
        table_package = ''
        if use_type == MyTableTool.USE_TYPE_CLIENT:
            table_prefix = config.client_table_prefix
            table_package = config.client_table_package
        else:
            table_prefix = config.server_table_prefix
            table_package = config.server_table_package

        with codecs.open('{}{}{}.proto'.format(config.proto_path + '/', table_prefix, self.name), 'w', 'utf-8') as f:
            f.write('// This code was generated by a tool\n\n')

            f.write('syntax = "proto2";\n\n')

            if len(self.get_package(use_type)) > 0:
                for sp in self.get_package(use_type):
                    f.write('import "{}";\n'.format(sp))
                f.write('\n')

            if table_package != '':
                f.write('package {};\n'.format(table_package))
                f.write('\n')

            idx = 0
            sheet_cnt = len(self.sheets)
            for sheet in self.sheets:
                sheet.to_proto(use_type, f)
                idx = idx + 1
                if idx != sheet_cnt:
                    f.write('\n')

    def to_data(self, use_type):
        if use_type != MyTableTool.USE_TYPE_CLIENT and use_type != MyTableTool.USE_TYPE_SERVER:
            return
        if self.is_type(use_type) is False:
            return

        table_prefix = ''
        if use_type == MyTableTool.USE_TYPE_CLIENT:
            table_prefix = config.client_table_prefix
        else:
            table_prefix = config.server_table_prefix

        for sheet in self.sheets:
            if sheet.use_type == use_type:
                ret = os.system('python3 {} -s {} -p {} -m {} -o {} {}'.
                                format(os.path.join(run_path_dir, './table_writer.py'),
                                       sheet.idx,
                                       table_prefix + self.name,
                                       sheet.name,
                                       table_prefix + sheet.name,
                                       self.name))
                if ret != 0:
                    logger.error('打表出错 : 表格文件:{} 页签:{}，详情见上一条日志'.format(self.name, sheet.name))
                    return False
        return True
