using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Category("✫ Blackboard")]
    [Description("Create up to 3 floats from a Vector and save them to blackboard")]
    public class DecomposeVector : ActionTask
    {

        public BBParameter<Vector3> targetVector;
        [BlackboardOnly]
        public BBParameter<float> x;
        [BlackboardOnly]
        public BBParameter<float> y;
        [BlackboardOnly]
        public BBParameter<float> z;

        protected override string info {
            get { return "Decompose Vector " + targetVector; }
        }

        protected override void OnExecute() {
            x.value = targetVector.value.x;
            y.value = targetVector.value.y;
            z.value = targetVector.value.z;
            EndAction();
        }
    }
}