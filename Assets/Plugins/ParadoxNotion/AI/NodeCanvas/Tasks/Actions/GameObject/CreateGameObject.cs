using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Category("GameObject")]
    public class CreateGameObject : ActionTask
    {

        public BBParameter<string> objectName;
        public BBParameter<Vector3> position;
        public BBParameter<Vector3> rotation;

        [BlackboardOnly]
        public BBParameter<GameObject> saveAs;

        protected override void OnExecute() {
            var newGO = new GameObject(objectName.value);
            newGO.transform.position = position.value;
            newGO.transform.eulerAngles = rotation.value;
            saveAs.value = newGO;
            EndAction();
        }
    }
}