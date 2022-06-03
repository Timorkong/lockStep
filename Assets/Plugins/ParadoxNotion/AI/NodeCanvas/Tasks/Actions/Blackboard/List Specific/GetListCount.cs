﻿using System.Collections;
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;


namespace AINodeCanvas.Tasks.Actions
{

    [Category("✫ Blackboard/Lists")]
    public class GetListCount : ActionTask
    {

        [RequiredField]
        [BlackboardOnly]
        public BBParameter<IList> targetList;
        [BlackboardOnly]
        public BBParameter<int> saveAs;

        protected override string info {
            get { return string.Format("{0} = {1}.Count", saveAs, targetList); }
        }

        protected override void OnExecute() {
            saveAs.value = targetList.value.Count;
            EndAction(true);
        }
    }
}