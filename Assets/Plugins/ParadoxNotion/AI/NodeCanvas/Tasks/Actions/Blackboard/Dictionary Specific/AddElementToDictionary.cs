﻿using UnityEngine;
using System.Collections.Generic;
using AIParadoxNotion.Design;
using AINodeCanvas.Framework;

namespace AINodeCanvas.Tasks.Actions
{

    [Category("✫ Blackboard/Dictionaries")]
    public class AddElementToDictionary<T> : ActionTask
    {

        [BlackboardOnly]
        [RequiredField]
        public BBParameter<Dictionary<string, T>> dictionary;

        public BBParameter<string> key;
        public BBParameter<T> value;

        protected override string info {
            get { return string.Format("{0}[{1}] = {2}", dictionary, key, value); }
        }

        protected override void OnExecute() {
            if ( dictionary.value == null ) {
                EndAction(false);
                return;
            }
            dictionary.value[key.value] = value.value;
            EndAction();
        }
    }
}