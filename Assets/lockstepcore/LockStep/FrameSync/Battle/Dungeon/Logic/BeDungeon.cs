using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeDungeon
{
    EnumBattleType battleType = EnumBattleType.None;

    public BeScence beScence = null;

    public BeDungeon(EnumBattleType battleType)
    {
        this.battleType = battleType;
    }

    public void Init()
    {
    }


    public void Update(float deltaTime)
    {
        if (this.beScence != null) beScence.Update();
    }
}
