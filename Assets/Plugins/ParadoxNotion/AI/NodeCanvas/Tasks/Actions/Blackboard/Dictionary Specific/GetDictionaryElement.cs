﻿using UnityEngine;
using System.Collections.Generic;
using AIParadoxNotion.Design;
using AINodeCanvas.Framework;

namespace AINodeCanvas.Tasks.Actions
{

    [Category("✫ Blackboard/Dictionaries")]
    public class GetDictionaryElement<T> : ActionTask
    {

        [BlackboardOnly]
        [RequiredField]
        public BBParameter<Dictionary<string, T>> dictionary;

        public BBParameter<string> key;

        [BlackboardOnly]
        public BBParameter<T> saveAs;

        protected override string info {
            get { return string.Format("{0} = {1}[{2}]", saveAs, dictionary, key); }
        }

        protected override void OnExecute() {
            if ( dictionary.value == null ) {
                EndAction(false);
                return;
            }
            saveAs.value = dictionary.value[key.value];
            EndAction();
        }
    }
}