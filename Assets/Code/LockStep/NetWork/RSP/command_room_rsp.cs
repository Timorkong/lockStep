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

        GameApplaction.Instance.playerSeat = rsp.player_seat;

        if (Global.Setting.ShowNetWorkLog)
        {
            Debug.Log(string.Format("创建房间 player_seat = {0}", rsp.player_seat));
        }

        RoomInfo.Instance.Refresh(rsp.room_info);
    }

    [MessageHandle((uint)CMD.CMD_LEAVE_ROOM_RSP)]
    public static void CMD_LEAVE_ROOM_RSP(MsgData msg)
    {
        CMD_LEAVE_ROOM_RSP rsp = NetUtil.DeserializeMsg<CMD_LEAVE_ROOM_RSP>(msg);

        RoomList.Instance.Refresh(rsp.room_list);
    }

    [MessageHandle((uint)CMD.CMD_UPDATE_ROOM_INFO_NOTICE)]
    public static void CMD_UPDATE_ROOM_INFO_NOTICE(MsgData msg)
    {
        CMD_UPDATE_ROOM_INFO_NOTICE rsp = NetUtil.DeserializeMsg<CMD_UPDATE_ROOM_INFO_NOTICE>(msg);

        RoomInfo.Instance.Refresh(rsp.room_info);
    }

    [MessageHandle((uint)CMD.CMD_JOIN_ROOM_RSP)]
    public static void CMD_JOIN_ROOM_RSP(MsgData msg)
    {
        CMD_JOIN_ROOM_RSP rsp = NetUtil.DeserializeMsg<CMD_JOIN_ROOM_RSP>(msg);

        GameApplaction.Instance.playerSeat = rsp.player_seat;

        if (Global.Setting.ShowNetWorkLog)
        {
            Debug.Log(string.Format("加入房间 seat = {0}", rsp.player_seat));
        }

        RoomInfo.Instance.Refresh(rsp.room_info);
    }
}
