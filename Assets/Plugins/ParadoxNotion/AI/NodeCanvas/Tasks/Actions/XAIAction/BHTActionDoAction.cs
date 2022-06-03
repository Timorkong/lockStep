using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("执行动作")]
    [Description("根据技能ID执行对应的动作")]
    [Category("指令")]
    [System.Serializable]
    public class BHTActionDoAction : ActionTask
    {
        public BHTActionDoAction()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_DO_ACTION;
        }

        [Name("参数")]
        public XBHTDoActionConfig doActionConfig = new XBHTDoActionConfig();

        protected override string info
        {
            get
            {
                if(doActionConfig.breakable)
                    return string.Format("执行会被打断的动作: " + doActionConfig.actionId + " 时长: " + doActionConfig.duration);
                else
                    return string.Format("执行动作: " + doActionConfig.actionId + " 时长: " + doActionConfig.duration);
            }
        }

        virtual public Pbe.AIActionDoActionConfig ToProto()
        {
            Pbe.AIActionDoActionConfig cfg = new Pbe.AIActionDoActionConfig()
            {
                ActionId = doActionConfig.actionId,
                Duration = doActionConfig.duration,
                Breakable = doActionConfig.breakable
            };
            return cfg;
        }
    }

    [SceneNode]
    [Name("执行动作")]
    [Description("根据技能ID执行对应的动作")]
    [Category("指令")]
    [System.Serializable]
    public class BHTActionDoActionSN : BHTActionDoAction
    {
        [BlackboardOnly]
        public BBParameter<int> unitID;

        override public Pbe.AIActionDoActionConfig ToProto()
        {
            Pbe.AIActionDoActionConfig cfg = new Pbe.AIActionDoActionConfig()
            {
                ActionId = doActionConfig.actionId,
                Duration = doActionConfig.duration,
                Breakable = doActionConfig.breakable,
                UID = new Pbe.BBParam()
                {
                    Name = unitID.name,
                    Type = XAIConfigTool.Type2TypeId(unitID.varType)
                }
            };
            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTDoActionConfig
    {
        [Name("动作(技能) ID")]
        [SerializeField]
        public int actionId = 0;
        [Name("Loop动作的持续时间(ms)")]
        [SerializeField]
        public int duration = 0;
        [Name("被击是否中断当前剧情树")]
        [SerializeField]
        public bool breakable = false;
    }
}
