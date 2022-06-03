using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("Set Visibility")]
    [Category("GameObject")]
    public class SetObjectActive : ActionTask<Transform>
    {

        public enum SetActiveMode
        {
            Deactivate = 0,
            Activate = 1,
            Toggle = 2
        }

        public SetActiveMode setTo = SetActiveMode.Toggle;

        protected override string info {
            get { return string.Format("{0} {1}", setTo, agentInfo); }
        }

        protected override void OnExecute() {

            bool value;

            if ( setTo == SetActiveMode.Toggle ) {

                value = !agent.gameObject.activeSelf;

            } else {

                value = (int)setTo == 1;
            }

            agent.gameObject.SetActive(value);
            EndAction();
        }
    }
}