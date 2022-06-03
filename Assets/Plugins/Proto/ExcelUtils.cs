using ExcelDataReader;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace ExcelToolNS
{
    public class ExcelUtils
    {
        public static int ParseInt(string str)
        {
            if (!int.TryParse(str, out int o))
            {
                //Error
            }
            return o;
        }

        public static float ParseFloat(string str)
        {
            float.TryParse(str, out float o);
            return o;
        }

        public static bool ParseBool(string str)
        {
            return ParseInt(str) == 1;
        }

        public static double ParseDouble(string str)
        {
            double.TryParse(str, out double o);
            return o;
        }

        public static string[] ParseStringArr(string str, char separator)
        {
            return str?.Split(separator);
        }

        public static int[] ParseIntArr(string str, char separator)
        {
            List<int> list = new List<int>();

            if (!string.IsNullOrEmpty(str))
            {
                var strArr = str.Split(separator);
                foreach (var item in strArr)
                {
                    list.Add(ParseInt(item));
                }
            }

            return list.ToArray();
        }

        public static float[] ParseFloatArr(string str, char separator)
        {
            List<float> list = new List<float>();

            if (!string.IsNullOrEmpty(str))
            {
                var strArr = str.Split(separator);
                foreach (var item in strArr)
                {
                    list.Add(ParseFloat(item));
                }
            }

            return list.ToArray();
        }

        public static double[] ParseDoubleArr(string str, char separator)
        {
            List<double> list = new List<double>();

            if (!string.IsNullOrEmpty(str))
            {
                var strArr = str.Split(separator);
                foreach (var item in strArr)
                {
                    list.Add(ParseDouble(item));
                }
            }

            return list.ToArray();
        }

        public static bool[] ParseBoolArr(string str, char separator)
        {
            List<bool> list = new List<bool>();

            if (!string.IsNullOrEmpty(str))
            {
                var strArr = str.Split(separator);
                foreach (var item in strArr)
                {
                    list.Add(ParseBool(item));
                }
            }

            return list.ToArray();
        }

        public static string ParseFieldName(string key, bool firstLower = false)
        {
            var strArr = key.Split('_');
            var str = strArr[0];

            if (firstLower)
            {
                str = str.Substring(0, 1).ToLower() + str.Substring(1);
            }

            for (int i = 1; i < strArr.Length; i++)
            {
                str += strArr[i].Substring(0, 1).ToUpper() + strArr[i].Substring(1);
            }

            return str;
        }

        public static ExcelWorksheet GetExcelWorksheet(string path, string sheetName)
        {
            //var fileinfo = new FileInfo(path);
            //只读
            using (var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                ExcelPackage ep = new ExcelPackage(fs);
                return ep.Workbook.Worksheets[sheetName];
            }
        }

        public static string[][] ReadExcelSheetData(string path, string sheetName)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                //读表方式 1.ExcelDataReader
                using (var reader = ExcelReaderFactory.CreateReader(file))
                {
                    do
                    {
                        List<string[]> dataList = new List<string[]>();

                        int fieldCount = 0;

                        while (reader.Read())
                        {
                            if (reader.Name.Trim() != sheetName)
                            {
                                reader.NextResult();
                                continue;
                            }
                            //第一行获取有效列数
                            if (reader.Depth == 0)
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var obj = reader.GetValue(i);
                                    if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                                    {
                                        break;
                                    }
                                    fieldCount = i + 1;
                                }
                            }

                            //一行数据
                            string[] rowStrArr = new string[fieldCount];
                            for (int i = 0; i < rowStrArr.Length; i++)
                            {
                                var obj = reader.GetValue(i);
                                if (obj != null)
                                {
                                    rowStrArr[i] = obj.ToString();
                                }
                            }
                            dataList.Add(rowStrArr);
                        }

                        //去除底下行首为空的
                        for (int i = dataList.Count - 1; i >= 0; i--)
                        {
                            if (dataList[i].Length > 0 && !string.IsNullOrEmpty(dataList[i][0]))
                            {
                                break;
                            }

                            dataList.RemoveAt(i);
                        }

                        return dataList.ToArray();
                    }
                    while (reader.NextResult());
                }
            }
        }
    }
}
