using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class AdressTest : MonoBehaviour
{
    public string key = "local/Level1.level";

    private void Start()
    {
        Addressables.InitializeAsync();
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

