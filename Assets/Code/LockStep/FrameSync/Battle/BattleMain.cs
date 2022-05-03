using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMain 
{
    protected static EnumBattleType mBattleType = EnumBattleType.None;

    private IBattle mBattle = null;

    protected static BattleMain mBattleMain = null;

    public static BattleMain Instance
    {
        get { return mBattleMain; }
    }

    protected void InitBattle(IBattle battle)
    {
        this.mBattle = battle;
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
        var deltaTime = (int)(Time.deltaTime *  GloableLogic.VALUE_1000);

        FrameSync.Instance.UpdateFrame();

        if(this.mBattle != null)
        {
            this.mBattle.Update(deltaTime);
        }
    }
}
