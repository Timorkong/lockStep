using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicWorld 
{
    public LogicRoom logicRoom;

    public InputManager inputManager;

    public uint frame = 0;

    public void Init()
    {
        inputManager = new InputManager();

        logicRoom = new LogicRoom();

        FrameSync.instance.SetLogicWorld(this);
    }

    public void Update(float deltaTime)
    {
        if(inputManager != null)
        {
            inputManager.Update(deltaTime);
        }

        FrameSync.instance.UpdateFrame();

        if(logicRoom!= null)
        {
            logicRoom.Update(deltaTime);
        }
    }
}
