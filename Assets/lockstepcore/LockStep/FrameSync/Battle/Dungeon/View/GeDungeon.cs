using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeDungeon
{
    public BeDungeon beDungeon = null;

    public GeScence geScence = null;

    public BeEntity mainEntity = null;

    public GeDungeon(BeDungeon beDungeon)
    {
        this.beDungeon = beDungeon;
    }

    public void Update()
    {
        if (geScence != null) geScence.Update();
    }
}
