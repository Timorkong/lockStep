using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine.UI;
using UnityEngine;

namespace AINodeCanvas.Tasks.Conditions
{

    [Category("UGUI")]
    public class ButtonClicked : ConditionTask
    {

        [RequiredField]
        public BBParameter<UnityEngine.UI.Button> button;

        protected override string info {
            get { return string.Format("Button {0} Clicked", button.ToString()); }
        }

        protected override string OnInit() {
            button.value.onClick.AddListener(OnClick);
            return null;
        }

        protected override bool OnCheck() { return false; }
        void OnClick() { YieldReturn(true); }
    }
}