using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFrameCommand 
{
    public byte seat = 0xFF;

    private MsgData msgData = null;

    public BaseFrameCommand(MsgData msgData)
    {
        this.msgData = msgData;
    }

    public byte GetSeat()
    {
        return seat;
    }

    public void GetActorBySeat(byte seatData)
    {

    }
    protected void BaseReset()
    {
        seat = 0xFF;
    }
}
