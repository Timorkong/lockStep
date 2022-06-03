/*
 * @Descripttion: 修饰-时间间隔
 * @Author: kevinwu
 * @Date: 2019-12-02 19:49:36
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.BehaviourTrees
{

    [Name("时间间隔")]
    [Category("修饰|限制节点")]
    // [Category("Decorators")]
    [Description("每隔多少秒执行一次当前节点")]
    // [Icon("Remap")]
    [Color("770033")]
    [System.Serializable]
    public class TimeInterval : BTDecorator
    {
        [Name("参数")]
        public XBHTDecoratorTimeIntervalParam timeInterval = new XBHTDecoratorTimeIntervalParam();

        public TimeInterval()
        {
            detailType = EnumBTNodeDecorator.AINODE_DECORATOR_TIME_INTERVAL;
        }

        protected override Status OnExecute(Component agent, IBlackboard blackboard) {
            return status;
        }

        public BHTDecoratorTimeIntervalConfig ToJsonData()
        {
            BHTDecoratorTimeIntervalConfig cfg = new BHTDecoratorTimeIntervalConfig();
            cfg.config.tickNum = Mathf.Floor(this.timeInterval.tickNum * BehaviourTreeDefine.TimeFrame);
            return cfg;
        }

        #if UNITY_EDITOR

        protected override void OnNodeGUI() {

            GUILayout.Label("间隔" + timeInterval.tickNum + "秒执行一次");
        }

#endif
    }

    [System.Serializable]
    public class XBHTDecoratorTimeIntervalParam
    {
        [Name("间隔时间(s)")]
        public float tickNum = 0;
    }

    [System.Serializable]
    public class BHTDecoratorTimeIntervalConfig  : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTDecoratorTimeIntervalParam config = new XBHTDecoratorTimeIntervalParam();
    }
}