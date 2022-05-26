using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSystemLogin : ClientSystem
{
    string ip = "127.0.0.1";
    int port = 3333;

    public override void OnStart(SystemContent systemContent)
    {
        base.OnStart(systemContent);

        NetManager.Instance.Connect2Server(ip, port, 10000);
    }
}
