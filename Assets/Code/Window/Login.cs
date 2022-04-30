using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoSingleton<Login>
{
    public void OnClick()
    {
        Debug.LogError("Swithch Battle system");

        ClientSystemManager.instance.SwitchSystem<ClientSystemBattle>();
    }

    public void OnClickDisConnect()
    {
        Debug.LogError("On Clic kDisConnect");

        NetManager.instance.DisConnect();
    }

    public void OnClickBinding()
    {
        GameBindSystem.instance.BindMessgeHandle();
    }

    public void OnClickSendTick()
    {
        command_req.CMD_HEART_BEAT_REQ();
    }
}
