using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode, SceneNode]
    [Name("添加 Buff")]
    [Description("通过 BuffId 给自身或者目标添加 Buff")]
    [Category("指令")]
    public class BHTActionAddBuff : ActionTask
    {
        public BHTActionAddBuff()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_BUFF;
        }

        [Name("目标类型")]
        public EnumBTNodeTarget targetType = EnumBTNodeTarget.AINODE_TARGET_SELF;

        [Name("目标黑板ID")]
        [ShowIf("targetType", 1)]
        public BBParameter<int> targetID = new BBParameter<int>(0);

        [Name("Buff Id")]
        public int buffID = 0;

        [Name("Buff等级")]
        public int buffLevel = 0;

        [Name("持续时间(ms)")]
        public int duration = 0;

        protected override string info
        {
            get
            {
                if (targetType == EnumBTNodeTarget.AINODE_TARGET_SELF)
                    return string.Format("添加 Buff:[ID(" + buffID + ")|等级" + buffLevel + "|持续" + duration + "毫秒");
                else
                    return string.Format("添加 Buff:[ID(" + buffID +")目标" + targetID.name + "|等级" + buffLevel + "|持续" + duration + "毫秒");
            }
        }

        public Pbe.AIActionAddBuffConfig ToProto()
        {
            var cfg = new Pbe.AIActionAddBuffConfig()
            {
                TargetType = System.Convert.ToInt32(targetType),
                BuffID = buffID,
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
