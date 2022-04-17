using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoSingleton<NetManager>
{
    protected NetWorkSocket mSocket = null;

    protected bool isInited = false;

    public NetManager()
    {
        this.mSocket = new NetWorkSocket();

        this.isInited = true;
    }

    public void Update()
    {
        if(isInited == false)
        {
            return;
        }

        this.mSocket.Tick();
    }

    public void Connect2Server(string ip , int port,int timeout)
    {
        this.mSocket.ConnectToServer(ip, port, timeout);
    }

    public int SendCommad<CommandType>(CommandType cmd) where CommandType:IProtocolStream , IGetMsgID
    {
        int ret = -1;

        ret = mSocket.SendCommand<CommandType>(cmd);

        return ret;
    }
}
