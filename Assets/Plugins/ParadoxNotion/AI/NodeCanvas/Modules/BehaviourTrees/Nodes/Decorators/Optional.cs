using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.BehaviourTrees
{

    [Name("Optional")]
    [Category("暂不开放")]
    // [Category("Decorators")]
    [Description("Executes the decorated node without taking into account it's return status, thus making it optional to the parent node for whether it returns Success or Failure.\nThis has the same effect as disabling the node, but instead it executes normaly")]
    [Icon("UpwardsArrow")]
    public class Optional : BTDecorator
    {

        protected override Status OnExecute(Component agent, IBlackboard blackboard) {

            if ( decoratedConnection == null ) {
                return Status.Optional;
            }

            if ( status == Status.Resting ) {
                decoratedConnection.Reset();
            }

            status = decoratedConnection.Execute(agent, blackboard);
            return status == Status.Running ? Status.Running : Status.Optional;
        }
    }
}