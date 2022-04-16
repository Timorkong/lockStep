using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumSyncMode
{
    None,
    /// <summary>
    //���ز��� 
    /// </summary>
    LocalFrame,
    /// <summary>
    /// ������ͬ��
    /// </summary>
    SyncFrame,
}

public enum EnumBattleType
{
    None = -1,

    DunGeon = 0,
}

public interface IBattle
{
    EnumBattleType GetBattleType { get; }

    EnumSyncMode GetSyncMode();

    void Update(int delta);

    void FrameUpdate(int delta);
}
