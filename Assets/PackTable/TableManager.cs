using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class TableManager<TableArrayT, T, K, T_1> : Singleton<T_1>, IEnumerable where TableArrayT : IMessage, new() where T_1: new() 
{ 
    /****************  Members  ****************/
    public TableArrayT array;
    public K key;
    public readonly Dictionary<K, T> dic = new Dictionary<K, T>();
    /****************  Overides  ****************/
    public IEnumerator GetEnumerator()
    {
        return dic.GetEnumerator(); 
    }


    /****************  Public Funs  ****************/
    public virtual void AddTable(T table)
    {
        K key = GetKey(table);

        // 服务器允许ID为0的KEY， 暂时注掉了
        //if (typeof(K) == typeof(uint))
        //{
        //    if ((uint)(object)key == 0)
        //    {
        //        return;
        //    }
        //}

        if (dic.ContainsKey(key))
        {
            Debug.LogError(array.ToString() + "'s key: " + key + " exist!");
        }
        else
        {
            dic.Add(key, table);
        }

        PostProcess(table);

        //Network.PrintSendMsgProperties(table);

    }

    public abstract K GetKey(T table);
    //public virtual K GetKey(T table)
    //{
    //    uint id = 0;

    //    bool invokeSuccess = false;
    //    System.Type type = table.GetType();
    //    PropertyInfo pinfo = type.GetProperty("id");
    //    if (pinfo != null)
    //    {
    //        MethodInfo mInfo = pinfo.GetGetMethod();
    //        if (mInfo != null)
    //        {
    //            invokeSuccess = true;
    //            id = Convert.ToUInt32(mInfo.Invoke(table, null));
    //        }
    //    }

    //    if (invokeSuccess == false)
    //    {
    //        Console.LogError("can not find field \"id\" in .proto, if not contain you can overwrite function \"GetKey\"");
    //    }

    //    //TypeConverter conv = TypeDescriptor.GetConverter(typeof(K));
    //    //K ret = (K)conv.ConvertFrom(id);

    //    return (K)(object)id;
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="tbl"></param>
    /// <param name="bShowErrLog">是否显示查表失败的错误日志，原来是必然要显示的，本地化过程中会频繁出这个log，因此加一个参数部分暂时屏蔽（jerrylai_2015-03-06）</param>
    /// <returns></returns>
    public virtual bool TryGetValue(K key, out T tbl, bool bShowErrLog = true)
    {
        if (!dic.TryGetValue(key, out tbl))
        {
            if (typeof(K) == typeof(long))
            {
                uint subKey = (uint)(Convert.ToInt64(key) & uint.MaxValue);
                uint id = (uint)(Convert.ToInt64(key) >> 32);

                if (bShowErrLog)
                {
                    Debug.LogError(typeof(T) + " 表格中没有 id = " + id + " sub key = " + subKey + " 的数据 !!!!");
                }
            }
            else
            {
                if (bShowErrLog)
                {
                    Debug.LogError(typeof(T) + " 表格中没有 id = " + key.ToString() + " 的数据 !!!!");
                }
            }

            return false;
        }

        return true;
    }

    protected virtual void PostProcess(T table) { }
    protected virtual void OnAllTablesLoaded() { }

    [System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
    public void OnResourceLoaded(AssetReference res)
    {
        /*
        byte[] raw_data = null;
        byte[] data = new byte[raw_data.Length - 3];
        for (int i = 0; i < raw_data.Length - 3; data[i] = raw_data[i + 3], ++i) ;
        array = NetUtil.Deserialize<TableArrayT>(data,false);

        using (MemoryStream stream = new MemoryStream(data))
        {
           // array = NetUtil.Deserialize<TableArrayT>(data);
            
            //Debug.Log(res.FileName);

            //if (GameApp.Instance.showTableContent && Application.platform == RuntimePlatform.WindowsEditor)
            //{
            //    Network.PrintRecivedMsgProperties(array);
            //}

            System.Type type = array.GetType();
            PropertyInfo pinfo = type.GetProperty("rows");
            if (pinfo != null)
            {
                MethodInfo mInfo = pinfo.GetGetMethod();
                if (mInfo != null)
                {
                    List<T> list = mInfo.Invoke(array, null) as List<T>;
                    if (list != null)
                    {
                        foreach (T table in list)
                        {
                            //Console.Log(table);

                            AddTable(table);
                        }
                    }
                }
                OnAllTablesLoaded();
            }
            else
            {
                Debug.LogError(array.ToString() + " does not has \"rows" + key + " exist!");
            }
        }
        */
        
    }
}
