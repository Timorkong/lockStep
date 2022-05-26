using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFactory 
{
    public static BaseBattle Createbattle(EnumBattleType battleType , EnumSyncMode syncMode)
    {
        BaseBattle ret = null;
    
        switch (battleType)
        {
            case EnumBattleType.None:
                {
                    break;
                }
            case EnumBattleType.DunGeon:
                {
                    ret = new PVEBattle(battleType , syncMode);
                    break;
                }
        }
        return ret;
    }
}
