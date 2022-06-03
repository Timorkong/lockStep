using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("靠近目标")]
    [Category("NotSupport")]
    public class BHTActionCloseTarget : ActionTask<Transform>
    {
        public BHTActionCloseTarget()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_CLOST_TARGET;
        }

        [Name("参数")]
        public XBHTActionCloseTargetConfig closeTargetConfig = new XBHTActionCloseTargetConfig();

        protected override string info {
            get {
                string tempString;
                if (closeTargetConfig.timeType == EnumXAIThinkType.DEFAULT)
                {
                    tempString = string.Format("靠近目标[" + closeTargetConfig.moveType + "] 持续" + closeTargetConfig.tickNum + "s");
                } else
                {
                    tempString = string.Format("随机时间靠近目标[" + closeTargetConfig.moveType + "] 持续\n" + 
                        "min:[" + closeTargetConfig.tickNumRandomMin + "s]\n"+ 
                         "max:[" + closeTargetConfig.tickNumRandomMax + "s]");
                }
                return tempString;
            }
        }

        // protected override void OnExecute() {

        //     EndAction();
        // }

        public BHTActionCloseTargetConfig ToJsonData()
        {
            BHTActionCloseTargetConfig cfg = new BHTActionCloseTargetConfig();
            cfg.config.tickNum = Mathf.Floor(this.closeTargetConfig.tickNum * BehaviourTreeDefine.TimeFrame);
            cfg.config.moveType = closeTargetConfig.moveType;
            cfg.config.timeType = closeTargetConfig.timeType;
            cfg.config.tickNumRandomMin = closeTargetConfig.tickNumRandomMin;
            cfg.config.tickNumRandomMax = closeTargetConfig.tickNumRandomMax;
            return cfg;
        }
    }


    [System.Serializable]
    public class XBHTActionCloseTargetConfig
    {
        [Name("选择巡逻动作")]
        [SerializeField]
        public EnumXAIMoveType moveType = EnumXAIMoveType.WALK;

        [Name("持续时间")]
        [SerializeField]
        public EnumXAIThinkType timeType = EnumXAIThinkType.DEFAULT;

        [ShowIf("timeType",(int)EnumXAIThinkType.DEFAULT)]
        [Name("持续时间(默认5s)")]
        [SerializeField]
        public float tickNum = 5;

        [ShowIf("timeType", (int)EnumXAIThinkType.RANDOM)]
        [Name("Min")]
        [SerializeField]
        public float tickNumRandomMin = 0;

        [ShowIf("timeType", (int)EnumXAIThinkType.RANDOM)]
        [Name("Max")]
        [SerializeField]
        public float tickNumRandomMax = 5;


    }

    [System.Serializable]
    public class BHTActionCloseTargetConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTActionCloseTargetConfig config = new XBHTActionCloseTargetConfig();
    }
}