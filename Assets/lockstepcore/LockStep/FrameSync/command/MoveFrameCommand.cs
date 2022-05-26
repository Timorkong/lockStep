using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PROTOCOL_WAR;

public class MoveFrameCommand : BaseFrameCommand, IFrameCommand
{
    private PROTOCOL_WAR.CMD_WAR_MOVE cmd = null;

    public Cmd.ID.CMD CmdId
    {
        get
        {
            return Cmd.ID.CMD.CMD_WAR_MOVE;
        }
    }

    public MoveFrameCommand(MsgData msgData) : base(msgData)
    {
        cmd = NetUtil.DeserializeMsg<CMD_WAR_MOVE>(msgData);

        this.PlayerSeat = cmd.seat;

        if (Global.Setting.ShowNetWorkLog)
        {
            Debug.LogError(ToString());
        }
    }

    public void ExecCommand()
    {
        var beEntity = GetActorBySeat();

        if (beEntity != null)
        {
            beEntity.SetMoveDir(cmd.move_x, cmd.move_y);
        }
    }

    public string GetString
    {
        get
        {
            var ret = string.Format("收到战斗帧移动 seat = {0} move_x = {1} move_y = {2}", cmd.seat, cmd.move_x, cmd.move_y);

            return ret;
        }
    }
}

public class SyncSequenceCommand : BaseFrameCommand, IFrameCommand
{
    private PROTOCOL_WAR.CMD_WAR_SEQUENCE_NOTICE cmd = null;

    public Cmd.ID.CMD CmdId
    {
        get { return Cmd.ID.CMD.CMD_WAR_SEQUENCE_NOTICE; }
    }

    public SyncSequenceCommand(MsgData msgData) : base(msgData)
    {
        cmd = NetUtil.DeserializeMsg<CMD_WAR_SEQUENCE_NOTICE>(msgData);

        if (Global.Setting.ShowSequence)
        {
            Debug.LogError(ToString());
        }
    }

    public override uint Sequence => (uint)cmd.sequence;

    public void ExecCommand()
    {
        
    }

    public string GetString
    {
        get
        {
            var ret = string.Format("收到帧同步数据 sequence = {0} ", cmd.sequence);

            return ret;
        }
    }
}
