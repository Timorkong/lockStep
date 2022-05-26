using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cmd.ID;
using PROTOCOL;
using ProtoBuf;
using PROTOCOL_WAR;

public partial class command_rsp
{
    [MessageHandle((uint)CMD.CMD_ENTER_GAME_RSP)]
    public static void CMD_ENTER_GAME_RSP(MsgData msg)
    {
        CMD_ENTER_GAME_RSP rsp = NetUtil.DeserializeMsg<CMD_ENTER_GAME_RSP>(msg);

        BattleMain.data = rsp.data;

        ClientSystemManager.Instance.SwitchSystem<ClientSystemBattle>();
    }

    [MessageHandle((uint)CMD.CMD_START_GAME_RSP)]
    public static void CMD_START_GAME_RSP(MsgData msg)
    {
        CMD_START_GAME_RSP rsp = NetUtil.DeserializeMsg<CMD_START_GAME_RSP>(msg);

        foreach(var user in rsp.room_info.user_list)
        {
            BattleMain.Instance.mBattle.CreateEntity(user.user_seat);
        }

        ClientSystemManager.Instance.CurrentSystem.curState = EnumClienSystemState.onTick;

        FrameSync.Instance.StartFrameSync(Global.Setting.SyncMode);

        Loading.Instance.Hide();

        InputManager.Instance.Show();
    }
}
