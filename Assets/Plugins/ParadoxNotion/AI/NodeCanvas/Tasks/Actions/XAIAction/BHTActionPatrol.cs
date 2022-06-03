/*
 * @Descripttion: AI行为-巡逻
 * @Author: kevinwu
 * @Date: 2019-11-20 10:58:21
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;
using AINodeCanvas.Tasks.Conditions;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("执行巡逻")]
    [Category("NotSupport")]
    [System.Serializable]
    public class BHTActionPatrol : ActionTask<Transform>
    {
        public BHTActionPatrol()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_PATROL;
        }
        [Name("参数")]
        public XBHTPatrolConfig patrolConfig = new XBHTPatrolConfig();

        protected override string info {
            get {
                string tempString = "进入巡逻,";
                if (patrolConfig.limitNode == 0)
                {
                    tempString += "不打断";
                }
                else
                {
                    tempString += "被限制节点[" + patrolConfig.limitNode + "]打断";
                }
                return tempString;
                }
        }

        public BHTActionPatrolConfig ToJsonData()
        {
            BHTActionPatrolConfig cfg = new BHTActionPatrolConfig();
            cfg.config = this.patrolConfig;
            cfg.config.limitNode = patrolConfig.limitNode;
            cfg.config.breakType = patrolConfig.breakType;
            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTPatrolConfig
    {
        [Name("选择巡逻动作")]
        [SerializeField]
        public EnumXAIMoveType moveType = EnumXAIMoveType.WALK;

        [Name("打断条件(0不打断)")]
        [SerializeField]
        public int limitNode = 0;

        [Name("打断类型")]
        [SerializeField]
        public EnumBreakType breakType = EnumBreakType.绝对距离;
    }

    [System.Serializable]
    public class BHTActionPatrolConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTPatrolConfig config;
        public XBHTConditionXandYConig limitConfigAbsolute = new XBHTConditionXandYConig();
        public XBHTConditionDistanceConig limitConfigRelative = new XBHTConditionDistanceConig();
    }
}