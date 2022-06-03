
/*
 * AI 行为 Z 字靠近
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("Z字靠近")]
    [Description("以Z字的形状起伏靠近目标")]
    [Category("AI行为")]
    [System.Serializable]
    public class BHTActionZigApproach : ActionTask
    {
        public BHTActionZigApproach()
        {
            detailType = EnumBTNodeAction.AINODE_ZIG_APPROACH;
        }

        [Name("参数")]
        public XBHTZigApproachConfig zigConfig = new XBHTZigApproachConfig();

        protected override string info
        {
            get
            {
                return string.Format("Z字靠近:[斜向角度 " + zigConfig.angle + "]");
            }
        }

        public BHTZigApproachConfig ToJsonData()
        {
            BHTZigApproachConfig cfg = new BHTZigApproachConfig();
            cfg.config.angle = zigConfig.angle == 0.0f ? 45.0f : zigConfig.angle;
            return cfg;
        }

        public Pbe.AIActionZigApproachConfig ToProto()
        {
            Pbe.AIActionZigApproachConfig cfg = new Pbe.AIActionZigApproachConfig()
            {
                Angle = zigConfig.angle
            };
            return cfg;
        }

    }

    [System.Serializable]
    public class XBHTZigApproachConfig
    {
        [Name("斜向角度（度）")]
        [SliderField(0.0f, 90.0f)]
        [SerializeField]
        public float angle = 0.0f;
    }


    [System.Serializable]
    public class BHTZigApproachConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTZigApproachConfig config = new XBHTZigApproachConfig();
    }
}