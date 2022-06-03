using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{

    [Category("✫ Utility")]
    [Description("Return true or false based on the probability settings. Outcome is calculated EACH time this is checked")]
    public class Probability : ConditionTask
    {

        public BBParameter<float> probability = 0.5f;
        public BBParameter<float> maxValue = 1;

        protected override string info {
            get { return ( probability.value / maxValue.value * 100 ) + "%"; }
        }

        protected override bool OnCheck() {
            return Random.Range(0f, maxValue.value) <= probability.value;
        }
    }
}