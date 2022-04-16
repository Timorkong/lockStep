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

public class FrameSync:Singleton<FrameSync>
{
    protected Queue<IFrameCommand> frameQueue = new Queue<IFrameCommand>();

    const int cFrameMin = 2;

    static int debugCurFrameNeedUpdate = 0;

    private EnumSyncMode syncMode = EnumSyncMode.LocalFrame;

    private EnumFrameSyncState frameSyncState = EnumFrameSyncState.OnCreate;

    private IDungeonManager mMainLogic = null;

    private float _timePre = 0;

    public uint serverFrame;
    
    public uint serverFrameMs;
    
    public uint serverFramelater = 0;
    
    public float timeStart;
    
    public uint curFrame;
    
    public uint endFrame;
    
    public uint frameMs;
    
    public uint freamSpeed = 1; 
    
    public float fLocalAcc = 0;
    
    public static uint logicUpdateStep = 32;
    
    public static uint logicFrameStep = 2;
    
    public static int logicFrameStepDelta = 0;

    private bool mIsGetStartFrame = false;

    public bool isFire = true;

    public int nDegree;

    public bool bInMoveMode;
    public bool isGetStartFrame
    {
        get
        {
            return mIsGetStartFrame;
        }
        private set
        {
            mIsGetStartFrame = value;
        }
    }
 
    public void SetMainLogic(IDungeonManager mainLogic)
    {
        mMainLogic = mainLogic;
    }

    public void StartFrameSync()
    {
        Debug.LogError("[֡ͬ��] ��ʼ֡ͬ��");
    }

    public void FirtFrameCommand(IFrameCommand cmd , bool force = false)
    {
        if (!isFire) return;

        switch (syncMode)
        {
            case EnumSyncMode.LocalFrame:
                {
                    frameQueue.Enqueue(cmd);

                    BaseFrameCommand baseFrame = cmd as BaseFrameCommand;

                    baseFrame.seat = 0;

                    break;
                }
            case EnumSyncMode.SyncFrame:
                {
                    break;
                }
        }
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

    public void PushNetCommand(Frame[] frames)
    {
        _pushNetCommand(frames);
    }
    
    public void SetServerFrame(uint frame)
    {

    }

    private void _pushNetCommand(Frame[] frames)
    {
        uint nowTime = (uint)(Time.time * GlobalLogic.VALUE_1000);

        float delta = Time.realtimeSinceStartup * GlobalLogic.VALUE_1000;

        for(int i = 0;i < frames.Length; i++)
        {
            var frame = frames[i];

            SetServerFrame(frame.sequence);

            for(int j = 0;j < frame.datas.Length; j++)
            {
                var fighterInput = frame.datas[j];

                byte seat = fighterInput.seat;

                inputData data = fighterInput.input;

                if(GameApplaction.instance.playerInfo.seat == seat)
                {

                }

                IFrameCommand frameCmd = FrameCommandFactory.CreateCommand(data.data1);

                if(frameCmd == null)
                {
                    Debug.LogFormat("Seat{0} Data Id {1}FrameCommand is Null!! \n", seat, data.data1);
                }
                else
                {
                    Debug.LogFormat("{0}Recive Cmd {1} \n", System.DateTime.Now.ToLongTimeString(), frameCmd.GetString());

                    frameCmd.SetValue(frame.sequence, seat, data);

                    BaseFrameCommand baseFrameCmd = frameCmd as BaseFrameCommand;

                    if(baseFrameCmd != null)
                    {
                        baseFrameCmd.sendTime = data.sendTime;
                    }

                    FrameCommandID frameCmdID = (FrameCommandID)frameCmd.GetID();

                    Debug.LogFormat("[֡ͬ��] �յ� �������� {0}", frameCmdID);

                    if (isGetStartFrame == false)
                    {
                        if(frameCmdID == FrameCommandID.GameStart)
                        {
                            isGetStartFrame = true;

                            ClearCmdQueue();
                        }
                    }

                    frameQueue.Enqueue(frameCmd);
                }
            }
        }
    }

    void UpdateLocalFrameCommand()
    {
        if (frameQueue.Count == 0) return;

        IFrameCommand cmd = frameQueue.Dequeue();

        var cmdBase = cmd as BaseFrameCommand;

        cmdBase.frame = curFrame;

        cmd.ExecCommand();
    }

    void UpdateLocalFrame()
    {
        float deltaTime = Time.deltaTime;

        deltaTime = Mathf.Clamp(deltaTime, 0, 100);

        fLocalAcc += deltaTime;

        float frameRate = logicFrameStep / 1000f;

        //�������ܶ��Ͷ��

        while(fLocalAcc >= frameRate)
        {
            curFrame += logicFrameStep;

            UpdateLocalFrameCommand();

            if(mMainLogic != null)
            {
                mMainLogic.Update(frameRate);
            }

            fLocalAcc -= frameRate;
        }
    }

    void UpdateSyncFrame()
    {
        int framesNeedUpdate = (int)(endFrame - curFrame) / (int)logicFrameStep / 2;

        framesNeedUpdate = Mathf.Clamp(framesNeedUpdate, cFrameMin / (int)logicFrameStep, 100);

        debugCurFrameNeedUpdate = framesNeedUpdate;

        int curFrameNeedUpdate = framesNeedUpdate;

        long curClientTimeMs = (long)(Time.realtimeSinceStartup - timeStart) * GlobalLogic.VALUE_1000 * freamSpeed;
    }

    public void ClearCmdQueue()
    {
        frameQueue.Clear();
    }
}
