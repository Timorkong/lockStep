using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{

    [Category("GameObject")]
    public class IsActive : ConditionTask<Transform>
    {

        protected override bool OnCheck() {
            return agent.gameObject.activeInHierarchy;
        }
    }
}