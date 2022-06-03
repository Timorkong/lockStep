﻿using System.Collections.Generic;
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;

namespace AINodeCanvas.Tasks.Actions
{

    [Category("✫ Blackboard/Lists")]
    public class GetIndexOfElement<T> : ActionTask
    {
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<List<T>> targetList;
        public BBParameter<T> targetElement;
        [BlackboardOnly]
        public BBParameter<int> saveIndexAs;

        protected override void OnExecute() {

            saveIndexAs.value = targetList.value.IndexOf(targetElement.value);
            EndAction(true);
        }
    }
}