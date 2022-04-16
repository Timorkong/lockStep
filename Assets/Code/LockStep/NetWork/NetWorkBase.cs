using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;

public delegate void NetWorkStatusCallBack(bool isDone, string errInfo);

public delegate void NetWorkReceiveCallBack(bool isDone, int boteSize, string errInfo);

public class NetWorkBase
{
    public enum NET_MANAGER_STATUS
    {
        NONE,//初始状态
        CONNECTED,//链接状态
        CONNECTING,
    }

    protected bool isInited = false;

    protected Socket client = null;

    protected NET_MANAGER_STATUS status = NET_MANAGER_STATUS.NONE;

    protected void SetNetManagerState(NET_MANAGER_STATUS status)
    {
        this.status = status;
    }

    protected NetWorkStatusCallBack connCB = null;

    protected NetWorkStatusCallBack sendCB = null;

    protected NetWorkStatusCallBack receiveCB = null;

    protected ManualResetEvent connectDone = new ManualResetEvent(false);

    public NetWorkBase()
    {
        if (this.isInited) return;

        this.isInited = true;

        this.SetNetManagerState(NET_MANAGER_STATUS.NONE);
    }

    public void Connect(string adress, int port, int maxTimeOut, NetWorkStatusCallBack cb = null)
    {
        this.connCB = cb;

        try
        {
            IPAddress ip = IPAddress.Parse(adress);

            IPEndPoint iEP = new IPEndPoint(ip, port);

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            connectDone.Reset();

            client.BeginConnect(iEP, new AsyncCallback(ConnectCallBack), client);

            SetNetManagerState(NET_MANAGER_STATUS.CONNECTING);

            if (connectDone.WaitOne(maxTimeOut, false))
            {
                SetNetManagerState(NET_MANAGER_STATUS.CONNECTED);

                if (this.connCB != null)
                {
                    this.connCB(true, "");
                }
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    protected void ConnectCallBack(IAsyncResult resualt)
    {
        if (isInited == false) return;

        if (client == null) return;

        var socket = (Socket)resualt.AsyncState;

        if (socket != null)
        {
            socket.EndConnect(resualt);

            SetNetManagerState(NET_MANAGER_STATUS.CONNECTED);

            connectDone.Set();


        }
    }

    public void Send(byte[] datas , int offset , int bufflen , NetWorkStatusCallBack cb = null)
    {
        this.sendCB = cb;

        if(isInited == false)
        {
            if(sendCB != null)
            {
                sendCB(false, "为初始化");
            }

            return;
        }

        try
        {
            if(client == null)
            {
                if(this.sendCB != null)
                {
                    sendCB(false, "为初始化");
                }
                return;
            }

            if(this.status != NET_MANAGER_STATUS.CONNECTED)
            {
                if(sendCB != null)
                {
                    sendCB(false, "为初始化");
                }
                return;
            }

            SocketError socketError ;

            client.BeginSend(datas, offset, bufflen, SocketFlags.None, out socketError, new AsyncCallback(sendCallBack), client);
        }

        catch(System.Exception e)
        {

        }

    }

    private void sendCallBack(IAsyncResult ar)
    {
        if(isInited == false || client == null || this.status != NET_MANAGER_STATUS.CONNECTED)
        {
            if(sendCB != null)
            {
                sendCB(false, "为初始化");
            }
            return;
        }

        var socket = (Socket)ar.AsyncState;
        var errorCode = SocketError.Success;
        var sendSize = this.client.EndSend(ar, out errorCode);

        if(errorCode != SocketError.Success)
        {
            sendCB(false, "发送失败");
            return;
        }

        if(sendCB != null)
        {
            sendCB(true, "");
        }
    }

    public void Receives(byte[] datas , int offset , int size , NetWorkStatusCallBack cb = null)
    {
        this.receiveCB = cb;

        if(isInited == false || client == null || status != NET_MANAGER_STATUS.CONNECTED)
        {
            if(this.receiveCB != null)
            {
                this.receiveCB(false, "为初始化");
            }

            return;
        }

        client.BeginReceive(datas, offset, size, SocketFlags.None, new AsyncCallback(receiveCallBack), this.client);
    }

    private void receiveCallBack(IAsyncResult ar)
    {
        if(isInited == false || client == null || status != NET_MANAGER_STATUS.CONNECTED)
        {
            if (this.receiveCB != null)
            {
                this.receiveCB(false, "为初始化");
            }

            return;
        }

        var socket = (Socket)ar.AsyncState;

        SocketError socketError = SocketError.Success;

        var receiveSize = client.EndReceive(ar, out socketError);

        if(socketError != SocketError.Success)
        {
            if (this.receiveCB != null)
            {
                this.receiveCB(false, "接收失败");
            }

            return;
        }

        if (this.receiveCB != null)
        {
            this.receiveCB(true, "");
        }
    }
}
