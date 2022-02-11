using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum FrameCommandID
{
    /// <summary>
    /// 开始游戏
    /// </summary>
    GameStart = 0,
    /// <summary>
    /// 移动
    /// </summary>
    Move = 1,
    /// <summary>
    /// 停止
    /// </summary>
    Stop = 2,
    /// <summary>
    /// 释放技能
    /// </summary>
    Skill = 3,
}

public class inputData : IProtocolStream
{
    public uint sendTime;
    public uint data1;
    public uint data2;
    public uint data3;

    #region Method
    public void encode(byte[] buffer, ref int pos) { }

    public void decode(byte[] buffer, ref int pos) { }

    public int getLen()
    {
        int _len = 0;
        // sendTime
        _len += 4;
        // data1
        _len += 4;
        // data2
        _len += 4;
        // data3
        _len += 4;
        return _len;
    }
    #endregion
}

public class fighterInput : IProtocolStream
{
    public byte seat;
    public inputData input = new inputData();

    #region Month
    public void encode(byte[] buffer , ref int pos)
    {

    }

    public void decode(byte[] buffer, ref int pos) { }

    public int getLen()
    {
        int len = 0;

        len += 1;
        
        len += input.getLen();

        return len;
    }
    #endregion
}

public class Frame : IProtocolStream
{
    public uint sequence;

    public fighterInput[] datas = new fighterInput[0];

    #region Method
    public void encode(byte[] buffer, ref int pos)
    {

    }

    public void decode(byte[] buffer, ref int pos) { }

    public int getLen()
    {
        int len = 0;

        len += 4;

        len += 2;

        for(int i = 0; i < datas.Length; i++)
        {
            len += datas[i].getLen();
        }

        return len;

    }
    #endregion
}

public class ProtocolRelayServer 
{
   
}
