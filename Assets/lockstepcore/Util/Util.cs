using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    /// <summary>
    /// <para>遍历Component所属GameObject的所有子节点，查找名字为goName的GameObject（包含了对应Compoent）</para>
    /// <para>包含Inactive子节点</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="root">根节点</param>
    /// <param name="goName">查找的节点名称</param>
    /// <param name="includeInactive"></param>
    /// <param name="includeRoot"></param>
    /// <returns>返回查询到的Component，如果没找到，则为null</returns>
    static public T Find<T>(Component root, string goName, bool includeInactive = true, bool includeRoot = false) where T : Component
    {
        if (root == null)
        {
            return null;
        }

        return Find<T>(root.transform, goName, includeInactive, includeRoot);
    }

    /// <summary>
    /// <para>遍历Component所属GameObject的所有子节点，查找名字为goName的GameObject（包含了对应Compoent）</para>
    /// <para>包含Inactive子节点</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="root">根节点</param>
    /// <param name="goName">查找的节点名称</param>
    /// <param name="includeInactive"></param>
    /// <param name="includeRoot"></param>
    /// <returns>返回查询到的Component，如果没找到，则为null</returns>
    static public T Find<T>(GameObject root, string goName, bool includeInactive = true, bool includeRoot = false) where T : Component
    {
        if (root == null)
        {
            return null;
        }

        return Find<T>(root.transform, goName, includeInactive, includeRoot);
    }

    /// <summary>
    /// <para>遍历Component所属GameObject的所有子节点，查找名字为goName的GameObject（包含了对应Compoent）</para>
    /// <para>包含Inactive子节点</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="root">根节点</param>
    /// <param name="goName">查找的节点名称</param>
    /// <param name="includeInactive"></param>
    /// <param name="includeRoot"></param>
    /// <returns>返回查询到的Component，如果没找到，则为null</returns>
    static public T Find<T>(UnityEngine.Transform root, string goName, bool includeInactive = true, bool includeRoot = false) where T : Component
    {
        if (root == null)
        {
            return null;
        }

        T[] list = root.gameObject.GetComponentsInChildren<T>(includeInactive);
        foreach (T t in list)
        {
            if (includeRoot == false && root == t.transform)
            {
                continue;
            }

            if (t.gameObject.name == goName)
            {
                return t;
            }
        }
        return null;
    }

    /// <summary>
    /// <para>遍历GameObject自身以及所有子节点，查找名字为goName的GameObject</para> 
    /// </summary>
    /// <param name="root">根节点</param>
    /// <param name="goName">查找的节点名称</param>
    /// <param name="includeInactive">是否包含Inactive节点</param>
    /// <param name="includeRoot"></param>
    /// <returns>返回查询到的GameObject，如果没找到，则为null</returns>
    public static GameObject Find(GameObject root, string goName, bool includeInactive = true, bool includeRoot = false)
    {
        if (root == null)
        {
            return null;
        }

        UnityEngine.Transform[] list = root.GetComponentsInChildren<UnityEngine.Transform>(includeInactive);
        foreach (UnityEngine.Transform t in list)
        {
            if (includeRoot == false && root.transform == t)
            {
                continue;
            }

            if (t.gameObject.name == goName)
            {
                return t.gameObject;
            }
        }

        return null;
    }

    static public T FindInParents<T>(GameObject go, bool includeSelf = true) where T : Component
    {
        if (go == null)
        {
            return null;
        }

        T comp = null;
        if (includeSelf)
        {
            comp = go.GetComponent<T>();
        }

        if (comp == null)
        {
            UnityEngine.Transform t = go.transform.parent;

            while (t != null && comp == null)
            {
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
        }
        return (T)comp;
    }

    /// <summary>
    /// <para>在父节点中查找名字为name的GameObject</para> 
    /// </summary>
    /// <param name="go">当前节点GameObject</param>
    /// <param name="name"></param>
    /// <returns>返回查找到的GameObject，如果没找到，则为null</returns>
    static public GameObject FindInParents(GameObject go, string name)
    {
        if (go == null)
        {
            return null;
        }

        UnityEngine.Transform t = go.transform.parent;
        if (t == null)
        {
            return null;
        }

        if (t.name == name)
        {
            return t.gameObject;
        }

        return FindInParents(t.gameObject, name);
    }

    /// <summary>
    /// 删除所有子节点
    /// </summary>
    /// <param name="go"></param>
    /// <param name="immediate">是否立即删除，编辑器有时需要</param>
    public static void DestroyAllChildren(GameObject go, bool immediate = false)
    {
        if (go == null)
        {
            return;
        }

        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < go.transform.childCount; i++)
        {
            list.Add(go.transform.GetChild(i).gameObject);
        }
        while (list.Count > 0)
        {
            if (immediate)
            {
                UnityEngine.Object.DestroyImmediate(list[0]);
            }
            else
            {
                UnityEngine.Object.Destroy(list[0]);
            }
            list[0] = null;
            list.RemoveAt(0);
        }
        list.Clear();
    }

    /// <summary>
    /// 删除所有子节点
    /// </summary>
    /// <param name="comp"></param>
    /// <param name="immediate">是否立即删除，编辑器有时需要</param>
    public static void DestroyAllChildren(Component comp, bool immediate = false)
    {
        if (comp == null)
        {
            return;
        }

        DestroyAllChildren(comp.gameObject, immediate);
    }

    /// <summary>
    /// <para>删除所有Active属性为true的子节点</para> 
    /// <para>内部调用的是Object.Destroy</para> 
    /// </summary>
    /// <param name="go">根节点</param>
    public static void DestroyActiveChildren(GameObject go)
    {
        if (go == null)
        {
            return;
        }

        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < go.transform.childCount; i++)
        {
            GameObject childGO = go.transform.GetChild(i).gameObject;
            if (childGO.activeSelf && childGO.activeInHierarchy)
            {
                list.Add(childGO);
            }
        }
        foreach (GameObject obj2 in list)
        {
            UnityEngine.Object.Destroy(obj2);
        }
        list.Clear();
    }

    /// <summary>
    /// <para>删除所有Active属性为true的子节点</para> 
    /// </summary>
    /// <param name="comp">根节点</param>
    public static void DestroyActiveChildren(Component comp)
    {
        if (comp == null)
        {
            return;
        }

        DestroyActiveChildren(comp.gameObject);
    }

    public static void Hide(GameObject go)
    {
        if (go == null)
            return;

        Renderer[] renders = go.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer render in renders)
        {
            render.enabled = false;
        }
    }
    
    public static Sprite LoadModelIcon(string IconName)
    {
        string path = "UI/AvatarIcon/" + IconName;

        Sprite ret = Resources.Load<Sprite>(path);

        return ret;
    }
    
    static float y_lenth = 120;

    static List<Vector2> y_pos_range = new List<Vector2>()
    {
        new Vector2(0,0.6f),
        new Vector2(0.5f,0.8f),
        new Vector2(0.8f,0.97f),
        new Vector2(0.971f,0.98f),
        new Vector2(0.981f,1)
    };

    /// <summary>
    /// 计算角度360
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    static public float angle_360(Vector3 from, Vector3 to)
    {
        float ret = Vector3.Angle(from, to);

        Vector3 vec3 = Vector3.Cross(from, to);

        if (vec3.z > 0)
        {
            ret = 360 - ret;
        }

        return ret;
    }

    static public string Bytes2String(byte[] bytes)
    {
        string ret = "";

        for (int i = 0; i < bytes.Length; i++)
        {
            ret += bytes[i].ToString() + " ";
        }

        return ret;
    }
}
