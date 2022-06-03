using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.BehaviourTrees
{

    [Name("条件限制")]
    [Description(" 判断一个条件，返回成功或失败")]
    // [Icon("Condition")]
    [Color("ff0000")]
    
    [System.Serializable]
    public class ConditionNode : BTNode, ITaskAssignable<ConditionTask>
    {
        public ConditionNode()
        {
            _NodeType = EnumBTNodeType.AINODE_TYPE_CONDITION;
        }

        [SerializeField]
        private ConditionTask _condition;

        public Task task {
            get { return condition; }
            set { condition = (ConditionTask)value; }
        }

        public ConditionTask condition {
            get { return _condition; }
            set { _condition = value; }
        }

        public override string name {
            get { return base.name.ToUpper(); }
        }

        protected override Status OnExecute(Component agent, IBlackboard blackboard) {
            if ( condition == null ) {
                return Status.Optional;
            }
            return condition.CheckCondition(agent, blackboard) ? Status.Success : Status.Failure;
        }
    }
}