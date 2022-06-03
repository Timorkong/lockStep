using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{

    [Category("GameObject")]
    public class HasComponent<T> : ConditionTask<Transform> where T : Component
    {

        protected override bool OnCheck() {
            return agent.GetComponent<T>() != null;
        }
    }
}