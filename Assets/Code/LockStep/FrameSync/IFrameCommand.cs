using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProtocolStream
{
    void encode(byte[] buffer, ref int pos);

    void decode(byte[] buffer, ref int pos);
}

public interface IFrameCommand
{
    FrameCommandID GetID();

    uint GetFrame();

    byte GetSeat();

    void ExecCommand();

    inputData GetInputData();

    void SetValue(uint frame, byte seat, inputData data);

    string GetString();

    void Reset();

}
