using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cmd.ID;
using PROTOCOL;
using ProtoBuf;

[ProtoContract]
public partial class command_rsp 
{
    [MessageHandle((uint) CMD.CMD_HEART_BEAT_RSP)]
    public static void CMD_HEART_BEAT_RSP(MsgData msg)
    {
        CMD_HEART_BEAT_RSP rsp = NetUtil.DeserializeMsg<CMD_HEART_BEAT_RSP>(msg);
    }
}
