using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode, SceneNode]
    [Name("添加 BuffInfo")]
    [Description("通过 BuffInfoId 给自身或者目标添加 Buff")]
    [Category("指令")]
    public class BHTActionAddBuffInfo : ActionTask
    {
        public BHTActionAddBuffInfo()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_ADD_BUFF_INFO;
        }

        [Name("目标类型")]
        public EnumBTNodeTarget targetType = EnumBTNodeTarget.AINODE_TARGET_SELF;

        [Name("目标黑板ID")]
        [ShowIf("targetType", 1)]
        public BBParameter<int> targetID = new BBParameter<int>(0);

        [Name("BuffInfo Id")]
        public int buffInfoID = 0;

        [Name("Buff 等级")]
        public int buffLevel = 0;

        [Name("持续时间(ms)")]
        public int duration = 0;

        protected override string info
        {
            get
            {
                if (targetType == EnumBTNodeTarget.AINODE_TARGET_SELF)
                    return string.Format("添加 BuffInfo:[ID(" + buffInfoID + ")|等级" + buffLevel + "|持续" + duration + "毫秒");
                else
                    return string.Format("添加 BuffInfo:[ID(" + buffInfoID + ")目标" + targetID.name + "|等级" + buffLevel + "|持续" + duration + "毫秒");
            }
        }

        public Pbe.AIActionAddBuffInfoConfig ToProto()
        {
            var cfg = new Pbe.AIActionAddBuffInfoConfig()
            {
                TargetType = System.Convert.ToInt32(targetType),
                BuffInfoID = buffInfoID,
                BuffLevel = buffLevel,
                MillisecDuration = duration
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
