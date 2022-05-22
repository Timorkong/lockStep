using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumClienSystemState
{
    onNone,
    onInit,
    onEnter,
    onTick,
    onExit,
    onEnd,
    onError,
}

public class ClientSystem : IClientSystem
{
    /// <summary>
    /// ��ǰ״̬
    /// </summary>
    protected EnumClienSystemState mCurState = EnumClienSystemState.onNone;
    /// <summary>
    /// �ϴ�״̬
    /// </summary>
    protected EnumClienSystemState mLastState = EnumClienSystemState.onNone;

    protected string mName = "";

    public EnumClienSystemState curState
    {
        get
        {
            return mCurState;
        }
        set
        {
            mCurState = value;
        }
    }
    public EnumClienSystemState lastState
    {
        get
        {
            return mLastState;
        }
        set
        {
            mLastState = value;  
        }
    }

    public ClientSystemManager SystemManager
    {
        set; get;
    }

    public string GetName()
    {
        return mName;
    }

    public void SetName(string name)
    {
        this.mName = name;
    }

    private bool m_bStart = false;

    public bool BStart
    {
        get
        {
            return m_bStart;
        }
        set
        {
            m_bStart = value;
        }
    }

    public void OnEnterSystem()
    {
        if(curState != EnumClienSystemState.onError)
        {
            curState = EnumClienSystemState.onEnter;

            OnEnter();
        }
    }

    public virtual void OnEnter()
    {

    }

    public void OnExitSystem()
    {
        curState = EnumClienSystemState.onExit;

        OnExit();
    }

    public virtual void OnExit()
    {

    }

    public void OnStartSystem(SystemContent systemContent)
    {
        BStart = true;
        OnStart(systemContent);
    }

    public virtual void OnStart(SystemContent systemContent)
    {

    }

    public void Update(float deltaTime)
    {
        if(curState == EnumClienSystemState.onTick)
        {
            OnUpdate(deltaTime);
        }
    }

    protected virtual void OnUpdate(float deltaTime)
    {

    }

    public virtual void BeforEnter()
    {

    }
}
