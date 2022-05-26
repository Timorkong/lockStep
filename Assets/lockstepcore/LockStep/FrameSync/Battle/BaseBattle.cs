using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBattle : IBattle, IUpdate
{
    protected EnumBattleType mBattleType = EnumBattleType.None;

    protected EnumSyncMode SyncMode = EnumSyncMode.None;

    public BeDungeon beDungeon = null;

    public GeDungeon geDungeon = null;

    public BeScence beScence = null;

    public GeScence geScence = null;

    public BeEntity mainBeEntity = null;

    public GeEntity mainGeEntity = null;

    public EnumBattleType GetBattleType
    {
        get { return mBattleType; }
    }

    public EnumSyncMode GetSyncMode()
    {
        return EnumSyncMode.None;
    }

    public BaseBattle(EnumBattleType battleType, EnumSyncMode syncMode)
    {
        this.mBattleType = battleType;

        this.SyncMode = syncMode;
    }

    public void InitBattle()
    {
        CreateDungeon();

        CreateScene();
    }

    public void CreateDungeon()
    {
        beDungeon = new BeDungeon(mBattleType);

        geDungeon = new GeDungeon(beDungeon);
    }

    public void CreateScene()
    {
        beScence = new BeScence(beDungeon);

        geScence = new GeScence(beScence, geDungeon);
    }

    public void CreateEntity(int seat)
    {
        BeEntity beEntity = new BeEntity(beScence , seat);

        GeEntity geEntity = new GeEntity(beEntity, geScence);

        if(seat == GameApplaction.Instance.playerSeat)
        {
            mainBeEntity = beEntity;

            mainGeEntity = geEntity;
        }
    }

    public virtual void UpdateLogic(int delta)
    {
        if (beDungeon != null) beDungeon.Update(delta);
    }

    public virtual void UpdateView(int delta)
    {
        if (geDungeon != null) geDungeon.Update();
    }
}
