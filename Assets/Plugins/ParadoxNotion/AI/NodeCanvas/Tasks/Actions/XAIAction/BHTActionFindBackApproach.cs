using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;
using AINodeCanvas.Tasks.Conditions;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("找背靠近")]
    [Description("靠近目标的背部")]
    [Category("AI行为")]
    [System.Serializable]
    public class BHTActionFindBackApproach : ActionTask
    {
        public BHTActionFindBackApproach()
        {
            detailType = EnumBTNodeAction.AINODE_FINDBACK_APPROACH;
        }

        [Name("参数")]
        public XBHTFindBackApproachConfig directlyConfig = new XBHTFindBackApproachConfig();

        public BHTFindBackApproachConfig ToJsonData()
        {
            BHTFindBackApproachConfig cfg = new BHTFindBackApproachConfig();
            return cfg;
        }

        public Pbe.AIActionFindBackApproachConfig ToProto()
        {
            Pbe.AIActionFindBackApproachConfig cfg = new Pbe.AIActionFindBackApproachConfig();
            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTFindBackApproachConfig
    {
    }

    [System.Serializable]
    public class BHTFindBackApproachConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTFindBackApproachConfig config = new XBHTFindBackApproachConfig();
    }
}
