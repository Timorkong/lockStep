using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnmuSyncMode
{
    None,
    /// <summary>
    /// 本地测试
    /// </summary>
    LocalFrame,
    /// <summary>
    /// 服务器同步
    /// </summary>
    SyncFrame,
}

public enum EnumFrameSyncState
{
    /// <summary>
    /// 创建链接
    /// </summary>
    OnCreate,
    /// <summary>
    /// 开始同步
    /// </summary>
    OnStart,
    /// <summary>
    /// 同步中
    /// </summary>
    OnTick,
    /// <summary>
    /// 重连
    /// </summary>
    OnReconnect,
    /// <summary>
    /// 结束
    /// </summary>
    OnEnd,
}

public class FrameSync:Singleton<FrameSync>
{
    protected Queue<IFrameCommand> frameQueue = new Queue<IFrameCommand>();

    const int cFrameMin = 2;

    static int debugCurFrameNeedUpdate = 0;

    private EnmuSyncMode syncMode = EnmuSyncMode.LocalFrame;

    private EnumFrameSyncState frameSyncState = EnumFrameSyncState.OnCreate;

    private LogicWorld mLogicWorld = null;

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
 
    public void SetLogicWorld(LogicWorld logicWorld)
    {
        mLogicWorld = logicWorld;
    }
    
    public void StartFrameSync()
    {
        Debug.LogError("[帧同步] 开始帧同步");
    }

    public void FirtFrameCommand(IFrameCommand cmd , bool force = false)
    {
        if (!isFire) return;

        switch (syncMode)
        {
            case EnmuSyncMode.LocalFrame:
                {
                    frameQueue.Enqueue(cmd);

                    BaseFrameCommand baseFrame = cmd as BaseFrameCommand;

                    baseFrame.seat = 0;

                    break;
                }
            case EnmuSyncMode.SyncFrame:
                {
                    break;
                }
        }
    }

    public void UpdateFrame()
    {
        switch (syncMode)
        {
            case EnmuSyncMode.LocalFrame:
                {
                    UpdateLocalFrame();

                    break;
                }
            case EnmuSyncMode.SyncFrame:
                {
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

                    Debug.LogFormat("[帧同步] 收到 数据类型 {0}", frameCmdID);

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

        //本地能跑多块就多块

        while(fLocalAcc >= frameRate)
        {
            curFrame += logicFrameStep;

            UpdateLocalFrameCommand();

            if(mLogicWorld != null)
            {
                mLogicWorld.Update(frameRate);
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
