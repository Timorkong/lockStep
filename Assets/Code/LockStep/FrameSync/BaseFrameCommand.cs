using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFrameCommand 
{
    public uint frame;

    public byte seat = 0xFF;

    public uint sendTime;

    public byte GetSeat()
    {
        return seat;
    }

    public void GetActorBySeat(byte seatData)
    {

    }

    void Record(string cmd)
    {

    }
    public uint GetSendTime()
    {
        return sendTime;
    }
    protected void BaseReset()
    {
        frame = 0;

        seat = 0xFF;

        sendTime = 0;
    }

}
