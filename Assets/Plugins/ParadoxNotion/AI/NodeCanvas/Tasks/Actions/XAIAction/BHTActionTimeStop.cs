/*
 * @Descripttion: AI行为-巡逻
 * @Author: kevinwu
 * @Date: 2019-11-20 10:58:21
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("停止(已启用)")]
    [Description("AI停止一定时间后继续执行下一个节点")]
    [Category("NotSupport")]
    [System.Serializable]
    public class BHTActionTimeStop : ActionTask<Transform>
    {
        public BHTActionTimeStop()
        {
            detailType = EnumBTNodeAction.AINODE_TIME_STOP;
        }
        [Name("参数")]
        public XBHTTimeStopConfig timeStopConfig = new XBHTTimeStopConfig();
        // [Name("选择巡逻动作")]
        // [SerializeField]
        // public EnumXAIMoveType moveType = EnumXAIMoveType.WALK;

        protected override string info {
            get { return string.Format("停止" + this.timeStopConfig.tickNum + "秒"); }
        }

        // protected override void OnExecute() {

        //     EndAction();
        // }

        public BHTActionTimeStopConfig ToJsonData()
        {
            BHTActionTimeStopConfig cfg = new BHTActionTimeStopConfig();
            cfg.config.tickNum = Mathf.Floor(this.timeStopConfig.tickNum * BehaviourTreeDefine.TimeFrame);
            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTTimeStopConfig
    {
        [Name("停止时间(s)")]
        [SerializeField]
        public float tickNum = 0;
    }

    [System.Serializable]
    public class BHTActionTimeStopConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTTimeStopConfig config = new XBHTTimeStopConfig();
    }
}