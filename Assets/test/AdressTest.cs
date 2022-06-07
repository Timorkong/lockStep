using System.Collections;
using System.Collections.Generic;
using System.IO;
using Table;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class AdressTest : MonoBehaviour
{
    public string key = "local/Level1.level";

    private void Start()
    {
        Addressables.InitializeAsync();
        InitProto();
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
        StartCoroutine(IE_Down());
    }

    IEnumerator IE_Down()
    {
        var op = Addressables.DownloadDependenciesAsync(key);
        yield return op;
        var op1 = Addressables.LoadAssetAsync<GameObject>(key);
        yield return op1;
        if (op1.Result != null)
        {
            GameObject.Instantiate(op1.Result, this.transform);
        }
        else
        {
            Debug.LogError("op1 = null");
        }
    }
}

