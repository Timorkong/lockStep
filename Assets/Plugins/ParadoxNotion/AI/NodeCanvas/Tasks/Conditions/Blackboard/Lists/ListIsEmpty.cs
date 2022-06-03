using System.Collections;
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;


namespace AINodeCanvas.Tasks.Conditions
{

    [Category("✫ Blackboard/Lists")]
    public class ListIsEmpty : ConditionTask
    {

        [RequiredField]
        [BlackboardOnly]
        public BBParameter<IList> targetList;

        protected override string info {
            get { return string.Format("{0} Is Empty", targetList); }
        }

        protected override bool OnCheck() {
            return targetList.value.Count == 0;
        }
    }
}