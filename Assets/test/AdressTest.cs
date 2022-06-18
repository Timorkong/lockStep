using System.Collections;
using System.Collections.Generic;
using System.IO;
using Table;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using System;

public class AdressTest : MonoBehaviour
{
    [NonSerialized]
    public string key = "c_table_AVATAR.bytes";

    private void Start()
    {
        Addressables.InitializeAsync();
        //InitProto();
    }

    void InitProto()
    {

        string filePath = $"{Application.dataPath}/PackTable/table_byte/c_table_AVATAR.bytes";
        var bytes = File.ReadAllBytes(filePath);
        var copy = new byte[bytes.Length - 3];
        for (int i = 3; i < bytes.Length; i++)
        {
            copy[i - 3] = bytes[i];
        }
        var a = AVATAR_ARRAY.Parser.ParseFrom(copy);
        var fd = AVATAR_ARRAY.Descriptor.FindFieldByName("rows");
        var rows = fd.Accessor.GetValue(a) as IList<AVATAR>;
        foreach (var e in rows)
        {
            Debug.Log(e.ToString());
        }
    }

    public void OnCLickDown()
    {
        // StartCoroutine(IE_Down());
        TableLoader.Instance.LoadTables();
    }

    IEnumerator IE_Down()
    {
        Debug.LogError("load name = " + key);

        var op1 = Addressables.LoadAssetAsync<UnityEngine.TextAsset>(key);
        yield return op1;
        if (op1.Result != null)
        {
            //op1.Result.bytes
            Debug.LogError("load sucess");
        }
        else
        {
            Debug.LogError("op1 = null");
        }
    }
}

