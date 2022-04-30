using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System.IO;

public class NetUtil
{
    public static T DeserializeMsg<T>(MsgData data , bool showLog = false)
    {
        T rsp;

        using(MemoryStream stream  = new MemoryStream(data.msg))
        {
            rsp = ProtoBuf.Serializer.Deserialize<T>(stream);
        }

        return rsp;
    }
}
