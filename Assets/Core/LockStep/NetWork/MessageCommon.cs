using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum NET_DEFINE
{
    HEAD_LENTH_SIZE = 4,//包头长度字段大小
    HEAD_MSG_ID_SIZE = 4,//包头消息号大小
    HEAD_SIZE = HEAD_LENTH_SIZE + HEAD_MSG_ID_SIZE, //包头大小
}

public class MsgData
{
    public int msgSize = 0;
    public uint msgID = 0;
    public byte[] msg = null;
    public MsgData(int size)
    {
        this.msgSize = size;
        msg = new byte[size];
    }

    public override string ToString()
    {
        string output = string.Format("msgSize = {0} msgId = {1} msg = {2}", msgSize, msgID, Util.Bytes2String(msg));

        return output;
    }
}
