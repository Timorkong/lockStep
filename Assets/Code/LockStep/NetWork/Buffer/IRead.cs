using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRead
{
    bool Read(byte[] buff, int readLen, bool skip);

    byte ReadByte(int pos, bool skip);

    short ReadShort(int pos, bool skip);

    int ReadInt32(int pos, bool skip);

    uint ReadUint32(int pos, bool skip);
}
