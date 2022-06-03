using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Category("GameObject")]
    [Description("Note that this is very slow")]
    public class FindObjectOfType<T> : ActionTask where T : Component
    {

        [BlackboardOnly]
        public BBParameter<T> saveComponentAs;
        [BlackboardOnly]
        public BBParameter<GameObject> saveGameObjectAs;

        protected override void OnExecute() {
            var o = Object.FindObjectOfType<T>();
            if ( o != null ) {
                saveComponentAs.value = o;
                saveGameObjectAs.value = o.gameObject;
                EndAction(true);
                return;
            }

            EndAction(false);
        }
    }
}