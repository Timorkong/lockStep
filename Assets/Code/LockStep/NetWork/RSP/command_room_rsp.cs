using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cmd.ID;
using PROTOCOL;
using ProtoBuf;
using PROTOCOL_ROOM;

public partial class command_rsp 
{
    [MessageHandle((uint) CMD.CMD_ROOM_LIST_RSP)]
    public static void CMD_ROOM_LIST_RSP(MsgData msg)
    {
        CMD_ROOM_LIST_RSP rsp = NetUtil.DeserializeMsg<CMD_ROOM_LIST_RSP>(msg);

        RoomList.Instance.Refresh(rsp);
    }
}
