using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using PROTOCOL;
using PROTOCOL_ROOM;

public partial class command_req
{
    public static void CMD_ROOM_LIST_REQ()
    {
        CMD_ROOM_LIST_REQ req = new CMD_ROOM_LIST_REQ();

        var sendLen = NetManager.Instance.SendMsg(req, Cmd.ID.CMD.CMD_ROOM_LIST_REQ);
    }

    public static void CMD_CREATE_ROOM_REQ()
    {
        CMD_CREATE_ROOM_REQ req = new CMD_CREATE_ROOM_REQ();

        req.room_name = "default player enter room";

        NetManager.Instance.SendMsg(req, Cmd.ID.CMD.CMD_CREATE_ROOM_REQ);
    }

    public static void CMD_LEAVE_ROOM_REQ()
    {
        CMD_LEAVE_ROOM_REQ req = new CMD_LEAVE_ROOM_REQ();

        NetManager.Instance.SendMsg(req, Cmd.ID.CMD.CMD_LEAVE_ROOM_REQ);
    }

    public static void CMD_JOIN_ROOM_REQ(int room_unique_id)
    {
        CMD_JOIN_ROOM_REQ req = new CMD_JOIN_ROOM_REQ();

        req.room_unquie_id = room_unique_id;

        NetManager.Instance.SendMsg(req, Cmd.ID.CMD.CMD_JOIN_ROOM_REQ);
    }
}