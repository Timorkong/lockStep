using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("随机技能(可配置)")]
    [Category("NotSupport")]
    public class BHTActionRandomSkillSet : ActionTask<Transform>
    {
        public BHTActionRandomSkillSet()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_RANDOM_SKILL_SET;
        }

        [Name("参数")]
        public XBHTActionRandomSkillSetConfig attackconfig = new XBHTActionRandomSkillSetConfig();

        protected override string info {
            get { return string.Format("概率 " + attackconfig.SkillPro1 + "攻击类型 [" + attackconfig.attackType1 + "] \n" +
                                       "概率 " + attackconfig.SkillPro2 + "攻击类型 [" + attackconfig.attackType2 + "] \n" +
                                       "概率 " + attackconfig.SkillPro3 + "攻击类型 [" + attackconfig.attackType3 + "] \n"); }
        }


        public BHTActionRandomSkillSetConfig ToJsonData()
        {
            BHTActionRandomSkillSetConfig cfg = new BHTActionRandomSkillSetConfig();
            cfg.config = this.attackconfig;
            return cfg;
        }
    }


    [System.Serializable]
    public class XBHTActionRandomSkillSetConfig
    {
        [Name("选择攻击类型一")]
        public EnumSkillActionType attackType1 = EnumSkillActionType.SKILL;

        [Name("技能一概率")]
        public int SkillPro1 = 0;

        [Name("技能一是否动作跟随")]
        public bool actionFollow_1 = false;

        [Name("技能一是否特效跟随")]
        public bool effectFollow_1 = false;

        [Name("选择攻击类型二")]
        public EnumSkillActionType attackType2 = EnumSkillActionType.SKILL;

        [Name("技能二概率")]
        public int SkillPro2 = 0;

        [Name("技能二是否动作跟随")]
        public bool actionFollow_2 = false;

        [Name("技能二是否特效跟随")]
        public bool effectFollow_2 = false;

        [Name("选择攻击类型三")]
        public EnumSkillActionType attackType3 = EnumSkillActionType.SKILL;

        [Name("技能三概率")]
        public int SkillPro3 = 0;

        [Name("技能三是否动作跟随")]
        public bool actionFollow_3 = false;

        [Name("技能三是否特效跟随")]
        public bool effectFollow_3 = false;

    }

    [System.Serializable]
    public class BHTActionRandomSkillSetConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTActionRandomSkillSetConfig config = new XBHTActionRandomSkillSetConfig();
    }
}