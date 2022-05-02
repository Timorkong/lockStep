using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApplaction : MonoSingleton<GameApplaction>
{
    private bool mInit = false;

    public PlayerInfo playerInfo;

    public override void Init()
    {
        InitBindSystem();

        InitClientSystem();

        playerInfo = new PlayerInfo();

        Application.targetFrameRate = 30;

        mInit = true;
    }

    void InitBindSystem()
    {
        GameBindSystem.instance.BindMessgeHandle();
    }

    void InitClientSystem()
    {
        ClientSystemManager.Initialize();

        switch (Global.Setting.startSystem)
        {
            case EnumClientSystem.Battle:
                {
                    ClientSystemManager.instance.InitSystem<ClientSystemBattle>();
                    break;
                }
            case EnumClientSystem.Login:
                {
                    ClientSystemManager.instance.InitSystem<ClientSystemLogin>();
                    break;
                }
            case EnumClientSystem.Town:
                {
                    ClientSystemManager.instance.InitSystem<ClientSystemTown>();
                    break;
                }
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (mInit == false) return;

        float deltaTime = Time.deltaTime;

        ClientSystemManager.instance.Update(deltaTime);
    }

    private void LateUpdate()
    {
        
    }
}

