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
        req.data = new PROTOCOL_COMMON.pre_battle_data();
        req.data.level_name = "Level1";
        NetManager.Instance.SendMsg(req, Cmd.ID.CMD.CMD_ENTER_GAME_REQ);
    }

    public static void CMD_START_GAME_REQ()
    {
        CMD_START_GAME_REQ req = new CMD_START_GAME_REQ();
        NetManager.Instance.SendMsg(req, Cmd.ID.CMD.CMD_START_GAME_REQ);
    }

    public static void CMD_WAR_MOVE_REQ(float x, float y)
    {
        CMD_WAR_MOVE req = new CMD_WAR_MOVE();

        if (BattleMain.Instance.mBattle.mainBeEntity != null)
        {
            req.seat = BattleMain.Instance.mBattle.mainBeEntity.seat;

            req.move_x = x;

            req.move_y = y;

            NetManager.Instance.SendMsg(req, Cmd.ID.CMD.CMD_WAR_MOVE);
        }

    }
}