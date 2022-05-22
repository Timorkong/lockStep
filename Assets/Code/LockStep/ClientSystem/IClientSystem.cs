using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClientSystem
{
    string GetName();

    void SetName(string name);

    void OnEnter();

    void OnExit();

    void OnStart(SystemContent systemContent);

    void Update(float deltaTime);

    void BeforEnter();

    EnumClienSystemState curState
    {
        get;set;
    }
}
