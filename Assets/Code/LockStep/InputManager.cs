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

            if(FrameSync.Instance.nDegree != nDegree || FrameSync.Instance.bInMoveMode != bInMoving)
            {
                FrameSync.Instance.FirtFrameCommand(cmd);

                FrameSync.Instance.nDegree = nDegree;

                FrameSync.Instance.bInMoveMode = bInMoving;
            }
        }
        else
        {
            if(FrameSync.Instance.bInMoveMode != bInMoving)
            {
                StopFrameCommand cmd = new StopFrameCommand();

                FrameSync.Instance.FirtFrameCommand(cmd);

                FrameSync.Instance.bInMoveMode = bInMoving;
            }
        }
    }
}
