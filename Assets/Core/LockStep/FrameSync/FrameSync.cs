using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumFrameSyncState
{
    /// <summary>
    /// ��������
    /// </summary>
    OnCreate,
    /// <summary>
    /// ��ʼͬ��
    /// </summary>
    OnStart,
    /// <summary>
    /// ͬ����
    /// </summary>
    OnTick,
    /// <summary>
    /// ����
    /// </summary>
    OnReconnect,
    /// <summary>
    /// ����
    /// </summary>
    OnEnd,
}

public class FrameSync : Singleton<FrameSync>
{
    protected Queue<IFrameCommand> frameQueue = new Queue<IFrameCommand>();

    private EnumSyncMode syncMode = EnumSyncMode.LocalFrame;

    private EnumFrameSyncState frameSyncState = EnumFrameSyncState.OnCreate;

    public uint serverFrame;

    public uint serverFrameMs;

    public uint curFrame;

    public float fLocalAcc = 0;

    public static uint logicUpdateStep = 32;

    public static uint logicFrameStep = 2;

    public void StartFrameSync(EnumSyncMode syncMode)
    {
        this.syncMode = syncMode;

        Debug.LogError("set sync mode = " + syncMode);
    }

    public void UpdateFrame()
    {
        switch (syncMode)
        {
            case EnumSyncMode.LocalFrame:
                {
                    UpdateLocalFrame();

                    break;
                }
            case EnumSyncMode.SyncFrame:
                {
                    UpdateSyncFrame();
                    break;
                }
        }
    }
    /*
    private void _pushNetCommand(Frame[] frames)
    {
        for (int i = 0; i < frames.Length; i++)
        {
            var frame = frames[i];

            SetServerFrame(frame.sequence);

            for (int j = 0; j < frame.datas.Length; j++)
            {
                var fighterInput = frame.datas[j];

                byte seat = fighterInput.seat;

                inputData data = fighterInput.input;

                if (GameApplaction.Instance.playerSeat == seat)
                {

                }

                IFrameCommand frameCmd = FrameCommandFactory.CreateCommand(data.data1);

                if (frameCmd == null)
                {
                    Debug.LogFormat("Seat{0} Data Id {1}FrameCommand is Null!! \n", seat, data.data1);
                }
                else
                {
                    Debug.LogFormat("{0}Recive Cmd {1} \n", System.DateTime.Now.ToLongTimeString(), frameCmd.GetString());

                    BaseFrameCommand baseFrameCmd = frameCmd as BaseFrameCommand;

                    Cmd.ID.CMD frameCmdID = frameCmd.GetID();

                    Debug.LogFormat("[֡ͬ��] �յ� �������� {0}", frameCmdID);

                    if (isGetStartFrame == false)
                    {
                        isGetStartFrame = true;

                        ClearCmdQueue();
                    }

                    frameQueue.Enqueue(frameCmd);
                }
            }
        }
    }
    */

    public void PushNetCommand(MsgData msg)
    {
        _pushNetCommand(msg);
    }

    private void _pushNetCommand(MsgData msg)
    {
        IFrameCommand frameCmd = FrameCommandFactory.CreateCommand(msg);

        if (frameCmd.CmdId == Cmd.ID.CMD.CMD_WAR_SEQUENCE_NOTICE)
        {
            SetServerFrame(frameCmd.Sequence);
        }

        frameQueue.Enqueue(frameCmd);
    }

    public void SetServerFrame(uint frame)
    {
        this.serverFrame = frame;
    }

    void UpdateLocalFrameCommand()
    {
        if (frameQueue.Count == 0) return;

        IFrameCommand cmd = frameQueue.Dequeue();

        cmd.ExecCommand();
    }

    void UpdateLocalFrame()
    {
        float deltaTime = Time.deltaTime;

        deltaTime = Mathf.Clamp(deltaTime, 0, 100);

        fLocalAcc += deltaTime;

        float frameRate = logicFrameStep / 1000f;

        while (fLocalAcc >= frameRate)
        {
            curFrame += logicFrameStep;

            UpdateLocalFrameCommand();

            fLocalAcc -= frameRate;
        }
    }

    void UpdateSyncFrame()
    {
        int framesNeedUpdate = (int)(serverFrame - curFrame);

        framesNeedUpdate = Mathf.Clamp(framesNeedUpdate, 0, 100);

        while (framesNeedUpdate >= 0 && frameQueue.Count > 0)
        {
            var cmd = frameQueue.Dequeue();

            if (cmd.CmdId == Cmd.ID.CMD.CMD_WAR_SEQUENCE_NOTICE)
            {
                curFrame = cmd.Sequence;

                framesNeedUpdate--;
            }

            cmd.ExecCommand();

            BattleMain.Instance.mBattle.UpdateLogic(0);
        }

        BattleMain.Instance.mBattle.UpdateView(0);
    }

    public void ClearCmdQueue()
    {
        frameQueue.Clear();
    }
}
