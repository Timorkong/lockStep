using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFrameCommand : BaseFrameCommand , IFrameCommand
{
    public short degree;
    public bool run;

    public FrameCommandID GetID()
    {
        return FrameCommandID.Move;
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
            data1 = (uint)FrameCommandID.Move,

            data2 = (uint)degree,

            data3 = (uint) (run ? 1 : 0),

            sendTime = sendTime,
        };

        return data;
    }

    public void SetValue(uint frame , byte seat , inputData data)
    {
        this.frame = frame;

        this.seat = seat;

        this.degree = (short)(data.data2);

        this.run = data.data3 == 1;
    }

    public string GetString()
    {
        return "";
    }

    public void Reset()
    {
        BaseReset();
        degree = 0;
        run = false;
    }

}
