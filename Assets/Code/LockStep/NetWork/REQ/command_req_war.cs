using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using PROTOCOL;
using PROTOCOL_WAR;

public partial class command_req
{

    public static void CMD_ENTER_GAME_REQ()
    {
        CMD_ENTER_GAME_REQ req = new CMD_ENTER_GAME_REQ();
        NetManager.Instance.SendMsg(req, Cmd.ID.CMD.CMD_ENTER_GAME_REQ);
    }
}