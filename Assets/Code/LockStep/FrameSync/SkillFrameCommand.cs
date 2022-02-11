using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFrameCommand : BaseFrameCommand , IFrameCommand
{
    public uint skillSolt;
    public uint skillSlotUp = 0;
       
    public FrameCommandID GetID()
    {
        return FrameCommandID.Skill;
    }

    public uint GetFrame()
    {
        return frame;
    }

    public inputData GetInputData()
    {
        inputData data = new inputData()
        {
            data1 = (uint)FrameCommandID.Skill,
            data2 = skillSolt,
            data3 = skillSlotUp,
            sendTime = sendTime,
        };

        return data;
    }

    public void ExecCommand()
    {

    }

    public void SetValue(uint frame , byte seat , inputData data)
    {
        this.frame = frame;
        this.seat = seat;
        this.skillSolt = data.data2;
        this.skillSlotUp = data.data3;
    }

    public string GetString()
    {
        return "";
    }

    public void Reset()
    {
        BaseReset();
        skillSolt = 0;
        skillSlotUp = 0;
    }
}
