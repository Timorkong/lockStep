using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.BehaviourTrees
{
    [Category("暂不开放")]
    // [Category("Composites")]
    // [Category("组合节点")]
    [Description("Works like a normal Selector, but when a child node returns Success, that child will be moved to the end.\nAs a result, previously Failed children will always be checked first and recently Successful children last")]
    [Icon("FlipSelector")]
    [Color("b3ff7f")]
    public class FlipSelector : BTComposite
    {

        private int current;

        protected override Status OnExecute(Component agent, IBlackboard blackboard) {

            for ( var i = current; i < outConnections.Count; i++ ) {

                status = outConnections[i].Execute(agent, blackboard);

                if ( status == Status.Running ) {
                    current = i;
                    return Status.Running;
                }

                if ( status == Status.Success ) {
                    SendToBack(i);
                    return Status.Success;
                }
            }

            return Status.Failure;
        }

        void SendToBack(int i) {
            var c = outConnections[i];
            outConnections.RemoveAt(i);
            outConnections.Add(c);
        }

        protected override void OnReset() {
            current = 0;
        }
    }
}