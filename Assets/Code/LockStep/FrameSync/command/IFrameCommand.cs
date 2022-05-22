using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IFrameCommand
{
    Cmd.ID.CMD GetID();

    byte GetSeat();

    void ExecCommand();

    string GetString();
}
