using AINodeCanvas.Framework;
using AIParadoxNotion.Design;

namespace AINodeCanvas.Tasks.Actions
{

    [Category("✫ Blackboard")]
    public class SetVariable<T> : ActionTask
    {

        [BlackboardOnly]
        public BBParameter<T> valueA;
        public BBParameter<T> valueB;

        protected override string info {
            get { return valueA + " = " + valueB; }
        }

        protected override void OnExecute() {
            valueA.value = valueB.value;
            EndAction();
        }
    }
}