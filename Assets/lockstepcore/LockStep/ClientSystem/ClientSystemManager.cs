using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EnumClientSystem
{
    Login = 0,
    Town = 1,
    Battle = 2,
}

public class SystemContent
{
    public delegate void OnStart();
    public OnStart onStart;
}

public class ClientSystemManager : Singleton<ClientSystemManager>
{
    protected Dictionary<string, IClientSystem> dClientSystem = new Dictionary<string, IClientSystem>();

    public IClientSystem CurrentSystem { get; set; }

    public IClientSystem TargetSystem { get; set; }

    public void InitSystem<T>(params object[] userData) where T : class, IClientSystem
    {
        this.SwitchSystem<T>();
    }

    public void SwitchSystem<T>(SystemContent systemContent = null)  where T:class, IClientSystem
    {
        if(CurrentSystem != null && CurrentSystem.GetType() == typeof(T))
        {
            Debug.LogErrorFormat("切换system失败{0} -> {0}", typeof(T).Name);
            return;
        }

        if(TargetSystem != null)
        {
            Debug.LogErrorFormat("[切换sytem] 目标{0}", null != TargetSystem ? TargetSystem.GetType().Name : "[invalid]");
            return;
        }

        Type t = typeof(T);
        IClientSystem nextClientSystem = null;
        dClientSystem.TryGetValue(t.Name, out nextClientSystem);
        TargetSystem = nextClientSystem;
        if(TargetSystem == null)
        {
            TargetSystem = Activator.CreateInstance<T>();
            ClientSystem system = TargetSystem as ClientSystem;
            system.SystemManager = this;
            system.SetName(t.Name);
            dClientSystem.Add(t.Name, TargetSystem);
        }

        _onChangeClear();

        Debug.LogFormat("current system: {0}", CurrentSystem == null ? "null" : CurrentSystem.GetName());
        Debug.LogFormat("target system: {0}", TargetSystem == null ? "null" : TargetSystem.GetName());

        if(Global.Setting.startSystem == EnumClientSystem.Battle)
        {

        }

        if(CurrentSystem != TargetSystem)
        {
            if(TargetSystem != null)
            {
                TargetSystem.BeforEnter();
            }

            if (CurrentSystem != null) (CurrentSystem as ClientSystem).OnExitSystem();

            CurrentSystem = TargetSystem;

            TargetSystem = null;

            if (CurrentSystem != null) (CurrentSystem as ClientSystem).OnEnterSystem();

            if (CurrentSystem != null) (CurrentSystem as ClientSystem).OnStartSystem(systemContent);
        }
    }

    private void _onChangeClear()
    {

    }

    public void Update(float deltaTime)
    {
        if(CurrentSystem != null)
        {
            CurrentSystem.Update(deltaTime);
        }

        if(TargetSystem != null)
        {
            TargetSystem.Update(deltaTime);
        }
    }
}
