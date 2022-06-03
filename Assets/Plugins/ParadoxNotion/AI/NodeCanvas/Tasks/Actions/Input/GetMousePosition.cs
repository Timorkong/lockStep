using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Category("Input")]
    public class GetMousePosition : ActionTask
    {

        [BlackboardOnly]
        public BBParameter<Vector3> saveAs;
        public bool repeat;


        protected override void OnExecute() { Do(); }
        protected override void OnUpdate() { Do(); }

        void Do() {
            saveAs.value = Input.mousePosition;
            if ( !repeat )
                EndAction();
        }
    }
}