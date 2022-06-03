using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("自杀")]
    [Category("指令")]
    [Description("剧情中的自杀")]
    public class BHTActionSuicide : ActionTask
    {
        public BHTActionSuicide()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_SUICIDE;
        }

        [Name("是否播放死亡动画")]
        public bool playAnimation = true;

        [ShowIf("playAnimation", 1)]
        [Name("死亡动作ID(技能ID)")]
        public int skillID = 0;

        [Name("是否隐藏掉落表现")]
        public bool hideDropItem = false;

        protected override string info
        {
            get
            {
                if (playAnimation)
                    if(skillID == 0)
                        return string.Format("自杀: 播放默认死亡动画");
                    else
                        return string.Format("自杀: 播放动画" + skillID);
                else
                    return string.Format("自杀: 不播放动画");
            }
        }

        virtual public Pbe.AIActionSuicideConfig ToProto()
        {
            var cfg = new Pbe.AIActionSuicideConfig()
            {
                PlayAnimation = playAnimation,
                SkillID = skillID,
                HideDropItem = hideDropItem,
            };
            if (!cfg.PlayAnimation) {
                cfg.SkillID = 0;
            }
            return cfg;
        }
    }

    [SceneNode]
    [Name("自杀")]
    [Category("指令")]
    [Description("剧情中的自杀")]
    public class BHTActionSuicideSN : BHTActionSuicide
    {
        [BlackboardOnly]
        public BBParameter<int> unitID;

        override public Pbe.AIActionSuicideConfig ToProto()
        {
            var cfg = new Pbe.AIActionSuicideConfig()
            {
                PlayAnimation = playAnimation,
                SkillID = skillID,
                UID = new Pbe.BBParam()
                {
                    Name = unitID.name,
                    Type = XAIConfigTool.Type2TypeId(unitID.varType)
                },
                HideDropItem = hideDropItem,
            };
            return cfg;
        }
    }
}
