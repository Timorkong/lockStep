using System.IO;
using UnityEditor;

/// <summary>
/// 打表工具
/// </summary>
public partial class TablePacker : Editor
{
    #region 配置信息

    private static string m_ToolsPath = ".\\..\\common\\tools\\";
    private static string m_LogPath = ".\\..\\common\\tools\\table_tools.log";

    #endregion 配置信息

    /// <summary>
    /// 工程所在目录，是Assets的父目录
    /// </summary>
    private static string m_CurDir;

    /// <summary>
    /// 执行命令
    /// </summary>
    /// <param name="cmd">命令</param>
    /// <param name="flag">标记</param>
    private static void ExeCmd(int cmd = 0,
        string flag = "")
    {
        m_CurDir = Directory.GetCurrentDirectory();
        string toolsPath = m_CurDir + m_ToolsPath;
        try
        {
            string cmdFile = "run.py";
            string cmdPar = "";
            switch (cmd)
            {
                case 0:
                    {
                        cmdFile = "run.py";
                        cmdPar = " type-client";
                    }
                    break;
                case 1:
                    {
                        cmdFile = "run.py";
                        cmdPar = " type-client^only_byte-1";
                    }
                    break;
                case 2:
                    {
                        cmdFile = "pack_cmd.py";
                        cmdPar = "";
                    }
                    break;
            }
            /*
            var param = string.Format("{0}{1}{2}", toolsPath, cmdFile, cmdPar);
            UnityEngine.Debug.LogError(string.Format("toolsPath = {0} param = {1}", toolsPath, param));
            return;
            */
            Directory.SetCurrentDirectory(toolsPath);
            UnityCallProcess.Call(new UnityCallProcess.CallData()
            {
                fileName = "python3",
                arguments = string.Format("{0}{1}{2}", toolsPath, cmdFile, cmdPar),
                withWindow = false,
            });
            Directory.SetCurrentDirectory(m_CurDir);
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError(ex);
            Directory.SetCurrentDirectory(m_CurDir);
        }

        UnityEngine.Debug.Log("【" + flag + "】 完成 " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        CheckLog(m_CurDir + m_LogPath);
    }

    /// <summary>
    /// 检查重要日志
    /// </summary>
    /// <param name="logFilePath"></param>
    private static void CheckLog(string logFilePath)
    {
        if (string.IsNullOrEmpty(logFilePath)
            || File.Exists(logFilePath) == false)
        {
            return;
        }

        int tipLev = 0;
        string logWarnOrErr = "<color=TIP_COLOR>重要日志:</color>";
        string[] infos = File.ReadAllLines(logFilePath);
        bool haveColor = false;
        int cnt = 0;
        int warnCnt = 0;//控制警告显示的数量，避免量大Unity显示不完整
        int errCnt = 0;//控制错误显示的数量，避免量大Unity显示不完整
        foreach (string line in infos)
        {
            if (string.IsNullOrEmpty(line)
                || line.Contains("|SYS|")
                || line.Contains("|INFO|"))
            {
                continue;
            }
            if (line.Contains("|WARN|"))
            {
                if (warnCnt > 10)
                {
                    continue;
                }
                warnCnt++;
                if (tipLev < 1)
                {
                    tipLev = 1;
                }
                if (haveColor)
                {
                    logWarnOrErr += "</color>";
                    haveColor = false;
                }
                logWarnOrErr += string.Format("\n\n<color=#FFCA00>{0}", line);
                haveColor = true;
            }
            else if (line.Contains("|ERROR|"))
            {
                if (errCnt > 10)
                {
                    continue;
                }
                errCnt++;
                if (tipLev < 2)
                {
                    tipLev = 2;
                }
                if (haveColor)
                {
                    logWarnOrErr += "</color>";
                    haveColor = false;
                }
                logWarnOrErr += string.Format("\n\n<color=red>{0}", line);
                haveColor = true;
            }
            else
            {
                logWarnOrErr += string.Format("\n{0}", line);
            }
            cnt++;
        }
        if (haveColor)
        {
            logWarnOrErr += "</color>";
            haveColor = false;
        }

        string tipColor = "black";
        if (tipLev == 1)
        {
            tipColor = "#FFCA00";
        }
        else if (tipLev == 2)
        {
            tipColor = "red";
        }
        logWarnOrErr = logWarnOrErr.Replace("TIP_COLOR", tipColor);

        if (cnt == 0)
        {
            UnityEngine.Debug.Log(logWarnOrErr + "无");
        }
        else
        {
            UnityEngine.Debug.Log(logWarnOrErr);
        }
    }
}
