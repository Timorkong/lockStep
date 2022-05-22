using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMain 
{
    protected static EnumBattleType mBattleType = EnumBattleType.None;

    public BaseBattle mBattle = null;

    protected static BattleMain mBattleMain = null;

    public static PROTOCOL_COMMON.pre_battle_data data = null;

    public static BattleMain Instance
    {
        get { return mBattleMain; }
    }

    protected void InitBattle(BaseBattle battle)
    {
        this.mBattle = battle;

        mBattle.InitBattle();
    }

    public BattleMain(EnumBattleType type)
    {
        mBattleType = type;
    }

    public static BattleMain OpenBattle(EnumBattleType battleType , EnumSyncMode syncMode)
    {
        mBattleMain = new BattleMain(battleType);

        var battle = BattleFactory.Createbattle(battleType, syncMode);

        mBattleMain.InitBattle(battle);

        return mBattleMain;
    }

    public void Update()
    {
        FrameSync.Instance.UpdateFrame();
    }
}
