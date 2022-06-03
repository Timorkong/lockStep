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
    [Name("远离目标")]
    [Description("远离目标/逃跑")]
    [Category("AI行为")]
    [System.Serializable]
    public class BHTActionAwayTarget : ActionTask
    {
        public BHTActionAwayTarget()
        {
            detailType = EnumBTNodeAction.AINODE_AWAY_TARGET;
        }

        [Name("参数")]
        public XBHTAwayTargetConfig directlyConfig = new XBHTAwayTargetConfig();

        protected override string info
        {
            get {
                string tempString = string.Format("远离目标:[" + directlyConfig.faceType + "] 移动方式 "+ "[" +
                    directlyConfig.moveType +
                    "]");

                return tempString;
            }
        }


        public BHTAwayTargetConfig ToJsonData()
        {
            BHTAwayTargetConfig cfg = new BHTAwayTargetConfig();
            cfg.config.faceType = directlyConfig.faceType;
            cfg.config.moveType = directlyConfig.moveType;
            return cfg;
        }

        public Pbe.AIActionAwayTargetConfig ToProto()
        {
            Pbe.AIActionAwayTargetConfig cfg = new Pbe.AIActionAwayTargetConfig()
            {
                FaceType = (int)directlyConfig.faceType,
                MoveType = (int)directlyConfig.moveType
            };
            return cfg;
        }
    }


    [System.Serializable]
    public class XBHTAwayTargetConfig
    {

        [Name("远离面向")]
        [SerializeField]
        public EnumFaceTo faceType = EnumFaceTo.面向目标;

        [Name("移动方式")]
        [SerializeField]
        public EnumXAIMoveType moveType  = EnumXAIMoveType.WALK;

    }

    [System.Serializable]
    public class BHTAwayTargetConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTAwayTargetConfig config = new XBHTAwayTargetConfig();
    }

}