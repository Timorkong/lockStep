using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApplaction : MonoSingleton<GameApplaction>
{
    private bool mInit = false;

    public int playerSeat = -1;

    protected override void Awake()
    {
        base.Awake();

        this.InitSystem();
    }

    public void InitSystem()
    {
        InitBindSystem();

        InitClientSystem();

        Application.targetFrameRate = 30;

        mInit = true;
    }

    void InitBindSystem()
    {
        GameBindSystem.Instance.BindMessgeHandle();
    }

    void InitClientSystem()
    {
        ClientSystemManager.Initialize();

        switch (Global.Setting.startSystem)
        {
            case EnumClientSystem.Battle:
                {
                    ClientSystemManager.Instance.InitSystem<ClientSystemBattle>();
                    break;
                }
            case EnumClientSystem.Login:
                {
                    ClientSystemManager.Instance.InitSystem<ClientSystemLogin>();
                    break;
                }
            case EnumClientSystem.Town:
                {
                    ClientSystemManager.Instance.InitSystem<ClientSystemTown>();
                    break;
                }
        }
    }

    private void Update()
    {
        if (mInit == false) return;

        float deltaTime = Time.deltaTime;

        ClientSystemManager.Instance.Update(deltaTime);
    }

    private void LateUpdate()
    {
        
    }
}

