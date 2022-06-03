using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("发起攻击")]
    [Category("NotSupport")]
    public class BHTActionAttack : ActionTask<Transform>
    {
        public BHTActionAttack()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_SKILL;
        }

        [Name("参数")]
        public ActionAttackConig attackconfig = new ActionAttackConig();

        protected override string info {
            get { return string.Format("攻击类型 [" + attackconfig.attackType + "]"); }
        }

        protected override void OnExecute() {

            EndAction();
        }

        public BHTActionAttackConfig ToJsonData()
        {
            BHTActionAttackConfig cfg = new BHTActionAttackConfig();
            cfg.config = this.attackconfig;
            return cfg;
        }

        public Pbe.AIActionAttackConfig ToProto()
        {
            Pbe.AIActionAttackConfig cfg = new Pbe.AIActionAttackConfig()
            {
                AttackType = (int)this.attackconfig.attackType,
                ActionFollow = this.attackconfig.actionFollow,
                EffectFollow = this.attackconfig.effectFollow
            };
            return cfg;
        }

    }

    [System.Serializable]
    public class ActionAttackConig
    {
        [Name("选择攻击类型")]
        public EnumSkillActionType attackType = EnumSkillActionType.SKILL;

        [Name("技能动作是否跟随")]
        public bool actionFollow = false;

        [Name("技能特效是否跟随")]
        public bool effectFollow = false;

    }

    [System.Serializable]
    public class BHTActionAttackConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
       public ActionAttackConig config;
    }

}