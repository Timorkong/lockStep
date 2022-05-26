using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void OnLoadScence(Scene scene, LoadSceneMode mode);

public delegate void OnDestroying(Scene scene);

public class SceneManager : MonoSingleton<SceneManager>
{
    public OnLoadScence onLoadScence;

    public OnDestroying onDestroying;

    private void Start()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += Loading;//绑定委托，加载场景的时候会调用此方法
        UnityEngine.SceneManagement.SceneManager.sceneUnloaded += Destroying;//绑定委托，销毁场景的时候会调用此方法
    }

    public void LoadScence(string scenceName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scenceName, LoadSceneMode.Single);
    }


    private void Loading(Scene scene, LoadSceneMode move)
    {
        if (onLoadScence != null) onLoadScence(scene, move);
        //dosomething
    }

    private void Destroying(Scene scene)
    {
        //dosomething

        if (onDestroying != null) onDestroying(scene);
    }

}
