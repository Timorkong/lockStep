using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumSyncMode
{
    None,
    /// <summary>
    // 本地
    /// </summary>
    LocalFrame,
    /// <summary>
    /// 服务器同步
    /// </summary>
    SyncFrame,
}

public enum EnumBattleType
{
    None = -1,

    DunGeon = 0,
}

public interface IUpdate
{
    void UpdateLogic(int delta);

    void UpdateView(int delta);
}

public interface IBattle
{
    EnumBattleType GetBattleType { get; }

    EnumSyncMode GetSyncMode();

    void InitBattle();

    void UpdateLogic(int deltaTime);
}
