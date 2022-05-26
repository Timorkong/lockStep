using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IFrameCommand
{
    Cmd.ID.CMD CmdId { get; }

    uint Sequence { get; }

    int PlayerSeat { get; }

    string GetString { get; }

    void ExecCommand();

}
