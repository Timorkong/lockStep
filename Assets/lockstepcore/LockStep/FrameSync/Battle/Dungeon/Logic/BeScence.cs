using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeScence
{
    public BeDungeon mBeDungeon = null;

    public BeEntity mainEntity = null;

    protected List<BeEntity> beEntities = new List<BeEntity>();

    protected Dictionary<int, BeEntity> dicSeat2Entity = new Dictionary<int, BeEntity>();

    public void AddEntity(BeEntity beEntity)
    {
        switch (beEntity.entityType)
        {
            case EntityType.Player:
                {
                    dicSeat2Entity.Add(beEntity.seat, beEntity);

                    break;
                }
        }

        beEntities.Add(beEntity);
    }

    public BeEntity GetEntityBySeat(int seat)
    {
        BeEntity ret = null;

        if(this.dicSeat2Entity.TryGetValue(seat , out ret))
        {
            
        }

        return ret;
    }

    public BeScence(BeDungeon dungeon)
    {
        this.mBeDungeon = dungeon;

        dungeon.beScence = this;
    }

    public void Update()
    {
        foreach(var entity in beEntities)
        {
            entity.Update();
        }
    }
}
