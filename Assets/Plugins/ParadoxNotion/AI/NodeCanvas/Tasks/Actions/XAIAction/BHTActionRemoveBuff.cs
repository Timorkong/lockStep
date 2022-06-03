using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode, SceneNode]
    [Name("删除 Buff")]
    [Description("删除 BuffId 对应的 Buff")]
    [Category("指令")]
    public class BHTActionRemoveBuff : ActionTask
    {
        public BHTActionRemoveBuff()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_REMOVE_BUFF;
        }

        [Name("目标类型")]
        public EnumBTNodeTarget targetType = EnumBTNodeTarget.AINODE_TARGET_SELF;

        [Name("目标黑板ID")]
        [ShowIf("targetType", 1)]
        public BBParameter<int> targetID = new BBParameter<int>(0);

        [Name("Buff Id")]
        public int buffID = 0;

        protected override string info
        {
            get
            {
                if (targetType == EnumBTNodeTarget.AINODE_TARGET_SELF)
                    return string.Format("删除 Buff:[ID(" + buffID + ")");
                else
                    return string.Format("删除 Buff:[ID(" + buffID + ")目标" + targetID.name);
            }
        }

        public Pbe.AIActionRemoveBuffConfig ToProto()
        {
            var cfg = new Pbe.AIActionRemoveBuffConfig()
            {
                TargetType = System.Convert.ToInt32(targetType),
                BuffID = buffID,
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
