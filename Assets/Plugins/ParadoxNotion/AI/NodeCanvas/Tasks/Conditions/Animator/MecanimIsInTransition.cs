using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{

    [Name("Is In Transition")]
    [Category("Animator")]
    public class MecanimIsInTransition : ConditionTask<Animator>
    {

        public BBParameter<int> layerIndex;

        protected override string info {
            get { return "Mec.Is In Transition"; }
        }

        protected override bool OnCheck() {

            return agent.IsInTransition(layerIndex.value);
        }
    }
}