using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : SingleWindow<Login>
{
    public void OnClick()
    {
        Debug.LogError("Swithch Battle system");

        ClientSystemManager.Instance.SwitchSystem<ClientSystemBattle>();
    }

    public void OnClickDisConnect()
    {
        Debug.LogError("On Clic kDisConnect");

        NetManager.Instance.DisConnect();
    }

    public void OnClickBinding()
    {
        GameBindSystem.Instance.BindMessgeHandle();
    }

    public void OnClickSendTick()
    {
        command_req.CMD_HEART_BEAT_REQ();
    }

    public void OnClickEnterRoom()
    {
        Hide();
        RoomList.Instance.Show();
    }
}
