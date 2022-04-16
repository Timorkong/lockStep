using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorkSocket 
{
    protected bool isInited = false;

    protected NetWorkBase mNetWorkBase = null;

    protected string serverIp = "";

    protected int port = 0;

    public NetWorkSocket()
    {
        mNetWorkBase = new NetWorkBase();

        this.isInited = true;
    }

    public void ConnectToServer(string serverIp, int port, int maxTimeOut)
    {
        this.serverIp = serverIp;

        this.port = port;
        
        this.mNetWorkBase.Connect(serverIp, port, maxTimeOut);
    }
}
