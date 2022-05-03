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
}