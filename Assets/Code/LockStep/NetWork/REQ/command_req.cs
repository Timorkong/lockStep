using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using PROTOCOL;

public partial class command_req
{
    public static void CMD_HEART_BEAT_REQ()
    {
        CMD_HEART_BEAT_REQ req = new CMD_HEART_BEAT_REQ();

        req.id = 0;

        var sendLen = NetManager.instance.SendMsg<CMD_HEART_BEAT_REQ>(req, Cmd.ID.CMD.CMD_HEART_BEAT_REQ);

        Debug.LogError("send len = " + sendLen.ToString());
    }
}