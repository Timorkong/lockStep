using AINodeCanvas.Framework;
using AIParadoxNotion.Design;


namespace AINodeCanvas.Tasks.Conditions
{

    [Category("✫ Blackboard")]
    public class CheckString : ConditionTask
    {

        [BlackboardOnly]
        public BBParameter<string> valueA;
        public BBParameter<string> valueB;

        protected override string info {
            get { return valueA + " == " + valueB; }
        }

        protected override bool OnCheck() {
            return valueA.value == valueB.value;
        }
    }
}