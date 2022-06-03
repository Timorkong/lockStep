using System.Collections.Generic;
using System.Linq;
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

#if UNITY_5_5_OR_NEWER
using NavMeshAgent = UnityEngine.AI.NavMeshAgent;
using NavMeshPath = UnityEngine.AI.NavMeshPath;
using NavMeshPathStatus = UnityEngine.AI.NavMeshPathStatus;
#endif

namespace AINodeCanvas.Tasks.Conditions
{

    [Category("Movement")]
    [Description("Check if a path exists for the agent and optionaly save the resulting path positions")]
    public class PathExists : ConditionTask<NavMeshAgent>
    {

        public BBParameter<Vector3> targetPosition;
        [BlackboardOnly]
        public BBParameter<List<Vector3>> savePathAs;

        protected override bool OnCheck() {
            var path = new NavMeshPath();
            agent.CalculatePath(targetPosition.value, path);
            savePathAs.value = path.corners.ToList();
            return path.status == NavMeshPathStatus.PathComplete;
        }
    }
}