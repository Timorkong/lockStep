using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{

    [Category("GameObject")]
    [Description("Find a transform child by name within the agent's transform")]
    public class FindChildByName : ActionTask<Transform>
    {

        [RequiredField]
        public BBParameter<string> childName;

        [BlackboardOnly]
        public BBParameter<Transform> saveAs;

        protected override string info {
            get { return string.Format("{0} = {1}.FindChild({2})", saveAs, agentInfo, childName); }
        }

        protected override void OnExecute() {
            var result = agent.Find(childName.value);
            saveAs.value = result;
            EndAction(result != null);
        }
    }
}