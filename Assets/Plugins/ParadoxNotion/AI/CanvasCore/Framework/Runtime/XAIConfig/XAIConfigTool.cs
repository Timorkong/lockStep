/*
 * @Descripttion: 封装基础的数据转定点数
 * @Author: kevinwu
 * @Date: 2019-11-27 19:37:55
 */
using UnityEngine;

namespace AINodeCanvas.Framework
{
    public static class XAIConfigTool
    {
        /// <summary>
        /// 位置转为像数需要乘以100再转定点数防溢出float->long转换（乘以一万）
        /// </summary>
        /// <param name="value">value To Translate</param>
        /// <returns></returns>
        public static long FloatToPBPosLong(this float value)
        {
            value = value * 100.0f;
            if (value > long.MaxValue / 10000)
            {
                Debug.LogWarning("Float Too Large");
                return long.MaxValue;
            }
            if (value < long.MinValue / 10000)
            {
                Debug.LogWarning("Float too small");
                return long.MinValue;
            }
            return (long)(value * 10000);
        }

        /// <summary>
        /// Z位置转为像数需要乘以100，导入到laya需要*-1再转定点数防溢出float->long转换（乘以一万）
        /// </summary>
        /// <param name="value">value To Translate</param>
        /// <returns></returns>
        public static long FloatToPBPosZLong(this float value)
        {
            value = value * -100.0f;
            if (value > long.MaxValue / 10000)
            {
                Debug.LogWarning("Float Too Large");
                return long.MaxValue;
            }
            if (value < long.MinValue / 10000)
            {
                Debug.LogWarning("Float too small");
                return long.MinValue;
            }
            return (long)(value * 10000);
        }

        /// <summary>
        /// 防溢出float->long转换（乘以一万）
        /// yiqiudong，2019-10-12
        /// </summary>
        /// <param name="value">value To Translate</param>
        /// <returns></returns>
        public static long FloatToPBLong(this float value)
        {
            //无定点数
            return (long)(value);
            //if (value > long.MaxValue / 10000)
            //{
            //    Debug.LogWarning("Float Too Large");
            //    return long.MaxValue;
            //}
            //if (value < long.MinValue / 10000)
            //{
            //    Debug.LogWarning("Float too small");
            //    return long.MinValue;
            //}
            //return (long)(value * 10000);
        }

        /// <summary>
        /// 防溢出float->int转换（乘以一万）
        /// yiqiudong，2019-10-22
        /// </summary>
        /// <param name="value">value To Translate</param>
        /// <returns></returns>
        public static int FloatToPBInt(this float value)
        {
            //无定点数
            return (int)(value);
            //if (value > int.MaxValue / 10000)
            //{
            //    Debug.LogWarning("Float Too Large");
            //    return int.MaxValue;
            //}
            //if (value < int.MinValue / 10000)
            //{
            //    Debug.LogWarning("Float Too Small");
            //    return int.MinValue;
            //}
            //return (int)(value * 10000);
        }

        /// <summary>
        /// pb数据导入long->float（除以一万）
        /// yiqiudong，2019-10-12
        /// </summary>
        /// <param name="value">value To load</param>
        /// <returns></returns>
        public static float PBLongToFloat(this long value)
        {
            return value / 10000f;
        }

        public static int Type2TypeId(System.Type t)
        {
            if (t == typeof(int))
            {
                return 1;
            }
            else if (t == typeof(float))
            {
                return 2;
            }
            else if (t == typeof(string))
            {
                return 3;
            }
            return 0;
        }

    }
}

