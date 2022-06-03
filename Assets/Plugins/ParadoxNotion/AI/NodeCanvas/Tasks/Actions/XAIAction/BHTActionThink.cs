/*
 * @Descripttion: AI行为-巡逻
 * @Author: colecai
 * @Date: 2019-11-28 10:58:21
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;
using AINodeCanvas.Tasks.Conditions;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("思考")]
    [Description("思考等待，等待时间取决于AI的智力")]
    [Category("NotSupport")]
    [System.Serializable]
    public class BHTActionThink : ActionTask<Transform>
    {
        public BHTActionThink()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_THINK;
        }

        [Name("参数")]
        public XBHTActionThinkConfig thinkConfig = new XBHTActionThinkConfig();

        protected override string info
        {
            get {
                string tempString;
                if (thinkConfig.thinkType == EnumXAIThinkType.DEFAULT)
                {
                    tempString =  string.Format("[" + thinkConfig.thinkType + "]:根据智力停止");

                }else
                {
                    tempString  = string.Format("[" + thinkConfig.thinkType + "] 持续" +
                        "min: " + thinkConfig.minThink + "s" + "max: " + thinkConfig.maxThink + "s");
                }
                if (thinkConfig.limitNode == 0)
                {
                    tempString += "不打断";
                } else
                {
                    tempString += "被限制节点[" + thinkConfig.limitNode + "]打断";
                }
                return tempString;
            }
        }


        public BHTActionThinkConfig ToJsonData()
        {
            BHTActionThinkConfig cfg = new BHTActionThinkConfig();
            cfg.config.thinkType = thinkConfig.thinkType;
            cfg.config.minThink = thinkConfig.minThink;
            cfg.config.maxThink = thinkConfig.maxThink;
            cfg.config.limitNode = thinkConfig.limitNode;
            cfg.config.breakType = thinkConfig.breakType;
            return cfg;
        }
    }


    [System.Serializable]
    public class XBHTActionThinkConfig
    {
        [Name("思考模式")]
        [SerializeField]
        public EnumXAIThinkType thinkType  = EnumXAIThinkType.DEFAULT;

        [ShowIf("thinkType", (int)EnumXAIThinkType.RANDOM)]
        [Name("min")]
        [SerializeField]
        public float minThink = 0;
        
        [ShowIf("thinkType", (int)EnumXAIThinkType.RANDOM)]
        [Name("max")]
        [SerializeField]
        public float maxThink = 0;

        [Name("打断条件(0不打断)")]
        [SerializeField]
        public int limitNode = 0;

        [Name("打断类型")]
        [SerializeField]
        public EnumBreakType breakType = EnumBreakType.绝对距离;
    }

    [System.Serializable]
    public class BHTActionThinkConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTActionThinkConfig config = new XBHTActionThinkConfig();
        public XBHTConditionXandYConig limitConfigAbsolute = new XBHTConditionXandYConig();
        public XBHTConditionDistanceConig limitConfigRelative = new XBHTConditionDistanceConig();
    }

}