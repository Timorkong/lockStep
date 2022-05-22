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

    public void WriteShort(short value, bool bSyncFrame = false)
    {
        value = IPAddress.HostToNetworkOrder(value);
        this.WriteByte((byte)((value >> 0) & 0xff));
        this.WriteByte((byte)((value >> 8) & 0xff));
    }

    public void WriteUint(uint value, bool bSyncFrame = false)
    {
        this.WriteInt32((int)value, bSyncFrame);
    }

    public void WriteInt32(int value, bool bSyncFrame = false)
    {
        if (!bSyncFrame) value = IPAddress.HostToNetworkOrder(value);

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

    public virtual bool Read(byte[] buff, int readLen, bool skip = true)
    {
        if (this.Lenth < readLen)
        {
            Debug.LogError("获取失败，空间不足");

            return false;
        }

        int indexHead = Head;

        for (int i = 0; i < readLen; i++)
        {
            buff[i] = this.Buffer[indexHead++];

            indexHead %= BufferLen;
        }

        if (skip) UpdateHead(readLen);

        return true;
    }

    public virtual int ReadInt32(int pos = -1, bool skip = true)
    {
        int value = 0;

        if (pos == -1) pos = this.Head;

        for (int i = 0; i < 4; i++)
        {
            pos %= BufferLen;

            value |= (int)(this.ReadByte(pos++, skip) << (i * 8));
        }

        return value;
    }

    public virtual uint ReadUint32(int pos = -1, bool skip = true)
    {
        return (uint)ReadInt32(pos ,skip);
    }


    public virtual short ReadShort(int pos = -1, bool skip = true)
    {
        short value = 0;

        if (pos == -1) pos = this.Head;

        for (int i = 0; i < 2; i++)
        {
            pos %= BufferLen;

            value |= (short)(this.ReadByte(pos++, skip) << (i * 8));
        }

        return value;
    }

    public virtual byte ReadByte(int pos = -1, bool skip = true)
    {
        if (pos == -1) pos = this.Head;

        pos %= BufferLen;

        byte value = this.Buffer[pos];

        if (skip) UpdateHead(1);

        return value;
    }

    public bool UpdateHead(int len)
    {
        if (len == 0) return false;

        if (len > Lenth) return false;

        Head = (Head + len) % BufferLen;

        return true;
    }

    public bool UpdateTail(int len)
    {
        if (len == 0) return false;

        if (len > FreeLenth) return false;

        Tail = (len + Tail) % BufferLen;

        return true;
    }

    #endregion
}
