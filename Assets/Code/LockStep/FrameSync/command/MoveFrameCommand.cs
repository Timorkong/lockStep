using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PROTOCOL_WAR;

public class MoveFrameCommand : BaseFrameCommand, IFrameCommand
{
    public float move_x = 0;

    public float move_y = 0;

    private PROTOCOL_WAR.CMD_WAR_MOVE cmd = null;

    public Cmd.ID.CMD GetID()
    {
        return Cmd.ID.CMD.CMD_WAR_MOVE;
    }


    public MoveFrameCommand(MsgData msgData) : base(msgData)
    {
        cmd = NetUtil.DeserializeMsg<CMD_WAR_MOVE>(msgData);

        if (Global.Setting.ShowNetWorkLog)
        {
            Debug.LogError(GetString());
        }
    }

   
    public void ExecCommand()
    {

    }

    public string GetString()
    {
        var ret = string.Format("收到战斗帧移动 seat = {0} move_x = {1} move_y = {2}", cmd.seat, cmd.move_x, cmd.move_y);

        return ret;
    }
}

public class SyncSequenceCommand : BaseFrameCommand, IFrameCommand
{
    private PROTOCOL_WAR.CMD_WAR_SEQUENCE_NOTICE cmd = null;

    public Cmd.ID.CMD GetID()
    {
        return Cmd.ID.CMD.CMD_WAR_SEQUENCE_NOTICE;
    }

    public SyncSequenceCommand(MsgData msgData) : base(msgData)
    {
        cmd = NetUtil.DeserializeMsg<CMD_WAR_SEQUENCE_NOTICE>(msgData);

        if (Global.Setting.ShowSequence)
        {
            Debug.LogError(GetString());
        }
    }

    public void ExecCommand()
    {

    }

    public string GetString()
    {
        var ret = string.Format("收到帧同步数据 sequence = {0} ", cmd.sequence);

        return ret;
    }
}
