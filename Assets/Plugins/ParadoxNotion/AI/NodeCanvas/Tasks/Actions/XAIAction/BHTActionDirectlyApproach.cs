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
    [Name("直接靠近")]
    [Description("直接靠近目标")]
    [Category("AI行为")]
    [System.Serializable]
    public class BHTActionDirectlyApproach : ActionTask
    {
        public BHTActionDirectlyApproach()
        {
            detailType = EnumBTNodeAction.AINODE_DIRECTLY_APPROACH;
        }

        [Name("参数")]
        public XBHTDirectlyApproachConfig directlyConfig = new XBHTDirectlyApproachConfig();

        protected override string info
        {
            get {
                string tempString;
                if (directlyConfig.closeType == EnumCloseType.直接靠近)
                {
                    tempString = string.Format("直接靠近:[" + directlyConfig.closeType + "] 优先方式 " + "[" +
                    directlyConfig.directionType +
                    "]");
                }
                else
                {
                    tempString = string.Format("直接靠近:[" + directlyConfig.closeType + "]");
                }
                return tempString;
            }
        }


        public BHTDirectlyApproachConfig ToJsonData()
        {
            BHTDirectlyApproachConfig cfg = new BHTDirectlyApproachConfig();
            cfg.config.closeType = directlyConfig.closeType;
            cfg.config.directionType = directlyConfig.directionType;
            return cfg;
        }

        public Pbe.AIActionDirectlyApproachConfig ToProto()
        {
            Pbe.AIActionDirectlyApproachConfig cfg = new Pbe.AIActionDirectlyApproachConfig()
            {
                CloseType = (int)directlyConfig.closeType,
                DirectionType = (int)directlyConfig.directionType
            };
            return cfg;
        }
    }


    [System.Serializable]
    public class XBHTDirectlyApproachConfig
    {

        [Name("靠近方式")]
        [SerializeField]
        public EnumCloseType closeType = EnumCloseType.直接靠近;

        [Name("X/Z轴优先")]
        [SerializeField]
        [ShowIf("closeType",(int)EnumCloseType.直接靠近)]
        public EnumDirectionFirst directionType = EnumDirectionFirst.X轴优先;

    }

    [System.Serializable]
    public class BHTDirectlyApproachConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTDirectlyApproachConfig config = new XBHTDirectlyApproachConfig();
    }

}