using AINodeCanvas.Framework;
using AIParadoxNotion.Design;

namespace AINodeCanvas.Tasks.Conditions
{
    [ActorNode, SceneNode]
    [Category("条件判断")]
    [Name("总是返回假")]
    public class BHTPreconditionAlwaysFalse : ConditionTask
    {
        public BHTPreconditionAlwaysFalse()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_ALWAYS_FALSE;
        }

        protected override string info
        {
            get
            {
                return "FALSE";
            }
        }
    }
}