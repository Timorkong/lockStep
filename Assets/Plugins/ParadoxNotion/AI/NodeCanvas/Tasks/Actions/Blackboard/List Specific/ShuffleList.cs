using System.Collections;
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Category("✫ Blackboard/Lists")]
    public class ShuffleList : ActionTask
    {

        [RequiredField]
        [BlackboardOnly]
        public BBParameter<IList> targetList;

        protected override void OnExecute() {

            var list = targetList.value;

            for ( var i = list.Count - 1; i > 0; i-- ) {
                var j = (int)Mathf.Floor(Random.value * ( i + 1 ));
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }

            EndAction();
        }
    }
}