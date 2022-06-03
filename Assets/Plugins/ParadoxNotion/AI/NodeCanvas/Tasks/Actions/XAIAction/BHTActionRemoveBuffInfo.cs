using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode, SceneNode]
    [Name("删除 BuffInfo")]
    [Description("删除 BuffInfoId 对应的 Buff")]
    [Category("指令")]
    public class BHTActionRemoveBuffInfo : ActionTask
    {
        public BHTActionRemoveBuffInfo()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_REMOVE_BUFF_INFO;
        }

        [Name("目标类型")]
        public EnumBTNodeTarget targetType = EnumBTNodeTarget.AINODE_TARGET_SELF;

        [Name("目标黑板ID")]
        [ShowIf("targetType", 1)]
        public BBParameter<int> targetID = new BBParameter<int>(0);

        [Name("BuffInfo Id")]
        public int buffInfoID = 0;

        protected override string info
        {
            get
            {
                if (targetType == EnumBTNodeTarget.AINODE_TARGET_SELF)
                    return string.Format("删除 BuffInfo:[ID(" + buffInfoID + ")");
                else
                    return string.Format("删除 BuffInfo:[ID(" + buffInfoID + ")目标" + targetID.name);
            }
        }

        public Pbe.AIActionRemoveBuffInfoConfig ToProto()
        {
            var cfg = new Pbe.AIActionRemoveBuffInfoConfig()
            {
                TargetType = System.Convert.ToInt32(targetType),
                BuffInfoID = buffInfoID,
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
