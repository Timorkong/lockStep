using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCommandFactory { 
    public static IFrameCommand CreateCommand(uint type)
    {
        FrameCommandID id = (FrameCommandID)type;

        IFrameCommand ret = null;

        switch (id)
        {
            case FrameCommandID.GameStart:
                {
                    ret = new GameStartFrame();
                    break;
                }
            case FrameCommandID.Move:
                {
                    ret = new MoveFrameCommand();
                    break;
                }
            case FrameCommandID.Skill:
                {
                    ret = new SkillFrameCommand();
                    break;
                }
            case FrameCommandID.Stop:
                {
                    ret = new StopFrameCommand();
                    break;
                }
        }

        return ret;
    }
}
