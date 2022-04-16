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
}
