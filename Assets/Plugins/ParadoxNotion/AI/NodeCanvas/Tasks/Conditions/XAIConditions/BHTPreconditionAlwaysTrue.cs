using AINodeCanvas.Framework;
using AIParadoxNotion.Design;

namespace AINodeCanvas.Tasks.Conditions
{
    [ActorNode, SceneNode]
    [Category("条件判断")]
    [Name("总是返回真")]
    public class BHTPreconditionAlwaysTrue : ConditionTask
    {
        public BHTPreconditionAlwaysTrue()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_ALWAYS_TRUE;
        }

        protected override string info
        {
            get
            {
                return "TRUE";
            }
        }
    }
}