using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [ActorNode]
    [Category("条件判断")]
    [Name("判断伤害元素属性")]
    public class BHTPreconditionElementDamage : ConditionTask
    {
        public BHTPreconditionElementDamage()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_ELEMENT_DAMAGE;
        }

        [Name("目标类型")]
        public EnumBTNodeTarget targetType = EnumBTNodeTarget.AINODE_TARGET_SELF;

        [Name("目标黑板ID")]
        [ShowIf("targetType", 1)]
        public BBParameter<int> targetID = new BBParameter<int>(0);

        [Name("目标类型")]
        public EnumElementType elementType = EnumElementType.风;

        protected override string info
        {
            get
            {
                return string.Format("检测收到伤害时，伤害的属性为" + elementType.ToString());
            }
        }

        public Pbe.AIConditionElementDamageConfig ToProto()
        {
            var cfg = new Pbe.AIConditionElementDamageConfig()
            {
                TargetType = System.Convert.ToInt32(targetType),
                ElementState = System.Convert.ToInt32(elementType),
            };
            if (targetType == EnumBTNodeTarget.AINODE_TARGET_TARGET)
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

    [SceneNode]
    [Category("条件判断")]
    [Name("判断伤害元素属性")]
    public class BHTPreconditionElementDamage_SN : BHTPreconditionElementDamage
    {
        public BHTPreconditionElementDamage_SN():base()
        {
            targetType = EnumBTNodeTarget.AINODE_TARGET_TARGET;
        }
    }
}
