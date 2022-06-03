using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.BehaviourTrees
{

    [Name("执行行为")]
    [Description(" 执行操作并返回成功或失败.\n 运行直到操作完成才返回.")]
    // [Icon("Action")]
    public class ActionNode : BTNode, ITaskAssignable<ActionTask>
    {
        [SerializeField]
        private ActionTask _action;
        
        public ActionNode()
        {
            _NodeType = EnumBTNodeType.AINODE_TYPE_ACTION;
        }

        public Task task {
            get { return action; }
            set { action = (ActionTask)value; }
        }

        public ActionTask action {
            get { return _action; }
            set { _action = value; }
        }

        public override string name {
            get { return base.name.ToUpper(); }
        }


        protected override Status OnExecute(Component agent, IBlackboard blackboard) {

            if ( action == null ) {
                return Status.Optional;
            }

            if ( status == Status.Resting || status == Status.Running ) {
                return action.ExecuteAction(agent, blackboard);
            }

            return status;
        }

        protected override void OnReset() {
            if ( action != null ) {
                action.EndAction(null);
            }
        }

        public override void OnGraphPaused() {
            if ( action != null ) {
                action.PauseAction();
            }
        }
    }
}