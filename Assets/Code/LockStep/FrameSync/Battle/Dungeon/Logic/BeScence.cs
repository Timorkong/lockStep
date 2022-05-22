using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeScence
{
    public BeDungeon mBeDungeon = null;

    public BeEntity mainEntity = null;

    public List<BeEntity> beEntities = new List<BeEntity>();

    public BeScence(BeDungeon dungeon)
    {
        this.mBeDungeon = dungeon;

        dungeon.beScence = this;
    }

    public void Update()
    {
        if (mainEntity != null) mainEntity.Update();
    }
}
