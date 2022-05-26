using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBuffer
{
    public static readonly int DefaultSize = 256 * 1024;

    protected int mBufferLen = 0;

    protected int mMaxBufferLen = 0;

    protected byte[] mBuffer = null;

    protected int mHead = 0;

    protected int mTail = 0;

    public int BufferLen
    {
        get
        {
            return this.mBufferLen;
        }
    }

    public byte[] Buffer
    {
        get
        {
            return this.mBuffer;
        }
    }

    public int Head
    {
        get
        {
            return this.mHead;
        }
        set
        {
            this.mHead = value;
        }
    }

    public int Tail
    {
        get
        {
            return this.mTail;
        }
        set
        {
            this.mTail = value;
        }
    }
    /// <summary>
    /// 已经使用的长度
    /// </summary>
    public int Lenth
    {
        get
        {
            if (mTail >= mHead) return mTail - mHead;
            else return mBufferLen - mHead + mTail;
        }
    }
    /// <summary>
    /// 可以使用的长度
    /// </summary>
    public int FreeLenth
    {
        get
        {
            if (mTail >= Head) return mBufferLen - mTail - 1;
            else
            {
                return Head - mTail - 1;
            }
        }
    }

    public RingBuffer()
    {
        mBufferLen = DefaultSize;

        mMaxBufferLen = default;

        mHead = 0;

        mTail = 0;

        mBuffer = new byte[mBufferLen];
    }

    public virtual void Reset()
    {
        mHead = 0;

        mTail = 0;

        Array.Clear(mBuffer, 0, mBuffer.Length);
    }


    public override string ToString()
    {
        var output = "len = " + this.Lenth.ToString() + " ";

        for(int i = Head; i < Tail; i++)
        {
            output += mBuffer[i].ToString() + " ";
        }
        return output;
    }
}
