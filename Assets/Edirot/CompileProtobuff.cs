using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Diagnostics;

public class CompileProtobuff : MonoBehaviour
{
    private static string rootDir = "";


    [MenuItem("Assets/编译proto->c#")]
    public static void Compile2Csharp()
    {
        rootDir = Directory.GetCurrentDirectory();
        string protoPath = rootDir + $"/../common/proto/";
        string compileName = protoPath + "compile.sh";
        UnityEngine.Debug.LogError(compileName);
        Directory.SetCurrentDirectory(protoPath);
        UnityEngine.Debug.LogError(protoPath);
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "/bin/bash";
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.Arguments = compileName;

            Process p = Process.Start(psi);
            string strOutput = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            UnityEngine.Debug.Log(strOutput);

  //          Process p = pProcess.Start("/bin/bash", protoPath);
  //          p.WaitForExit();
            UnityEngine.Debug.LogError("compile");
            Directory.SetCurrentDirectory(rootDir);
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("error = " + e.Message);
            Directory.SetCurrentDirectory(rootDir);
        }
    }

    private static bool ProcessProtoMsg(string path)
    {
        string param =string.Format("-I=./proto --csharp_out=./ {0}.proto" , path);

        return CallProcess("protoc", param);
    }

    private static bool ProcessPrototTable(string name)
    {
        return true;
    }

    static bool CallProcess(string processName, string param)
    {
        ProcessStartInfo process = new ProcessStartInfo()
        {
            CreateNoWindow = false,
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            FileName = processName,
           // Arguments = param,
        };

        Process p = Process.Start(process);

        p.WaitForExit();
        return true;
    }
}
