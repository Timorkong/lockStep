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
        Debug.LogError(string.Format("connect isDone = {0} errInfo = {1}", isDone, errInfo));
    }

    public void StartReceive()
    {
        this.mNetWorkBase.Receives(mInputBuffer.Buffer, mInputBuffer.Tail, mInputBuffer.FreeLenth, ReceiveCallBack);
    }

    public void ReceiveCallBack(bool isDone, int receiveSize, string errInfo)
    {
        Debug.LogError(string.Format("reciveCallback isDone = {0} receiveSize = {1} errInfo = {2}", isDone, receiveSize, errInfo));

        if (isDone)
        {
            this.mReceiveSize += receiveSize;

            mInputBuffer.UpdateTail(receiveSize);

            if (mReceiveSize >= (int)NET_DEFINE.HEAD_SIZE)
            {
                mCurrentPackSize = mInputBuffer.CurrentPackLenth;
            }

            if (mReceiveSize >= mCurrentPackSize)
            {
                int packSize = this.ProcessCommand();

                this.mReceiveSize -= packSize;
            }

            Debug.LogError(mInputBuffer);

            this.StartReceive();
        }
    }

    protected int ProcessCommand()
    {
        int msgSize = mInputBuffer.ReadInt32(-1, true);

        uint msgId = mInputBuffer.ReadUint32(-1 , true);

        uint sequence = mInputBuffer.ReadUint32(-1, true);

        MsgData msgData = new MsgData(msgSize);

        msgData.msgID = msgId;

        msgData.sequence = sequence;

        mInputBuffer.Read(msgData.msg, msgSize);

        NetProcess.instance.Dispatch(msgId, msgData);
        
        return msgSize + (int)NET_DEFINE.HEAD_SIZE;
    }

    public int SendCommand<CommandType>(CommandType cmd) where CommandType : IProtocolStream, IGetMsgID
    {
        int pos = 0;
        cmd.encode(this.mBuffer, ref pos);
        return SendData(cmd.GetMsgId(), cmd.GetSequence(), mBuffer, pos, 0);
    }

    public int SendData(uint msgId, uint sequence, byte[] msgBytes, int msgLen, int timeOut, PushNetErrorCallBack cb = null)
    {
        if (sequence > 0)
        {
            mPackBuffer.WritePack(msgId, sequence, msgBytes, (short)msgLen);
        }

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


        mOutputBuffer.WriteUint((uint)msgLen);

        mOutputBuffer.WriteUint(msgId);

        mOutputBuffer.WriteUint(sequence);

        mOutputBuffer.Write(msgBytes, msgLen);

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
