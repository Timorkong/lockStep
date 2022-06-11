# !/usr/bin/python
# encoding=utf-8
# ==================================================
#   Excel表格数据 -> 二进制数据
#   Powered By 赵广宇, michaelpublic@qq.com
# --------------------------------------------------
#   Copyright © 2014 赵广宇. All Rights Reserved.
# ==================================================
#   更新：Jerrylai 2020.12.17
# ==================================================

import os
import re
import sys
import xlrd
import traceback
from logger import Logger

class Config(object):
    """
    配置模块
    """
    def __init__(self):
        self.max_field_number = 99 # 单表最大字段数
        
        self.workbook_dir = './../table/' # Excel表格文件目录
        self.proto_dir = './../proto/' # proto文件目录
        self.output_dir = './../../Assets/Data/Tables/' # 输出二进制文件目录

        self.default_proto_prefix = 'c_table_' # proto文件前缀
        self.default_output_prefix = 'c_table_' # 输出二进制文件前缀
        self.default_output_suffix = 'bytes' # 输出二进制文件后缀

class TableWriter(object):
    """
    处理模块
    """
    def __init__(self, workbook, sheet, pb2, array, output):
        """
        workbook - Excel文件名，例如：Avatar
        sheet - 页签索引，例如：0
        pb2 - proto的pb2文件名，例如：c_table_Avatar_pb2
        array - row结构数组类型名，例如：AVATAR_ARRAY
        """
        self.workbook_name = workbook

        # 打开工作表
        try:
            path = config.workbook_dir + '/' + workbook + '.xlsx'
            self.workbook = xlrd.open_workbook(path)
        except IOError:
            path = config.workbook_dir + '/' + workbook + '.xls'
            self.workbook = xlrd.open_workbook(path)
        if LOG_MODE:
            logger.info('打开文件 {}'.format(workbook))

        # 打开页签
        if type(sheet) == int or (type(sheet) == str and sheet.isdigit()):
            self.sheet = self.workbook.sheet_by_index(int(sheet))
        else:
            raise TypeError('工作簿页签参数类型错误')
        
        self.sheet_name = self.sheet.name.encode('utf8')
        if LOG_MODE:
            logger.info('加载页签 {}'.format(self.sheet_name))

        # 加载PB协议模块
        if config.proto_dir not in sys.path:
            sys.path.append(config.proto_dir)
        self.pb2 = __import__(pb2)
        if LOG_MODE:
            logger.info('导入PB协议 {}'.format(pb2))
        
        # 表格行存储
        self.row_array = getattr(self.pb2, array)()

        # 表格行描述
        #self.row_descriptor = self.row_array.rows._message_descriptor
        self.row_descriptor = getattr(self.pb2, array[:len(array)-6]).DESCRIPTOR
        
        # 输出文件
        self.output = output

    def __call__(self):
        """
        处理一个表格\n
        output - 输出文件，例如：c_table_AVATAR.bytes
        """

        # 遍历表格处理数据
        for nrow in range(self.sheet.nrows):
            if nrow < 4:
                # 前4行是表头，跳过
                continue
            row_values = self.sheet.row_values(nrow)
            if str(row_values[0]).strip() == '':
                # 空行跳过
                continue
            try:
                self.deal_row_values(row_values, nrow + 1)
            except KeyError as e:
                logger.error('处理行数据失败\n表格:{}|{} 行号:{}\n数据:{}\n原因:{}\n补充:{}'.format(
                    self.workbook_name,
                    self.sheet_name,
                    nrow + 1,
                    row_values,
                    '枚举值越界，可能是数据填错，也可能需要重打结构',
                    e.args))
                exit(1)
            except Exception as e:
                logger.error('处理行数据失败\n表格:{}|{} 行号:{}\n数据:{}\n可能原因:数据类型不匹配，枚举值越界（如果检查表格没问题，可能proto文件对不上了，需要完整打表）\n堆栈如下：\n{}'.format(
                    self.workbook_name,
                    self.sheet_name,
                    nrow + 1,
                    row_values,
                    traceback.format_exc()))
                exit(1)
        # 写入文件
        with open(config.output_dir + '/' + self.output, 'wb') as f:
            f.write('TBL'.encode('utf-8'))
            f.write(self.row_array.SerializeToString())

    def deal_row_values(self, row_values, row_id):
        """
        处理一行数据\n
        row_values - 一行数据\n
        row_id - 行号，从1开始计数        
        """
        # Tip: 这里开始不能打印 value，有些类型会导致脚本异常，原因是类型转换不正常

        if LOG_MODE:
            logger.info('处理行数据|{}'.format(str(row_values)))
        row = self.row_array.rows.add()
        for descriptor in self.row_descriptor.fields:
            if descriptor.number > config.max_field_number:
                continue
            value = row_values[descriptor.number - 1]
            # 全部转成unicode进行后续处理
            #if type(value) == str:
            #    value = value.decode('utf8')
            # 空字段不处理，使用默认值
            if type(value) == str and len(value.strip()) == 0:
                continue
            if LOG_MODE:
                self.deal_field_value(row, descriptor, value)
            else:
                try:
                    self.deal_field_value(row, descriptor, value)
                except ValueError as e:
                    logger.error('处理字段数据失败\n表格:{}|{} 行号:{} 列号:{}({})\n数据:{}\n原因:{}\n补充:{}'.format(
                        self.workbook_name,
                        self.sheet_name,
                        row_id,
                        chr(ord('A') + int(descriptor.number) - 1),
                        descriptor.name,
                        str(row_values),
                        '数据类型不匹配，可能是数据填错，也可能需要重打结构',
                        e.args))
                    exit(1)
                except Exception as e:
                    if len(e.args) == 3 and e.args[0] == 'MyError':
                        logger.error('处理字段数据失败\n表格:{}|{} 行号:{} 列号:{}({})\n数据:{}\n原因:{}\n建议:{}'.format(
                            self.workbook_name,
                            self.sheet_name,
                            row_id,
                            chr(ord('A') + int(descriptor.number) - 1),
                            descriptor.name,
                            str(row_values),
                            e.args[1],
                            e.args[2]))
                    else:
                        logger.error('处理字段数据失败\n表格:{}|{} 行号:{} 列号:{}({})\n数据:{}\n可能原因:{}\n补充信息:{}\n堆栈如下:\n{}'.format(
                            self.workbook_name,
                            self.sheet_name,
                            row_id,
                            chr(ord('A') + int(descriptor.number) - 1),
                            descriptor.name,
                            str(row_values),
                            'proto文件对不上了，需要重打结构',
                            e.args,
                            traceback.format_exc()))
                    exit(1)
        tmp = str(row) # 枚举越界类型的异常在处理数据的时候并不能报出来，这里做一次类型转换可以暴露出来
        if LOG_MODE:
            logger.info('行数据处理结果\n{}'.format(str(row)))

    def deal_field_value(self, pdata, descriptor, value, pstruct_name=''):
        """
        处理一个具体数据\n
        pdata - 父数据\n
        descriptor - 描述器\n
        value - 表格数据\n
        pstruct_name - 父数据是结构时结构名
        """
        if descriptor.type != descriptor.TYPE_MESSAGE:
            if descriptor.label == descriptor.LABEL_REPEATED:
                self.deal_normal_list(pdata, descriptor, value)
            else:
                setattr(pdata, descriptor.name, self.base_value_handle(descriptor, value, pstruct_name))
        else:
            if descriptor.label == descriptor.LABEL_REPEATED:
                value_arr = self.split_struct_data(value, True)
                for struct_expr in value_arr:
                    self.deal_struct(getattr(pdata, descriptor.name).add(), struct_expr)
            else:
                self.deal_struct(getattr(pdata, descriptor.name), value)

    def split_struct_data(self, value, struct_list=False):
        """
        拆分结构数据\n
        value - 表格数据\n
        struct_list - 是否是非普通(结构)结构列表
        """
        ret = []
        if value == '':
            return ret

        if not struct_list:
            value = value[1:-1] # 去除最外层的{}
        op = '^'
        if struct_list:
            op = '|'

        left_cnt = 0 # 左花括号数量
        data = ''
        for v in value:
            if v == op and left_cnt == 0:
                if data != '':
                    ret.append(data)
                data = ''
                continue
            data = data + v
            if v == '{':
                left_cnt = left_cnt + 1
            elif v == '}':
                left_cnt = left_cnt - 1
        if data != '':
            ret.append(data)
        return ret
        
    def deal_struct(self, struct, value):
        """
        解析结构化数据\n
        struct - 结构字段\n
        value - 表格数据
        """
        value = value.strip()

        if re.match('\{[^\^]*(\^[^\^]*)*\}', value) == None:
            raise Exception('MyError', '结构表达式错误', '检查结构的写法')

        fields = self.split_struct_data(value)
        
        for descriptor in struct.DESCRIPTOR.fields:
            if descriptor.number > len(fields) or descriptor.number > config.max_field_number:
                continue
            field_value = fields[descriptor.number - 1]
            if len(field_value.strip()) == 0:
                continue

            self.deal_field_value(struct, descriptor, field_value, struct.DESCRIPTOR.name)

    def deal_normal_list(self, pdata, descriptor, value):
        """
        处理普通(非结构)列表数据\n
        pdata - 父数据\n
        descriptor - 描述器\n
        value - 表格数据
        """
        if type(value) == str:
            for section in value.strip().split('|'):
                getattr(pdata, descriptor.name).append(self.base_value_handle(descriptor, section.strip()))
        else:
            getattr(pdata, descriptor.name).append(self.base_value_handle(descriptor, value))

    def base_value_handle(self, descriptor, val, struct_name=''):
        """
        基本值处理\n
        descriptor - 描述器\n
        val - 表格数据\n
        struct_name - 结构时把结构名传一下
        """
        ret = self.type_cast(descriptor, val)
        if descriptor.type == descriptor.TYPE_STRING:
            # 换行在打表时处理成统一的真换行
            ret = ret.replace('\\n', '\n')
        ret = self.handle_big_value(descriptor, ret, struct_name)
        return ret

    def handle_big_value(self, descriptor, val, struct_name=''):
        """
        辅助功能，处理大数\n
        descriptor - 描述器\n
        val - 表格数据\n
        struct_name - 结构时把结构名传一下
        """
        # 简单测试过滤，需要的话，补齐过滤列表
        big_value_name = []
        
        # 测试名
        test_name = descriptor.name
        if struct_name != '':
            test_name = '{}.{}'.format(struct_name, test_name)

        if descriptor.type is descriptor.TYPE_STRING and big_value_name.count(test_name) > 0:
            if len(val) <= 4: # 充钻石也是当奖励来配置的，不能省略了，现在最多4位，简单过滤
                return val
            return self.big_val_str_2_az(val)
        return val
    
    def big_val_str_2_az(self, value):
        """
        辅助功能，大数字符串(普通/科学计数法/AZ串)转AZ串
        """
        # print('input', value)

        is_scientific = False # 是否是科学计数法
        is_AZ = False # 是否已经是AZ串
        if value.find('+') != -1:
            is_scientific = True
        
        if is_scientific is False:
            for c in value:
                if ord(c) >= ord('A') and ord(c) <= ord('Z'):
                    is_AZ = True
                    break
        
        if is_AZ is True:
            return value

        t_val = 0
        if is_scientific:
            value = value.replace('+', '')
            scale_count = -1 # 放大的次数
            exp_value = -1 # 指数的值
            for idx in range(len(value)):
                if value[idx] == '.':
                    scale_count = 0
                    continue
                if value[idx] == 'E':
                    exp_value = 0
                    continue
                if exp_value == -1:
                    t_val = t_val * 10 + ord(value[idx]) - ord('0')
                    if scale_count != -1:
                        scale_count = scale_count + 1
                else:
                    exp_value = exp_value * 10 + ord(value[idx]) - ord('0')
            if exp_value > 0:
                if scale_count > 0:
                    exp_value = exp_value - scale_count
                if exp_value > 0:
                    t_val = t_val * pow(10, exp_value)
        else:
            t_val = int(value)
        # print('t_val', t_val)

        str_value = ''
        thousand_count = 0 # 有多少次除1000
        t_val_pre = t_val
        while t_val >= 1000000000:
            thousand_count = thousand_count + 3
            t_val = t_val / 1000000000
        while t_val >= 1000000:
            thousand_count = thousand_count + 2
            t_val = t_val / 1000000
        while t_val >= 1000:
            thousand_count = thousand_count + 1
            t_val_pre = t_val
            t_val = t_val / 1000
        if thousand_count > 0:
            t_val_pre = t_val_pre / 10
            str_value = '{}.{}'.format(t_val, str(t_val_pre % 100).zfill(2))
            chr_list = []
            while True:
                thousand_count = thousand_count - 1
                chr_list.append(thousand_count % 26)
                thousand_count = thousand_count / 26
                if thousand_count <= 0:
                    break
            chr_list_len = len(chr_list)
            for idx in range(chr_list_len):
                idx1 = chr_list_len - 1 - idx
                str_value = '{}{}'.format(str_value, chr(ord('A') + chr_list[idx1]))
        else:
            str_value = str(t_val)
        # print('str_value', str_value)
        return str_value

    @staticmethod
    def type_cast(descriptor, value):
        FLOAT_TYPE = (
                descriptor.TYPE_DOUBLE, # 1
                descriptor.TYPE_FLOAT, # 2
                )
        LONG_TYPE = (
                descriptor.TYPE_INT32, # 5
                descriptor.TYPE_INT64, # 3
                descriptor.TYPE_SINT32, # 17
                descriptor.TYPE_SINT64, # 18
                descriptor.TYPE_UINT32, # 13
                descriptor.TYPE_UINT64, # 4
                descriptor.TYPE_FIXED32, # 7
                descriptor.TYPE_FIXED64, # 6
                descriptor.TYPE_SFIXED32, # 15
                descriptor.TYPE_SFIXED64, # 16
                descriptor.TYPE_ENUM, # 14
                )
        BOOL_TYPE = (
                descriptor.TYPE_BOOL, # 8
                )
        STR_TYPE = (
                descriptor.TYPE_BYTES, # 12
                descriptor.TYPE_STRING, # 9
                )
        # descriptor.TYPE_MESSAGE - 11

        if descriptor.type in FLOAT_TYPE:
            if type(value) == str:
                return float(value) if len(value) else 0.0
            else:
                return float(value)
        elif descriptor.type in LONG_TYPE:
            if type(value) == str:
                return int(value) if len(value) else 0
            else:
                return int(value)
        elif descriptor.type in BOOL_TYPE:
            if type(value) == str: # 修复结构里的 bool 值总是被解析成 true
                value = value.encode('utf8')
                value = int(value)
            return bool(value)
        elif descriptor.type in STR_TYPE:
            if type(value) == float and value == int(value):
                value = int(value)
            if type(value) != str:
                return str(value)
            return value
        else:
            raise Exception('MyError', 'PB字段类型无法转换', '让程序检查')

def usage(arg0):
    print('假定要处理的 xlsx 文件命名为 Workbook.xlsx')
    print(arg0 + ' Workbook.xlsx [其他可选参数如下]')
    print('  参数   描述                     默认值                      相当于输入')
    print('  -s     页签索引                 [0]                         0')
    print('  -p     proto文件编译的pb2文件名  [c_table_workbook_pb2]     c_table_workbook')
    print('  -m     row结构类型名            [WORKBOOK]                 WORKBOOK')
    print('  -o     输出文件名               [c_table_workbook.bytes]   c_table_workbook')

def parse_arg(argv):
    """
    解析参数
    """
    if len(argv) < 2:
        return False, None

    workbook = None # Excel文件名
    sheet = None # 页签索引，0开始计数
    proto = None # pb2文件名
    proto_message = None # row结构类型名，实际用不到
    proto_message_array = None # row结构数组类型名，根据proto_message得到
    output = None # 输出文件名

    idx = 1
    while idx < len(argv):
        arg = argv[idx]
        if arg == '-s':
            if sheet is not None:
                return False, None
            idx += 1
            sheet = argv[idx]
        elif arg == '-p':
            if proto is not None:
                return False, None
            idx += 1
            proto = argv[idx] + '_pb2'
        elif arg == '-m':
            if proto_message is not None:
                return False, None
            idx += 1
            proto_message = argv[idx]
        elif arg == '-o':
            if output is not None:
                return False, None
            idx += 1
            output = argv[idx] + '.' + config.default_output_suffix
        else:
            if workbook is not None:
                return False, None
            workbook = arg
        idx += 1
    
    if workbook is None:
        return False, None
    
    if sheet is None:
        sheet = 0
    
    if proto is None:
        proto = config.default_proto_prefix + workbook.lower() + '_pb2'
    
    if proto_message is None:
        proto_message = workbook.upper()
    proto_message_array = proto_message + '_ARRAY'
    
    if output is None:
        output = config.default_output_prefix + workbook.lower() + '.' + config.default_output_suffix
    
    return True, (workbook, sheet, proto, proto_message_array, output)

logger = Logger() # 日志模块
config = '' # 配置信息
enter_cwd_dir = ''
python_file_dir = ''
LOG_MODE = False # 开发调试时打印日志

def get_exe_path(simple_path):
    global enter_cwd_dir
    global python_file_dir
    return os.path.join(enter_cwd_dir, python_file_dir, simple_path)

if __name__ == '__main__':
    enter_cwd_dir = os.getcwd()
    python_file_dir = os.path.dirname(sys.argv[0])
    
    log_level = Logger.LEVEL_WARN
    if LOG_MODE:
        log_level = Logger.LEVEL_INFO
    logger = Logger(log_level, get_exe_path('./table_tools'), LOG_MODE)
    if LOG_MODE:
        logger.reset()

    config = Config()
    config.output_dir = get_exe_path(config.output_dir)
    config.workbook_dir = get_exe_path(config.workbook_dir)
    config.proto_dir = get_exe_path(config.proto_dir)

    success, args = parse_arg(sys.argv)
    if not success:
        usage(sys.argv[0])
        exit(-1)

    if not os.path.exists(config.output_dir):
        os.makedirs(config.output_dir)

    if LOG_MODE:
        logger.info('参数: ' + str(args))
    TableWriter(*args)()