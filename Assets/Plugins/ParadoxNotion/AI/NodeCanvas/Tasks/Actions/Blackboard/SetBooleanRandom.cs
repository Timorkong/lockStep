using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Category("✫ Blackboard")]
    [Description("Set a blackboard boolean variable at random between min and max value")]
    public class SetBooleanRandom : ActionTask
    {

        [BlackboardOnly]
        public BBParameter<bool> boolVariable;

        protected override string info {
            get { return "Set " + boolVariable + " Random"; }
        }

        protected override void OnExecute() {
            boolVariable.value = Random.Range(0, 2) == 0 ? false : true;
            EndAction();
        }
    }
}