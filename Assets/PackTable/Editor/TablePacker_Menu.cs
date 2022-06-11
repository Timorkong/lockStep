using UnityEditor;

/// <summary>
/// 打表工具菜单
/// </summary>
public partial class TablePacker
{
    /// <summary>
    /// 打表_仅变了内容(bytes)
    /// </summary>
    [MenuItem("Assets/打表_只改内容", false)]
    public static void PackTables_OnlyBytes()
    {
        ExeCmd(1, "打表_只改内容");
    }

    /// <summary>
    /// 打表_完整(cs和bytes)
    /// </summary>
    [MenuItem("Assets/打表_改了结构", false)]
    public static void PackTables_Complete()
    {
        ExeCmd(0, "打表_改了结构");
    }
}