using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.BehaviourTrees
{

    // [Name("Step Sequencer")]
    // [Category("Composites")]
    // [Category("组合节点")]
    [Category("暂不开放")]
    [Name("步骤迭代器", EnumCompositesPriority.StepIterator)]
    [Description("一对一执行并且立即返回子节点状态。迭代器始终向前移动一个并循环其索引")]
    // [Description("Executes AND immediately returns children node status ONE-BY-ONE. Step Sequencer always moves forward by one and loops it's index")]
    [Icon("StepIterator")]
    [Color("bf7fff")]
    public class StepIterator : BTComposite
    {

        private int current;

        public override void OnGraphStarted() {
            current = 0;
        }

        protected override Status OnExecute(Component agent, IBlackboard blackboard) {
            current = current % outConnections.Count;
            return outConnections[current].Execute(agent, blackboard);
        }

        protected override void OnReset() {
            current++;
        }
    }
}