using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBattle : IBattle
{
    protected EnumBattleType mBattleType = EnumBattleType.None;

    protected EnumSyncMode SyncMode = EnumSyncMode.None;

    public EnumBattleType GetBattleType
    {
        get { return mBattleType; }
    } 

    public EnumSyncMode GetSyncMode()
    {
        return EnumSyncMode.None;
    }

    public BaseBattle(EnumBattleType battleType , EnumSyncMode syncMode)
    {
        this.mBattleType = battleType;
        this.SyncMode = syncMode;
    }

    public void Update(int delta)
    {

    }

    public void FrameUpdate(int delta)
    {

    }
}
