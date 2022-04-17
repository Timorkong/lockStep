using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum NET_DEFINE
{
    HEAD_LENTH_SIZE = 2,//包头长度字段大小
    HEAD_MSG_ID_SIZE = 4,//包头消息号大小
    HEAD_SEQUENCE_SIZE = 4,//包头消息序列号大小
    HEAD_SIZE = HEAD_LENTH_SIZE + HEAD_MSG_ID_SIZE + HEAD_SEQUENCE_SIZE, //包头大小
}

public class MsgData
{
    public int msgID = 0;
    public uint sequence = 0;
    public byte[] msg = null;
    public MsgData(int size)
    {
        msg = new byte[size];
    }
}
