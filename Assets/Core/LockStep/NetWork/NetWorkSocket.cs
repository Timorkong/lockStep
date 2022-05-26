using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PushNetErrorCallBack(int errorCode, int errorInfo);

public class NetWorkSocket
{
    protected bool isInited = false;

    protected NetWorkBase mNetWorkBase = null;

    protected NetOutputBuffer mOutputBuffer = null;

    protected NetInputBuffer mInputBuffer = null;

    protected PackBuffer mPackBuffer = null;

    protected string serverIp = "";

    protected int port = 0;

    protected byte[] mBuffer = null;
    /// <summary>
    /// 接收到到消息长度
    /// </summary>
    protected int mReceiveSize = 0;
    /// <summary>
    /// 当前包大小
    /// </summary>
    protected int mCurrentPackSize = 0;


    public NetWorkSocket()
    {
        mNetWorkBase = new NetWorkBase();

        mOutputBuffer = new NetOutputBuffer(mNetWorkBase);

        mInputBuffer = new NetInputBuffer();

        mBuffer = new byte[RingBuffer.DefaultSize];

        mPackBuffer = new PackBuffer();

        this.isInited = true;
    }

    public void ConnectToServer(string serverIp, int port, int maxTimeOut)
    {
        this.serverIp = serverIp;

        this.port = port;

        mOutputBuffer.Reset();

        mInputBuffer.Reset();

        this.mNetWorkBase.Connect(serverIp, port, maxTimeOut, ConnectCallBack);

        this.StartReceive();
    }

    public void ConnectCallBack(bool isDone, string errInfo)
    {
        if (Global.Setting.ShowNetWorkLog)
            Debug.Log(string.Format("connect isDone = {0} errInfo = {1}", isDone, errInfo));
    }

    public void StartReceive()
    {
        this.mNetWorkBase.Receives(mInputBuffer.Buffer, mInputBuffer.Tail, mInputBuffer.FreeLenth, ReceiveCallBack);
    }

    public void ReceiveCallBack(bool isDone, int receiveSize, string errInfo)
    {
        if (isDone)
        {
            try
            {
                this.mReceiveSize += receiveSize;

                mInputBuffer.UpdateTail(receiveSize);
                while (mReceiveSize > 0)
                {
                    if (mReceiveSize >= (int)NET_DEFINE.HEAD_SIZE)
                    {
                        mCurrentPackSize = mInputBuffer.CurrentPackLenth;
                    }

                    if (mReceiveSize >= mCurrentPackSize)
                    {
                        int packSize = this.ProcessCommand();

                        this.mReceiveSize -= packSize;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }

            this.StartReceive();
        }
    }
    string debugBytes = "";
    protected int ProcessCommand()
    {
        if (Global.Setting.ShowNetWorkLog) debugBytes = mInputBuffer.ToString();

        int msgSize = mInputBuffer.ReadInt32(-1, true);

        uint msgId = mInputBuffer.ReadUint32(-1, true);

        MsgData msgData = new MsgData(msgSize);

        msgData.msgID = msgId;

        mInputBuffer.Read(msgData.msg, msgSize);

        if (Global.Setting.ShowNetWorkLog)
        {
            Debug.Log(string.Format("receive msg : size = {0} msgId = {1} msg = {2}", msgSize, msgId, debugBytes));
        }
        //战斗帧消息
        if (isFrameSyncCmd((Cmd.ID.CMD)msgId))
        {
            FrameSync.Instance.PushNetCommand(msgData);
        }
        else//系统消息
        {
            NetProcess.Instance.Push(msgData);
        }
        return msgSize + (int)NET_DEFINE.HEAD_SIZE;
    }

    private bool isFrameSyncCmd(Cmd.ID.CMD cmd)
    {
        if (cmd > Cmd.ID.CMD.CMD_FRAME_SYNC_MIN && cmd < Cmd.ID.CMD.CMD_FRAME_SYNC_MAX) return true;
        return false;
    }

    public int SendData(uint msgId,byte[] msgBytes, int msgLen, int timeOut, PushNetErrorCallBack cb = null,bool bSyncFrame = false)
    {
        if (mNetWorkBase.Status != NetWorkBase.NET_MANAGER_STATUS.CONNECTED)
        {
            Debug.LogError("链接未创建");

            return -1;
        }

        if (this.isInited == false)
        {
            Debug.LogError("未初始化");

            return -2;
        }


        mOutputBuffer.WriteUint((uint)msgLen, bSyncFrame);

        mOutputBuffer.WriteUint(msgId, bSyncFrame);

        mOutputBuffer.Write(msgBytes, msgLen);

        if (Global.Setting.ShowNetWorkLog)
        {
            UnityEngine.Debug.Log(string.Format("send msg: size = {0} msgId = {1} bytes = {2}", msgLen, msgId, mOutputBuffer.ToString()));
        }

        return msgLen + (int)NET_DEFINE.HEAD_SIZE;
    }

    public void Tick()
    {
        if (isInited == false)
        {
            //Debug.LogError("NetWork Client is not Init");

            return;
        }

        if (mNetWorkBase.IsConnected == false)
        {
            // Debug.LogError("未初始化");

            return;
        }

        if (this.mOutputBuffer.Lenth != 0)
        {
            this.mOutputBuffer.Flush();
        }
    }

    public void DisConnect()
    {
        if (isInited == false || mNetWorkBase == null) return;

        mNetWorkBase.DisConnect();
    }
}
