using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSystemBattle : ClientSystem
{
    public override void OnEnter()
    {
    }

    public override void OnStart(SystemContent systemContent)
    {

    }

    public override void OnExit()
    {

    }

    protected override void OnUpdate(float deltaTime)
    {
        BattleMain.Instance.Update();
    }
}
