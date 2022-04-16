using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetOutputBuffer
{
    public readonly int DefaultSize = 1024 * 1024;

    public NetWorkBase netWorkBase = null;

    public int bufferLen = 0;

    public int maxBufferLen = 0;

    public byte[] buffer = null;

    public byte[] sendData = null;

    public int head = 0;

    public int tail = 0;

    public NetOutputBuffer(NetWorkBase netWorkBase)
    {
        this.netWorkBase = netWorkBase;

        bufferLen = DefaultSize;

        maxBufferLen = default;

        head = 0;

        tail = 0;

        buffer = new byte[bufferLen];

        sendData = new byte[bufferLen];
    }

    public bool Send(int size)
    {
        Array.Clear(sendData, 0, size);

        Array.Copy(buffer, head, sendData, 0, size);

        netWorkBase.Send(sendData, 0, size);

        return true;
    }

    public int Flush()
    {
        int flush = 0;

        int sendSize = 0;

        if (tail > head)
        {
            sendSize = tail - head;

            this.Send(sendSize);

            flush += sendSize;
        }
        else if (tail < head)
        {
            sendSize = bufferLen - head;

            this.Send(sendSize);

            flush += sendSize;

            head = 0;

            sendSize = tail;

            this.Send(sendSize);

            flush += sendSize;
        }

        head = tail = 0;

        return flush;
    }
}
