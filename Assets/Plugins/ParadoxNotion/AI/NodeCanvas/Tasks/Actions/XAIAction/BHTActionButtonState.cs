using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using System.Collections.Generic;

namespace AINodeCanvas.Tasks.Actions
{
    [SceneNode]
    [Name("设置按键状态")]
    [Category("指令")]
    [System.Serializable]
    public class BHTActionButtonState : ActionTask
    {
        public BHTActionButtonState()
        {
            detailType = EnumBTNodeAction.AINODE_BUTTON_STATE;
        }

        [Name("按键列表")]
        public List<EnumSkillPos> buttons = new List<EnumSkillPos>();

        [Name("状态")]
        public bool state = true;

        protected override string info
        {
            get
            {
                return $"设置按键状态 {state}";
            }
        }

        public Pbe.AIActionButtonStateConfig ToProto()
        {
            Pbe.AIActionButtonStateConfig cfg = new Pbe.AIActionButtonStateConfig()
            {
                State = state
            };
            cfg.ButtonPos.Clear();
            for (int i = 0; i < buttons.Count; i++)
            {
                cfg.ButtonPos.Add((int)buttons[i]);
            }
            return cfg;
        }
    }

}
