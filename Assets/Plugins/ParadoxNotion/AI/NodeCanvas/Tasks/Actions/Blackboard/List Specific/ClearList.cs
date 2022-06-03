using System.Collections;
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;


namespace AINodeCanvas.Tasks.Actions
{

    [Category("✫ Blackboard/Lists")]
    public class ClearList : ActionTask
    {

        [RequiredField]
        [BlackboardOnly]
        public BBParameter<IList> targetList;

        protected override string info {
            get { return string.Format("Clear List {0}", targetList); }
        }

        protected override void OnExecute() {
            targetList.value.Clear();
            EndAction(true);
        }
    }
}