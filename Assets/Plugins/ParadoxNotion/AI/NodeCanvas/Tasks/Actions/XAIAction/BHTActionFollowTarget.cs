/*
 * @Descripttion: 行为-跟随目标保持一定的距离
 * @Author: kevinwu
 * @Date: 2019-12-26 19:10:19
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("保持距离跟随目标")]
    [Description("始终执行后退和目标保持恒定的距离不变，当目标靠近我则后退，目标后退我则前进")]
    [Category("AI行为")]
    [System.Serializable]
    public class BHTActionFollowTarget : ActionTask
    {
        public BHTActionFollowTarget()
        {
            detailType = EnumBTNodeAction.AINODE_FOLLOW_TARGET;
        }
        //[Name("参数")]
        //public XBHTFollowTargetConfig followTargetConfig = new XBHTFollowTargetConfig();
        // public EnumXAIMoveType moveType = EnumXAIMoveType.WALK;

        protected override string info {
            get { return string.Format("保持距离跟随目标"); }
        }

        // protected override void OnExecute() {

        //     EndAction();
        // }

        public BHTActionFollowTargetConfig ToJsonData()
        {
            BHTActionFollowTargetConfig cfg = new BHTActionFollowTargetConfig();
            //cfg.config.distance = this.followTargetConfig.distance.FloatToPBLong();
            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTFollowTargetConfig
    {
        [Name("保持距离(X轴)")]
        [SerializeField]
        public float distance = 0;
    }

    [System.Serializable]
    public class BHTActionFollowTargetConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTFollowTargetConfig config = new XBHTFollowTargetConfig();
    }
}