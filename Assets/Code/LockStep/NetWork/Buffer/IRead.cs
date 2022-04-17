using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRead 
{
    int Read(byte[] buff, int readLen);

    byte ReadByte(int pos);

    short ReadShort(int pos);

    int ReadInt32(int pos);
}
