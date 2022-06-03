using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [ActorNode, SceneNode]
    [Category("条件判断")]
    [Name("是否在使用技能")]
    public class BHTPreconditionUsingSkill : ConditionTask
    {
        public BHTPreconditionUsingSkill()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_USING_SKILL;
        }

        [Name("目标类型")]
        public EnumBTNodeTarget targetType = EnumBTNodeTarget.AINODE_TARGET_SELF;

        [Name("目标黑板ID")]
        [ShowIf("targetType", 1)]
        public BBParameter<int> targetID = new BBParameter<int>(0);

        [Name("SkillId")]
        public int skillID = 0;

        protected override string info
        {
            get
            {
                if (targetType == EnumBTNodeTarget.AINODE_TARGET_SELF)
                    return string.Format("是否在使用技能(自身): " + skillID);
                else
                    return string.Format("是否在使用技能(" + targetID.name + "): " + skillID);
            }
        }

        public Pbe.AIConditionUsingSkillConfig ToProto()
        {
            var cfg = new Pbe.AIConditionUsingSkillConfig()
            {
                TargetType = System.Convert.ToInt32(targetType),
                SkillID = skillID
            };
            if(targetType == EnumBTNodeTarget.AINODE_TARGET_TARGET)
            {
                cfg.TargetID = new Pbe.BBParam()
                {
                    Name = targetID.name,
                    Type = XAIConfigTool.Type2TypeId(targetID.varType)
                };
            }
            return cfg;
        }
    }
}
