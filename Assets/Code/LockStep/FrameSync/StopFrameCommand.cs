using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopFrameCommand : BaseFrameCommand , IFrameCommand
{
    public FrameCommandID GetID()
    {
        return FrameCommandID.Stop;
    }

    public uint GetFrame()
    {
        return frame;
    }

    public void ExecCommand()
    {

    }

    public inputData GetInputData()
    {
        inputData data = new inputData()
        {
            data1 = (uint)FrameCommandID.Stop,
            data2 = 0,
            data3 = 0,
            sendTime = sendTime,
        };
        return data;
    }

    public void SetValue(uint frame , byte seat , inputData data)
    {
        this.frame = frame;

        this.seat = seat;
    }

    public string GetString()
    {
        return "";
    }

    public void Reset()
    {
        BaseReset();
    }
}
