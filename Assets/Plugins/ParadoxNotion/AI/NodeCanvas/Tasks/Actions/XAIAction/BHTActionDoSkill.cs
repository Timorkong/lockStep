using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("释放技能")]
    [Description("根据技能ID释放对应技能")]
    [Category("指令")]
    [System.Serializable]
    public class BHTActionDoSkill : ActionTask
    {
        public BHTActionDoSkill()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_DO_SKILL;
        }

        [Name("参数")]
        public XBHTDoSkillConfig doSkillConfig = new XBHTDoSkillConfig();

        protected override string info
        {
            get
            {
                return $"释放技能: {doSkillConfig.skillId} 直接释放:{doSkillConfig.bDoSkillDirectly}";
            }
        }

        virtual public Pbe.AIActionDoSkillConfig ToProto()
        {
            Pbe.AIActionDoSkillConfig cfg = new Pbe.AIActionDoSkillConfig()
            {
                SkillId = doSkillConfig.skillId
            };
            return cfg;
        }
    }

    [SceneNode]
    [Name("释放技能")]
    [Description("根据技能ID释放对应技能")]
    [Category("指令")]
    [System.Serializable]
    public class BHTActionDoSkillSN : BHTActionDoSkill
    {
        [BlackboardOnly]
        public BBParameter<int> unitID;

        protected override string info
        {
            get
            {
                return $"{unitID.name} 释放技能: {doSkillConfig.skillId} 直接释放:{doSkillConfig.bDoSkillDirectly}";
            }
        }

        public override Pbe.AIActionDoSkillConfig ToProto()
        {
            Pbe.AIActionDoSkillConfig cfg = new Pbe.AIActionDoSkillConfig()
            {
                SkillId = doSkillConfig.skillId,
                UID = new Pbe.BBParam()
                {
                    Name = unitID.name,
                    Type = XAIConfigTool.Type2TypeId(unitID.varType)
                },
                NeedDoSkillDirectly = doSkillConfig.bDoSkillDirectly,
            };
            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTDoSkillConfig
    {
        [Name("技能 ID")]
        [SerializeField]
        public int skillId = 0;

        [Name("直接释放技能")]
        [SerializeField]
        public bool bDoSkillDirectly = false;
    }
}
