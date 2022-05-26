using System.IO;
using UnityEngine;

public class NetManager : MonoSingleton<NetManager>
{
    protected NetWorkSocket mSocket = null;

    protected bool isInited = false;

    public NetWorkSocket NetSocket
    {
        get
        {
            return this.mSocket;
        }
    }

    public NetManager()
    {
        this.mSocket = new NetWorkSocket();

        this.isInited = true;
    }

    public void Update()
    {
        if (isInited == false)
        {
            return;
        }

        this.mSocket.Tick();

        NetProcess.Instance.Tick(0);
    }

    public void Connect2Server(string ip, int port, int timeout)
    {
        this.mSocket.ConnectToServer(ip, port, timeout);
    }

    public void DisConnect()
    {
        if (this.isInited == false) return;

        this.mSocket.DisConnect();
    }

    public int SendMsg<T>(T msg, Cmd.ID.CMD cmd, bool bSyncFrame = false)
    {
        int ret = -1;

        byte[] bytes = null;

        try
        {
            using (MemoryStream stream = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<T>(stream, msg);

                bytes = stream.ToArray();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("send message erro = " + e.Message);
        }


        ret = mSocket.SendData((uint)cmd, bytes, bytes.Length, 1000, null, bSyncFrame);

        return ret;
    }


    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();

        this.DisConnect();
    }
}
