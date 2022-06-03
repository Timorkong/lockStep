using UnityEngine;
using AINodeCanvas.Framework;
using AIParadoxNotion;
using AIParadoxNotion.Design;


namespace AINodeCanvas.Tasks.Conditions
{

    [Category("✫ Blackboard")]
    [Description("It's best to use the respective Condition for a type if existant since they support operations as well")]
    public class CheckVariable<T> : ConditionTask
    {

        [BlackboardOnly]
        public BBParameter<T> valueA;
        public BBParameter<T> valueB;

        protected override string info {
            get { return valueA + " == " + valueB; }
        }

        protected override bool OnCheck() {
            if ( typeof(Object).RTIsAssignableFrom(typeof(T)) ) { //special treat for unity objects
                return ( valueA.value as Object ) == ( valueB.value as Object );
            }
            return Equals(valueA.value, valueB.value);
        }
    }
}