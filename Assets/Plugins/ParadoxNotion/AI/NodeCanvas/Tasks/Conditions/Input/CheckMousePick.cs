using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{

    [Category("Input")]
    public class CheckMousePick : ConditionTask
    {

        public AIParadoxNotion.ButtonKeys buttonKey;
        [LayerField]
        public int layer;

        [BlackboardOnly]
        public BBParameter<GameObject> saveGoAs;
        [BlackboardOnly]
        public BBParameter<float> saveDistanceAs;
        [BlackboardOnly]
        public BBParameter<Vector3> savePosAs;

        private RaycastHit hit;

        protected override string info {
            get
            {
                var finalString = buttonKey.ToString() + " Click";
                if ( !string.IsNullOrEmpty(savePosAs.name) )
                    finalString += string.Format("\n<i>(SavePos As {0})</i>", savePosAs);
                if ( !string.IsNullOrEmpty(saveGoAs.name) )
                    finalString += string.Format("\n<i>(SaveGo As {0})</i>", saveGoAs);
                return finalString;
            }
        }

        protected override bool OnCheck() {

            if ( Input.GetMouseButtonDown((int)buttonKey) ) {
                if ( Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 1 << layer) ) {
                    saveGoAs.value = hit.collider.gameObject;
                    saveDistanceAs.value = hit.distance;
                    savePosAs.value = hit.point;
                    return true;
                }
            }
            return false;
        }
    }
}