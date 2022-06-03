using AINodeCanvas.Framework;
using AIParadoxNotion;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{

    [Name("Check Parameter Int")]
    [Category("Animator")]
    public class MecanimCheckInt : ConditionTask<Animator>
    {

        [RequiredField]
        public BBParameter<string> parameter;
        public CompareMethod comparison = CompareMethod.EqualTo;
        public BBParameter<int> value;

        protected override string info {
            get
            {
                return "Mec.Int " + parameter.ToString() + OperationTools.GetCompareString(comparison) + value;
            }
        }

        protected override bool OnCheck() {
            return OperationTools.Compare((int)agent.GetInteger(parameter.value), (int)value.value, comparison);
        }
    }
}