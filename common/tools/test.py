import sys
import os
from config import Config
import subprocess
config = Config()
from logger import Logger
more_path = ['./../jpy']
for x in more_path:
    p = os.path.join(os.path.dirname(sys.argv[0]), x)
    if os.path.exists(p):
        sys.path.append(p)
from jutil import JUtil
myutil = JUtil()
logger = ''
log_path = myutil.get_exe_path('./table_tools')
logger = Logger(Logger.LEVEL_INFO, log_path)
logger.reset()

path = '/Users/konglingzhao/Desktop/lockStep/lockStep/common/tools/././../proto/'
os.chdir(path)
curDir = os.getcwd()
logger.warn(curDir)
out_put = '/Users/konglingzhao/Desktop/lockStep/lockStep/common/tools/././../../Assets/Common/Tables/ '
table_prefix = 'c_table_'

# p = subprocess.Popen("protoc --csharp_out=/Users/konglingzhao/Desktop/lockStep/lockStep/common/tools/././../../Assets/Common/Tables/ c_table_*.proto", stderr=subprocess.PIPE)
os.system("protoc --csharp_out=/Users/konglingzhao/Desktop/lockStep/lockStep/common/tools/././../../Assets/Common/Tables/ c_table_*.proto")

# ret = Config.generate_cs_file(out_put, table_prefix)
# if ret.returncode != 0:
#   logger.warn(str(ret.stderr))
