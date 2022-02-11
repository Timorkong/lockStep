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
        if(CurrentSystem != null)
        {
            Debug.LogError("初始化系统，必须为空");
        }

        Type t = typeof(T);

        IClientSystem nextClientSystem = null;

        if(dClientSystem.TryGetValue(t.Name , out nextClientSystem))
        {
            CurrentSystem = nextClientSystem;
        }
        else
        {
            CurrentSystem = Activator.CreateInstance<T>() as IClientSystem;
            ClientSystem system = CurrentSystem as ClientSystem;
            system.SystemManager = this;
            system.SetName(t.Name);
            dClientSystem.Add(t.Name, CurrentSystem);
        }

        (CurrentSystem as ClientSystem).OnEnterSystem();
    }

    public void SwitchSystem<T>(SystemContent systemContent = null)  where T:class, IClientSystem
    {
        if(CurrentSystem != null && CurrentSystem.GetType() == typeof(T))
        {
            Debug.LogErrorFormat("【系统切换】 无法支持从 {0} -> {0}", typeof(T).Name);
            return;
        }

        if(TargetSystem != null)
        {
            Debug.LogErrorFormat("[系统切换] 上一个系统正在切换 {0}", null != TargetSystem ? TargetSystem.GetType().Name : "[invalid]");
            return;
        }

        Debug.LogError("开始切换系统");

        Type t = typeof(T);
        IClientSystem nextClientSystem = null;
        dClientSystem.TryGetValue(t.Name, out nextClientSystem);
        TargetSystem = nextClientSystem;
        if(TargetSystem == null)
        {
            TargetSystem = Activator.CreateInstance<T>() as IClientSystem;
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
            GameApplaction.instance.StartCoroutine(_SwitchSystemCoroutine(systemContent));
        }
    }

    IEnumerator _SwitchSystemCoroutine(SystemContent systemContent)
    {
        if (CurrentSystem != null) (CurrentSystem as ClientSystem).OnExitSystem();

        CurrentSystem = TargetSystem;

        TargetSystem = null;

        if (CurrentSystem != null) (CurrentSystem as ClientSystem).OnEnterSystem();

        if (CurrentSystem != null) (CurrentSystem as ClientSystem).OnStartSystem(systemContent);

        yield return null;
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
