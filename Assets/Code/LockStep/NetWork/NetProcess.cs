using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetProcess : Singleton<NetProcess>
{
    Queue<MsgData> msgQueue = new Queue<MsgData>();

    private EventRuter<uint> msgRuter = new EventRuter<uint>();

    uint receiveMaxSequence = 0;

    public void AddMsgHandle<MsgData>(uint msgId , Action<MsgData> handle)
    {
        RemoveMsgHandle(msgId, handle);
        msgRuter.On(msgId, handle);
    }

    public void RemoveMsgHandle<MsgData>(uint msgId , Action<MsgData> handle)
    {
        msgRuter.Off(msgId, handle);
    }
        

    public void Push(MsgData data)
    {
        msgQueue.Enqueue(data);
    }

    public MsgData Pop()
    {
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
    }

}
