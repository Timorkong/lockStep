using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using System.Collections;

namespace AINodeCanvas.Tasks.Actions
{

    [Category("✫ Blackboard")]
    [Description("Triggers a boolean variable for 1 frame to True then back to False")]
    public class TriggerBoolean : ActionTask
    {

        [RequiredField]
        [BlackboardOnly]
        public BBParameter<bool> variable;

        protected override string info {
            get { return string.Format("Trigger {0}", variable); }
        }

        protected override void OnExecute() {
            if ( variable.value == false ) {
                variable.value = true;
                StartCoroutine(Flip());
            }
            EndAction();
        }

        IEnumerator Flip() {
            yield return null;
            variable.value = false;
        }
    }
}