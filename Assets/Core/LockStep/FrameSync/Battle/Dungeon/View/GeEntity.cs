using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeEntity
{
    public BeEntity beEntity = null;

    public GeScence geScence = null;

    public GameObject model = null;

    public GeEntity(BeEntity beEntity, GeScence geScence)
    {
        this.beEntity = beEntity;

        this.geScence = geScence;

        geScence.geEntities.Add(this);

        var go = Resources.Load<GameObject>("Char/Char1/Char1");

        model = GameObject.Instantiate(go);

        model.transform.localPosition = beEntity.pos;
    }

    public void Update()
    {
        if (model == null) return;

        model.transform.localPosition = beEntity.pos;
    }
}
