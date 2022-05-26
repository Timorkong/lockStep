using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageHandleAttribute : Attribute
{
    public uint msgId = 0;
    public MessageHandleAttribute(uint msgId)
    {
        this.msgId = msgId;
    }
}
