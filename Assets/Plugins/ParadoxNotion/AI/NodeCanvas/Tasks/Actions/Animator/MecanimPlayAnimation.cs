using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("Play Animation")]
    [Category("Animator")]
    public class MecanimPlayAnimation : ActionTask<Animator>
    {

        public BBParameter<int> layerIndex;
        [RequiredField]
        public BBParameter<string> stateName;
        [SliderField(0, 1)]
        public float transitTime = 0.25f;
        public bool waitUntilFinish;

        private AnimatorStateInfo stateInfo;
        private bool played;

        protected override string info {
            get { return "Anim '" + stateName.ToString() + "'"; }
        }

        protected override void OnExecute() {
            if ( string.IsNullOrEmpty(stateName.value) ) {
                EndAction();
                return;
            }
            played = false;
            var current = agent.GetCurrentAnimatorStateInfo(layerIndex.value);
            agent.CrossFade(stateName.value, transitTime / current.length, layerIndex.value);
        }

        protected override void OnUpdate() {

            stateInfo = agent.GetCurrentAnimatorStateInfo(layerIndex.value);

            if ( waitUntilFinish ) {

                if ( stateInfo.IsName(stateName.value) ) {

                    played = true;
                    if ( elapsedTime >= ( stateInfo.length / agent.speed ) ) {
                        EndAction();
                    }

                } else if ( played ) {

                    EndAction();
                }

            } else {

                if ( elapsedTime >= transitTime ) {
                    EndAction();
                }
            }
        }
    }
}