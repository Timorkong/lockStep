using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ClientSystemBattle : ClientSystem
{
    public override void OnEnter()
    {
        BattleMain.OpenBattle(EnumBattleType.DunGeon, EnumSyncMode.SyncFrame);
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
