using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWrite 
{
    int Write(byte[] bytes, int size);

    void WriteByte(byte value);

    void WriteShort(short value);

    void WriteUint(uint value);

    void WriteInt32(int value);
}
