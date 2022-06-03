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
    [ActorNode]
    [Name("游走")]
    [Description("目标游走/自身游走")]
    [Category("AI行为")]
    [System.Serializable]
    public class BHTActionWander : ActionTask
    {
        public BHTActionWander()
        {
            detailType = EnumBTNodeAction.AINODE_WANDER;
        }

        [Name("参数")]
        public XBHTWanderConfig wanderConfig = new XBHTWanderConfig();

        protected override string info
        {
            get {
                string tempString = string.Format("游走:[" + wanderConfig.wanderType + "] 移动方式 "+ "[" +
                    wanderConfig.moveType +
                    "]");

                return tempString;
            }
        }


        public BHTWanderConfig ToJsonData()
        {
            BHTWanderConfig cfg = new BHTWanderConfig();
            cfg.config.wanderType = wanderConfig.wanderType;
            cfg.config.moveType = wanderConfig.moveType;
            return cfg;
        }

        public Pbe.AIActionWanderConfig ToProto()
        {
            Pbe.AIActionWanderConfig cfg = new Pbe.AIActionWanderConfig()
            {
                WanderType = (int)wanderConfig.wanderType,
                MoveType = (int)wanderConfig.moveType
            };
            return cfg;
        }
    }


    [System.Serializable]
    public class XBHTWanderConfig
    {

        [Name("游走目标")]
        [SerializeField]
        public EnumWanderTarget wanderType = EnumWanderTarget.自身游走;

        [Name("移动方式")]
        [SerializeField]
        public EnumXAIMoveType moveType  = EnumXAIMoveType.WALK;

    }

    [System.Serializable]
    public class BHTWanderConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTWanderConfig config = new XBHTWanderConfig();
    }

}