using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [System.Obsolete("Use Get Property instead")]
    [Category("GameObject")]
    public class GetGameObjectPosition : ActionTask<Transform>
    {

        [BlackboardOnly]
        public BBParameter<Vector3> saveAs;

        protected override string info {
            get { return "Get " + agentInfo + " position as " + saveAs; }
        }

        protected override void OnExecute() {
            saveAs.value = agent.position;
            EndAction();
        }
    }
}