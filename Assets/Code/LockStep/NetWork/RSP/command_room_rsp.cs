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

        RoomList.Instance.Refresh(rsp.room_list);
    }

    [MessageHandle((uint)CMD.CMD_CREATE_ROOM_RSP)]
    public static void CMD_CREATE_ROOM_RSP(MsgData msg)
    {
        CMD_CREATE_ROOM_RSP rsp = NetUtil.DeserializeMsg<CMD_CREATE_ROOM_RSP>(msg);

        RoomInfo.Instance.Refresh(rsp.room_info);
    }

    [MessageHandle((uint)CMD.CMD_LEAVE_ROOM_RSP)]
    public static void CMD_LEAVE_ROOM_RSP(MsgData msg)
    {
        CMD_LEAVE_ROOM_RSP rsp = NetUtil.DeserializeMsg<CMD_LEAVE_ROOM_RSP>(msg);

        RoomList.Instance.Refresh(rsp.room_list);
    }

    [MessageHandle((uint)CMD.CMD_LEAVE_ROOM_NOTICE)]
    public static void CMD_LEAVE_ROOM_NOTICE(MsgData msg)
    {
        CMD_LEAVE_ROOM_NOTICE rsp = NetUtil.DeserializeMsg<CMD_LEAVE_ROOM_NOTICE>(msg);

        RoomInfo.Instance.Refresh(rsp.room_info);
    }
}
