using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//用于接收数据
public class NetInputBuffer : RWBuffer
{
    public NetInputBuffer()
    {
    }


    public bool Peek(byte[] buff, int peekLen)
    {
        int dataLen = this.Lenth;

        if (dataLen < peekLen)
        {
            Debug.LogError("获取失败，空间不足");

            return false;
        }

        int indexHead = Head;

        for (int i = 0; i < peekLen; i++)
        {
            buff[i] = this.Buffer[indexHead++];
            indexHead %= BufferLen;
        }

        return true;
    }

    public bool UpdateHead(int len)
    {
        if (len == 0) return false;

        if (len > Lenth) return false;

        Head = (Head + len) % BufferLen;

        return true;
    }
}
