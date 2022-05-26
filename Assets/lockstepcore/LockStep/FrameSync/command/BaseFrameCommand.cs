using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFrameCommand 
{
    private int seat = 0xFF;

    private MsgData msgData = null;

    private BeEntity beEntity = null;

    public BaseFrameCommand(MsgData msgData)
    {
        this.msgData = msgData;
    }

    public virtual int PlayerSeat
    {
        get { return seat; }
        set { this.seat = value; }
    }

    public virtual uint Sequence
    {
        get { return 0; }
    }

    public BeEntity GetActorBySeat()
    {
        beEntity = BattleMain.Instance.mBattle.beScence.GetEntityBySeat(PlayerSeat);

        return this.beEntity;
    }

    protected void BaseReset()
    {
        seat = 0xFF;
    }
}
