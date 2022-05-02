using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackBuffer : RWBuffer
{
    public void WritePack(uint msgID , uint sequence , byte[] msg , short size)
    {
        this.WriteInt32(size);

        this.WriteInt32((int)msgID);

        this.WriteInt32((int)sequence);

        this.Write(msg, size);
    }
}
