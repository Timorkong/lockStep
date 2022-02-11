using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSystemBattle : ClientSystem
{
    public LogicWorld logicWorld;

    public override void OnEnter()
    {
        logicWorld = new LogicWorld();
    }

    public override void OnStart(SystemContent systemContent)
    {

    }

    public override void OnExit()
    {

    }

    protected override void OnUpdate(float deltaTime)
    {
        if(logicWorld != null)
        {
            logicWorld.Update(deltaTime);
        }
    }
}
