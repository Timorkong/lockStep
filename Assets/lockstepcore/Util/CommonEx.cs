using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// 通用扩展
/// </summary>
static public class CommonEx
{
    /// <summary>
    /// 查找结点
    /// </summary>
    /// <param name="monoNode"></param>
    /// <param name="path"></param>
    /// <param name="showErr"></param>
    static public T FindNode<T>(this MonoBehaviour monoNode,
        string path,
        bool showErr = true) where T : Component
    {
        if (monoNode == null)
        {
            if (showErr)
            {
                Debug.LogError("monoNode为空");
            }
            return null;
        }
        return monoNode.transform.FindNode<T>(path, showErr);
    }

    /// <summary>
    /// 查找结点
    /// </summary>
    /// <param name="tfNode"></param>
    /// <param name="path"></param>
    /// <param name="showErr"></param>
    static public T FindNode<T>(this Transform tfNode,
        string path,
        bool showErr = true) where T : Component
    {
        if (tfNode == null)
        {
            if (showErr)
            {
                Debug.LogError("tfNode为空");
            }
            return null;
        }
        Transform tf = tfNode.Find(path);
        if (tf == null)
        {
            if (showErr)
            {
                Debug.LogError(string.Format("FindNode错误，路径不存在\nWin = {0}-{1}\nPath = {2}", tfNode.GetType(), tfNode.name, path), tfNode);
            }
            return null;
        }
        return tf.GetComponent<T>();
    }
    
    /// 近似
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    static public bool Approximately(this Vector3 v1, Vector3 v2, float epsilon = 0.05f)
    {
        Vector3 delta = v1 - v2;
        if (Mathf.Abs(delta.x) > epsilon)
        {
            return false;
        }
        if (Mathf.Abs(delta.y) > epsilon)
        {
            return false;
        }
        if (Mathf.Abs(delta.z) > epsilon)
        {
            return false;
        }
        return true;
    }

    static public bool Approximately(this double a, double b, double epsilon = 0.000001f)
    {
        return Math.Abs(a - b) < epsilon;
    }

    /// <summary>
    /// 转字符串
    /// </summary>
    /// <param name="v"></param>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    static public string ToStr(this Vector3 v, int epsilon = 3)
    {
        string formatStr = "({0:F" + epsilon + "},{1:F" + epsilon + "},{2:F" + epsilon + "})";
        return string.Format(formatStr, v.x, v.y, v.z);
    }

    /// <summary>
    /// 转换可见
    /// </summary>
    /// <param name="tf"></param>
    /// <param name="active"></param>
    static public bool ChangeActive(this Transform tf, bool active)
    {
        if (tf == null)
        {
            return false;
        }
        return tf.gameObject.ChangeActive(active);
    }

    /// <summary>
    /// 转换可见
    /// </summary>
    /// <param name="go"></param>
    /// <param name="active"></param>
    static public bool ChangeActive(this GameObject go, bool active)
    {
        if (go == null || go.activeSelf == active)
        {
            return false;
        }
        go.SetActive(active);
        return true;
    }

    static public bool ChangeEnabled(this Behaviour behaviour, bool enabled)
    {
        if (behaviour == null || behaviour.enabled == enabled)
        {
            return false;
        }
        behaviour.enabled = enabled;
        return true;
    }
}