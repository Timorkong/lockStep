using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWrite 
{
    int Write(byte[] bytes, int size);

    void WriteByte(byte value);

    void WriteShort(short value, bool bSyncFrame = false);

    void WriteUint(uint value, bool bSyncFrame = false);

    void WriteInt32(int value, bool bSyncFrame = false);
}
