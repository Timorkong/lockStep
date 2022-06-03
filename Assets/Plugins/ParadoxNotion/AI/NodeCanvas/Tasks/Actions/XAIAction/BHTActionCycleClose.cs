using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("迂回靠近目标")]
    [Category("NotSupport")]
    public class BHTActionCycleClose : ActionTask<Transform>
    {
        public BHTActionCycleClose()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_CYCLE_CLOSE;
        }

        [Name("参数")]
        public XBHTActionCycleCloseConfig closeTargetConfig = new XBHTActionCycleCloseConfig();

        protected override string info {
            get {
                string tempString;
                if (closeTargetConfig.cycleType == EnumXAIThinkType.DEFAULT)
                {
                    tempString =  string.Format("进入迂回状态 \n" + "迂回次数: [" + closeTargetConfig.count + "]\n" + "迂回类型: " + closeTargetConfig.actionType + "\n");
                } else
                {
                    tempString =  string.Format("进入随机迂回状态 \n" + "迂回次数min: [" + closeTargetConfig.minCount + "]\n"  + "迂回次数max:[" + closeTargetConfig.maxCount + "]\n" + "迂回类型: " + closeTargetConfig.actionType + "\n");
                }
                return tempString;
            }
        }


        public BHTActionCycleCloseConfig ToJsonData()
        {
            BHTActionCycleCloseConfig cfg = new BHTActionCycleCloseConfig();
            cfg.config = this.closeTargetConfig;
            return cfg;
        }
    }


    [System.Serializable]
    public class XBHTActionCycleCloseConfig
    {
        [Name("选择迂回动作")]
        public EnumBTNodeCloseAction actionType = EnumBTNodeCloseAction.远离;

        [Name("迂回选择")]
        [SerializeField]
        public EnumXAIThinkType cycleType = EnumXAIThinkType.DEFAULT;

        [ShowIf("cycleType", (int)EnumXAIThinkType.DEFAULT)]
        [Name("固定迂回次数")]
        [SerializeField]
        public float count = 0;

        [ShowIf("cycleType", (int)EnumXAIThinkType.RANDOM)]
        [Name("Min")]
        [SerializeField]
        public float minCount = 0;

        [ShowIf("cycleType", (int)EnumXAIThinkType.RANDOM)]
        [Name("Max")]
        [SerializeField]
        public float maxCount = 5;


    }

    [System.Serializable]
    public class BHTActionCycleCloseConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTActionCycleCloseConfig config;
    }
}