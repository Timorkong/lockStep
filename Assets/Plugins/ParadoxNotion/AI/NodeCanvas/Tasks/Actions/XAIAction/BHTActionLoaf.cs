using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("游荡")]
    [Category("指令")]
    [Description("剧情中的自身游荡节点")]
    public class BHTActionLoaf : ActionTask
    {
        public BHTActionLoaf()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_LOAF;
        }

        [Name("持续时间(ms)")]
        public int milliseconds = 0;

        protected override string info
        {
            get
            {
                return string.Format("游荡: 持续" + milliseconds + "毫秒");
            }
        }

        public Pbe.AIActionLoafConfig ToProto()
        {
            var cfg = new Pbe.AIActionLoafConfig()
            {
                Milliseconds = milliseconds
            };
            return cfg;
        }
    }
}
