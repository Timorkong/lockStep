using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;

public class RWBuffer : RingBuffer, IRead, IWrite
{
    #region Write

    public int Write(byte[] bytes, int size)
    {
        int free = this.FreeLenth;

        if (free < size) return -1;

        for (int i = 0; i < size; i++)
        {
            Buffer[Tail++] = bytes[i];

            Tail %= BufferLen;
        }

        return size;
    }

    public void WriteShort(short value)
    {
        value = IPAddress.HostToNetworkOrder(value);
        this.WriteByte((byte)((value >> 0) & 0xff));
        this.WriteByte((byte)((value >> 8) & 0xff));
    }

    public void WriteUint(uint value)
    {
        this.WriteInt32((int)value);
    }

    public void WriteInt32(int value)
    {
        value = IPAddress.HostToNetworkOrder(value);

        for (int i = 0; i < 4; i++)
        {
            this.WriteByte((byte)((value >> (i * 8)) & 0xff));
        }
    }

    public void WriteByte(byte value)
    {
        this.Buffer[Tail++] = value;

        Tail %= BufferLen;
    }

    #endregion

    #region Read

    public virtual int Read(byte[] buff, int readLen)
    {
        return 0;
    }

    public virtual int ReadInt32(int pos)
    {
        int value = 0;

        for(int i = 0;i < 4; i++)
        {
            value |= (int)(this.ReadByte(pos++) << (i * 8));
        }

        return value;
    }

    public virtual short ReadShort(int pos)
    {
        short value = 0;

        for(int i = 0; i < 2; i++)
        {
            value |= (short)(this.ReadByte(pos++) << (i * 8));
        }

        return value;
    }

    public virtual byte ReadByte(int pos)
    {
        byte value = this.Buffer[pos];

        return value;
    }

    #endregion
}
