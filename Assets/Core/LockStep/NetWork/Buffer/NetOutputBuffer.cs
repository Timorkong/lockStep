using System;
//用于发送数据
public class NetOutputBuffer : RWBuffer
{
    public NetWorkBase netWorkBase = null;

    public byte[] sendData = null;

    public NetOutputBuffer(NetWorkBase netWorkBase) : base()
    {
        this.netWorkBase = netWorkBase;

        sendData = new byte[BufferLen];
    }

    public bool Send(int size)
    {
        Array.Clear(sendData, 0, size);

        Array.Copy(Buffer, Head, sendData, 0, size);

        netWorkBase.Send(sendData, 0, size);

        return true;
    }
    //逻辑要整理
    public int Flush()
    {
        int flush = 0;

        int sendSize = 0;

        if (Tail > Head)
        {
            sendSize = Tail - Head;

            this.Send(sendSize);

            flush += sendSize;
        }
        else if (Tail < Head)
        {
            sendSize = BufferLen - Head;

            this.Send(sendSize);

            flush += sendSize;

            Head = 0;

            sendSize = Tail;

            this.Send(sendSize);

            flush += sendSize;
        }

        Head = Tail = 0;

        return flush;
    }

}
