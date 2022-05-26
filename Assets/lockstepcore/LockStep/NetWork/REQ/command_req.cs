using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using PROTOCOL;

public partial class command_req
{
    static int id = 0;

    public static void CMD_HEART_BEAT_REQ()
    {
        CMD_HEART_BEAT_REQ req = new CMD_HEART_BEAT_REQ();

        req.id = id ++;

        req.name = "fdafdafdafd";

        var sendLen = NetManager.Instance.SendMsg<CMD_HEART_BEAT_REQ>(req, Cmd.ID.CMD.CMD_HEART_BEAT_REQ);
    }
}