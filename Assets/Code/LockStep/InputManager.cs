using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public const short DegreeDiv = 15;

    public void Update(float deltaTime)
    {
        _updateJoystick(deltaTime);
    }

    private void _updateJoystick(float deltaTime)
    {
        float mx = Input.GetAxisRaw("Horizontal");

        float my = Input.GetAxisRaw("Vertical");

        bool bInMoving = false;

        short nDegree = 0;

        if (Mathf.Abs(mx) > GlobalLogic.Value_0 || Mathf.Abs(my) > GlobalLogic.Value_0)
        {
            bInMoving = true;

            float l = Mathf.Sqrt(mx * mx + my * my);

            float redians = Mathf.Acos(mx / l);

            nDegree = (short)(Mathf.Rad2Deg * redians);

            if(my < 0)
            {
                nDegree = (short)(360 - nDegree);
            }

            nDegree /= DegreeDiv;
        }

        if (bInMoving)
        {
            MoveFrameCommand cmd = new MoveFrameCommand()
            {
                degree = nDegree,
                run = false,
            };

            if(FrameSync.instance.nDegree != nDegree || FrameSync.instance.bInMoveMode != bInMoving)
            {
                FrameSync.instance.FirtFrameCommand(cmd);

                FrameSync.instance.nDegree = nDegree;

                FrameSync.instance.bInMoveMode = bInMoving;
            }
        }
        else
        {
            if(FrameSync.instance.bInMoveMode != bInMoving)
            {
                StopFrameCommand cmd = new StopFrameCommand();

                FrameSync.instance.FirtFrameCommand(cmd);

                FrameSync.instance.bInMoveMode = bInMoving;
            }
        }
    }
}
