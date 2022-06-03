using AINodeCanvas.Framework;
using AIParadoxNotion;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{

    [Category("System Events")]
    [EventReceiver("OnMouseDown", "OnMouseUp")]
    public class CheckMouseClick : ConditionTask<Collider>
    {

        public MouseClickEvent checkType = MouseClickEvent.MouseDown;

        protected override string info {
            get { return checkType.ToString(); }
        }

        protected override bool OnCheck() {
            return false;
        }


        public void OnMouseDown() {
            if ( checkType == MouseClickEvent.MouseDown )
                YieldReturn(true);
        }

        public void OnMouseUp() {
            if ( checkType == MouseClickEvent.MouseUp )
                YieldReturn(true);
        }
    }
}