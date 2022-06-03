using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Category("✫ Blackboard")]
    public class NormalizeVector : ActionTask
    {

        public BBParameter<Vector3> targetVector;
        public BBParameter<float> multiply = 1;

        protected override void OnExecute() {
            targetVector.value = targetVector.value.normalized * multiply.value;
            EndAction(true);
        }
    }
}