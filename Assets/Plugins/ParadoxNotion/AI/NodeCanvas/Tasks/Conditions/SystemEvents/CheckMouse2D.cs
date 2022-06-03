using AINodeCanvas.Framework;
using AIParadoxNotion;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{

    [Category("System Events")]
    [Name("Check Mouse 2D")]
    [EventReceiver("OnMouseEnter", "OnMouseExit", "OnMouseOver")]
    public class CheckMouse2D : ConditionTask<Collider2D>
    {

        public MouseInteractionTypes checkType = MouseInteractionTypes.MouseEnter;

        protected override string info {
            get { return checkType.ToString(); }
        }

        protected override bool OnCheck() {
            return false;
        }

        public void OnMouseEnter() {
            if ( checkType == MouseInteractionTypes.MouseEnter )
                YieldReturn(true);
        }

        public void OnMouseExit() {
            if ( checkType == MouseInteractionTypes.MouseExit )
                YieldReturn(true);
        }

        public void OnMouseOver() {
            if ( checkType == MouseInteractionTypes.MouseOver )
                YieldReturn(true);
        }
    }
}