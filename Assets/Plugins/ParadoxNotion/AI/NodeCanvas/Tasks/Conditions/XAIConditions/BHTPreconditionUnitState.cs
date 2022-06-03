using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [ActorNode, SceneNode]
    [Category("条件判断")]
    [Name("检测单位状态")]
    public class BHTPreconditionUnitState : ConditionTask
    {
        public BHTPreconditionUnitState()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_UNIT_STATE;
        }

        [Name("目标类型")]
        public EnumBTNodeTarget targetType = EnumBTNodeTarget.AINODE_TARGET_SELF;

        [Name("目标黑板ID")]
        [ShowIf("targetType", 1)]
        public BBParameter<int> targetID = new BBParameter<int>(0);

        [Name("状态")]
        public EnumUnitState state = EnumUnitState.死亡;

        protected override string info
        {
            get
            {
                if (targetType == EnumBTNodeTarget.AINODE_TARGET_SELF)
                    return $"检测单位是否 {state}";
                else
                    return $"检测单位 {targetID.name} 是否 {state}";
            }
        }

        public Pbe.AIConditionCheckUnitStateConfig ToProto()
        {
            var cfg = new Pbe.AIConditionCheckUnitStateConfig()
            {
                TargetType = System.Convert.ToInt32(targetType),
                State = System.Convert.ToInt32(state)
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

}
