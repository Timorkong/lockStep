using AINodeCanvas.Framework;
using AINodeCanvas.Framework.Internal;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{

    [Category("✫ Blackboard")]
    public class CheckEnum : ConditionTask
    {

        [BlackboardOnly]
        public BBObjectParameter valueA = new BBObjectParameter(typeof(System.Enum));
        public BBObjectParameter valueB = new BBObjectParameter(typeof(System.Enum));

        protected override string info {
            get { return valueA + " == " + valueB; }
        }

        protected override bool OnCheck() {
            return Equals(valueA.value, valueB.value);
        }

        ////////////////////////////////////////
        ///////////GUI AND EDITOR STUFF/////////
        ////////////////////////////////////////
#if UNITY_EDITOR

        protected override void OnTaskInspectorGUI() {
            DrawDefaultInspector();
            if ( GUI.changed && valueB.varType != valueA.refType ) {
                valueB.SetType(valueA.refType);
            }
        }

#endif
    }
}