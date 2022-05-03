using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void OnMSGDeserialized(Cmd.ID.CMD cmd);

public class NetProcess : Singleton<NetProcess>
{
    Queue<MsgData> msgQueue = new Queue<MsgData>();

    private EventRuter<uint> msgRuter = new EventRuter<uint>();

    public OnMSGDeserialized onMSGDeserialized;

    uint receiveMaxSequence = 0;

    public void AddMsgHandle<T>(uint msgId , Action<T> handle)
    {
        RemoveMsgHandle(msgId, handle);
        msgRuter.On(msgId, handle);
    }

    public void RemoveMsgHandle<T>(uint msgId , Action<T> handle)
    {
        msgRuter.Off(msgId, handle);
    }

    public void Dispatch<T>(uint msgId , T data)
    {
        msgRuter.DisPatch<T>(msgId, data);
    }
        

    public void Push(MsgData data)
    {
        msgQueue.Enqueue(data);
    }

    public MsgData Pop()
    {
        if (msgQueue.Count == 0) return null;

        MsgData ret = this.msgQueue.Dequeue();

        return ret;
    }

    public void Tick(uint deltaInMillSecond)
    {
        this.MsgProcess();
    }

    protected void MsgProcess()
    {
        while (true)
        {
            MsgData msgData = Pop();

            if (msgData == null) return;

            Process(msgData);
        }
    }

    private void Process(MsgData msg)
    {
        if (this.receiveMaxSequence < msg.sequence) this.receiveMaxSequence = msg.sequence;

        if (onMSGDeserialized != null) onMSGDeserialized((Cmd.ID.CMD)msg.msgID);

        NetProcess.Instance.Dispatch(msg.msgID, msg);
    }

}
