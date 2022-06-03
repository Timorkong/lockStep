using AINodeCanvas.Framework;
using AINodeCanvas.Framework.Internal;
using AIParadoxNotion;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.BehaviourTrees
{

    [Category("组合节点")]
    [Name("左右选择节点", EnumCompositesPriority.Selector)]
    [Description("根据条件执行的结果来选择执行(true)左节点或者(false)右节点")]
    [Icon("Condition")]
    [Color("b3ff7f")]
    public class BinarySelector : BTNode, ITaskAssignable<ConditionTask>
    {

        public bool dynamic;

        [SerializeField]
        private ConditionTask _condition;

        private int succeedIndex;

        public override int maxOutConnections { get { return 2; } }
        public override Alignment2x2 commentsAlignment { get { return Alignment2x2.Right; } }

        public BinarySelector()
        {
            _NodeType = EnumBTNodeType.AINODE_TYPE_BINARY_SELECT_CONTROL;
        }

        public override string name {
            get { return base.name.ToUpper(); }
        }

        public Task task {
            get { return condition; }
            set { condition = (ConditionTask)value; }
        }

        private ConditionTask condition {
            get { return _condition; }
            set { _condition = value; }
        }

        protected override Status OnExecute(Component agent, IBlackboard blackboard) {

            if ( condition == null || outConnections.Count < 2 ) {
                return Status.Optional;
            }

            if ( dynamic || status == Status.Resting ) {
                var lastIndex = succeedIndex;
                succeedIndex = condition.CheckCondition(agent, blackboard) ? 0 : 1;
                if ( succeedIndex != lastIndex ) {
                    outConnections[lastIndex].Reset();
                }
            }

            return outConnections[succeedIndex].Execute(agent, blackboard);
        }


        ////////////////////////////////////////
        ///////////GUI AND EDITOR STUFF/////////
        ////////////////////////////////////////
#if UNITY_EDITOR

        public override string GetConnectionInfo(int i) {
            return i == 0 ? "TRUE" : "FALSE";
        }

        protected override void OnNodeGUI() {
            if ( dynamic ) {
                GUILayout.Label("<b>DYNAMIC</b>");
            }
        }

#endif
    }
}