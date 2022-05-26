using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeEntity
{
    public EntityType entityType = EntityType.Player;

    public BeScence beScence = null;

    public float speed = 1;

    public Vector3 moveDir = new Vector3();

    public Vector3 pos = new Vector3();

    public int seat = 0;

    public BeEntity(BeScence beScence, int seat)
    {
        this.beScence = beScence;

        this.seat = seat;

        beScence.AddEntity(this);
    }

    public void SetMoveDir(float x , float y)
    {
        moveDir.x = x;

        moveDir.z = y;

        moveDir.Normalize();
    }

    public void Update()
    {
        pos += moveDir * speed;
    }
}
