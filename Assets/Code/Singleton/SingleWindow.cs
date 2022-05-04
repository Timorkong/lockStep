using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMutexWindow
{
    void Show(bool needSendCmd = true);

    void Hide();

    bool IsVisible { get; }
}

public class SingleWindow<T> : MonoSingleton<T>, IMutexWindow where T : UnityEngine.Component
{
    protected GameObject window;

    private Cmd.ID.CMD curShowCMD = Cmd.ID.CMD.CMD_INVALID;

    public virtual Cmd.ID.CMD ShowCMD { get { return Cmd.ID.CMD.CMD_INVALID; } }

    public virtual void Request() { }

    protected override void Awake()
    {
        base.Awake();

        Transform t = this.transform.Find("Window");

        window = t.gameObject;
    }

    public void Show(bool needSendCmd = true)
    {
        if (IsVisible) return;

        if (needSendCmd && this.ShowCMD != Cmd.ID.CMD.CMD_INVALID)
        {
            NetProcess.Instance.onMSGDeserialized += OnMSGDeserialized;

            Request();

            return;
        }

        window.SetActive(true);

        OnShow();
    }

    private void OnMSGDeserialized(Cmd.ID.CMD cmd)
    {
        if (cmd == Cmd.ID.CMD.CMD_INVALID) return;

        if (ShowCMD != cmd) return;

        NetProcess.Instance.onMSGDeserialized -= OnMSGDeserialized;

        window.SetActive(true);

        OnShow();
    }

    public void Hide()
    {
        if (IsVisible == false) return;

        window.SetActive(false);

        OnHide();
    }

    public bool IsVisible
    {
        get { return this.window.activeSelf; }
    }

    public virtual void OnShow()
    {

    }

    public virtual void OnHide()
    {

    }
}
