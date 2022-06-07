#if UNITY_EDITOR
using System;
using System.Diagnostics;
using System.Collections.Generic;

/// <summary>
/// Unity执行外部程序
/// </summary>
public class UnityCallProcess
{
    /// <summary>
    /// 调用数据
    /// </summary>
    public class CallData
    {
        /// <summary>
        /// 程序名，例如：TortoiseProc.exe、svn.exe
        /// </summary>
        public string fileName;
        /// <summary>
        /// 参数
        /// </summary>
        public string arguments;
        /// <summary>
        /// 是否带有系统窗口，默认：true
        /// </summary>
        public bool withWindow = true;
        /// <summary>
        /// 环境变量
        /// </summary>
        public Dictionary<string, string> environment = null;
        /// <summary>
        /// 打印错误，默认：true
        /// </summary>
        public bool printErr = true;
    }

    /// <summary>
    /// 异步调用数据
    /// </summary>
    public class AsyncCallData : CallData
    {
        /// <summary>
        /// 回调
        /// </summary>
        public Action<ReturnData> cb;
    }

    /// <summary>
    /// 返回数据
    /// </summary>
    public class ReturnData
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success => string.IsNullOrEmpty(err);

        /// <summary>
        /// 错误信息
        /// </summary>
        public string err;
        /// <summary>
        /// 输出
        /// </summary>
        public string output;

        /// <summary>
        /// 增加错误信息
        /// </summary>
        /// <param name="msg"></param>
        public void AddErr(string msg)
        {
            if (!string.IsNullOrEmpty(err))
            {
                err += "\n";
            }
            err += $"{msg}";
        }

        public void AddOutput(string msg)
        {
            if (!string.IsNullOrEmpty(output))
            {
                output += "\n";
            }
            output += $"{msg}";
        }

        public override string ToString()
        {
            return $"success:{Success}\nerr:{err}\noutput:{output}";
        }
    }

    /// <summary>
    /// 异步执行外部程序
    /// </summary>
    /// <param name="data"></param>
    public static void AsyncCall(AsyncCallData data)
    {
        Process p = new Process();
        p.StartInfo = new ProcessStartInfo
        {
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            FileName = data.fileName,
            Arguments = data.arguments,
            StandardErrorEncoding = System.Text.UTF8Encoding.UTF8,
            StandardOutputEncoding = System.Text.UTF8Encoding.UTF8,
        };
        if (data.environment != null)
        {
            foreach (var e in data.environment)
            {
                p.StartInfo.Environment.Add(e.Key, e.Value);
            }
        }

        ReturnData rd = new ReturnData();

        p.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
        {
            rd.AddOutput(e.Data);
        };
        p.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
        {
            rd.AddErr(e.Data);
        };
        p.EnableRaisingEvents = true;
        p.Exited += (object sender, EventArgs e) =>
        {
            try
            {
                //注意：用户取消也是非成功
                if (data.printErr
                    && !rd.Success)
                {
                    UnityEngine.Debug.LogErrorFormat("{0} {1} ERROR\n{2}",
                        data.fileName,
                        data.arguments,
                        rd.err);
                }
                data.cb?.Invoke(rd);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError(ex.Message);
            }
        };
        p.Start();
        p.BeginOutputReadLine();
        p.BeginErrorReadLine();
    }

    /// <summary>
    /// 执行外部程序
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ReturnData Call(CallData data)
    {
        ReturnData rd = new ReturnData();

        ProcessStartInfo process = new ProcessStartInfo
        {
            CreateNoWindow = !data.withWindow,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            FileName = data.fileName,
            Arguments = data.arguments,
            StandardErrorEncoding = System.Text.UTF8Encoding.UTF8,
            StandardOutputEncoding = System.Text.UTF8Encoding.UTF8,
        };
        if (data.environment != null)
        {
            foreach (var e in data.environment)
            {
                process.Environment.Add(e.Key, e.Value);
            }
        }

        //如果外部程序不存在，要异常，所以加 try
        try
        {
            Process p = Process.Start(process);
            p.WaitForExit();
            rd.AddErr(p.StandardError.ReadToEnd());
            if (rd.Success && p.ExitCode != 0)
            {
                rd.AddErr("SomeError");
            }
            rd.AddOutput(p.StandardOutput.ReadToEnd());
            p.Close();
        }
        catch (Exception ex)
        {
            rd.AddErr(ex.Message);
        }

        //注意：用户取消也是非成功
        if (data.printErr
            && !rd.Success)
        {
            UnityEngine.Debug.LogErrorFormat("{0} {1} ERROR\n{2}",
                data.fileName,
                data.arguments,
                rd.err);
        }

        return rd;
    }

    public static void OpenCmd(string path = "")
    {
        string argv = "";
        if (!string.IsNullOrEmpty(path))
        {
            argv = $"/k cd /d \"{path}\"";
        }

        Process p = new Process();
        p.StartInfo = new ProcessStartInfo
        {
            CreateNoWindow = false,
            UseShellExecute = true,
            FileName = "cmd",
            Arguments = argv,
        };
        p.Start();
    }
}
#endif