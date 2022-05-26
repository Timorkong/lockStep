using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//用于接收数据
public class NetInputBuffer : RWBuffer
{
    public NetInputBuffer()
    {
    }

    public int CurrentPackLenth
    {
        get {
            int ret = ReadInt32(this.Head,false);

            return ret;
        }
    }
}
