using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartFrame : BaseFrameCommand,IFrameCommand
{
    public uint startTime;

    public FrameCommandID GetID()
    {
        return FrameCommandID.GameStart;
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
        inputData data = new inputData();
        {
            data.data1 = (uint)GetID();

            data.data2 = startTime;

            data.data3 = sendTime;
        }

        return data;
    }

    public void SetValue(uint frame , byte seat , inputData data)
    {
        this.frame = frame;

        this.seat = seat;

        startTime = data.data2;
    }

    public string GetString()
    {
        return "";
    }

    public void Reset()
    {

    }

}
