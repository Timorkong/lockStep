using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [ActorNode, SceneNode]
    [Category("条件判断")]
    [Name("是否有指定Buff")]
    public class BHTPreconditionHasBuff : ConditionTask
    {
        public BHTPreconditionHasBuff()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_HAS_BUFF;
        }

        [Name("目标类型")]
        public EnumBTNodeTarget targetType = EnumBTNodeTarget.AINODE_TARGET_SELF;

        [Name("目标黑板ID")]
        [ShowIf("targetType", 1)]
        public BBParameter<int> targetID = new BBParameter<int>(0);

        [Name("BuffId")]
        public int buffID = 0;

        protected override string info
        {
            get
            {
                if (targetType == EnumBTNodeTarget.AINODE_TARGET_SELF)
                    return string.Format("是否有指定Buff(自身): " + buffID);
                else
                    return string.Format("是否有指定Buff(" + targetID.name + "): " + buffID);
            }
        }

        public Pbe.AIConditionHasBuffConfig ToProto()
        {
            var cfg = new Pbe.AIConditionHasBuffConfig()
            {
                TargetType = System.Convert.ToInt32(targetType),
                BuffID = buffID
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
