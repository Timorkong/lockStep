using AINodeCanvas.Framework;
using AIParadoxNotion;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Conditions
{

    [Category("✫ Blackboard")]
    public class CheckVectorDistance : ConditionTask
    {

        [BlackboardOnly]
        public BBParameter<Vector3> vectorA;
        [BlackboardOnly]
        public BBParameter<Vector3> vectorB;
        public CompareMethod comparison = CompareMethod.EqualTo;
        public BBParameter<float> distance;

        protected override string info {
            get { return string.Format("Distance ({0}, {1}) {2} {3}", vectorA, vectorB, OperationTools.GetCompareString(comparison), distance); }
        }

        protected override bool OnCheck() {
            var d = Vector3.Distance(vectorA.value, vectorB.value);
            return OperationTools.Compare((float)d, (float)distance.value, comparison, 0f);
        }
    }
}