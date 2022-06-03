﻿using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{

    [Category("✫ Blackboard")]
    [Description("Set a blackboard float variable at random between min and max value")]
    public class SetFloatRandom : ActionTask
    {

        public BBParameter<float> minValue;
        public BBParameter<float> maxValue;

        [BlackboardOnly]
        public BBParameter<float> floatVariable;

        protected override string info {
            get { return "Set " + floatVariable + " Random(" + minValue + ", " + maxValue + ")"; }
        }

        protected override void OnExecute() {
            floatVariable.value = Random.Range(minValue.value, maxValue.value);
            EndAction();
        }
    }
}