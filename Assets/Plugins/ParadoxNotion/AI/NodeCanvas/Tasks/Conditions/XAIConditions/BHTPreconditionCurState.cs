/*
 * @Descripttion: 条件-当前状态区分
 * @Author: kevinwu
 * @Date: 2019-12-03 17:52:40
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [Category("NotSupport")]
    [Name("当前状态")]
    public class BHTPreconditionCurState : ConditionTask<Transform>
    {
        public BHTPreconditionCurState()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_NOW_STATE;
        }

        [Name("参数")]
        public XBHTConditionCurStateParam curStateConfig = new XBHTConditionCurStateParam();

        protected override string info 
        {
            get { return string.Format("当前状态等于[" + curStateConfig.state + "]"); }
        }

        public XBHTConditionCurStateConfig ToJsonData()
        {
            XBHTConditionCurStateConfig cfg = new XBHTConditionCurStateConfig();
            cfg.config = this.curStateConfig;
            return cfg;
        }

    }

    [System.Serializable]
    public class XBHTConditionCurStateParam
    {
        [Name("指定等于当前状态")]
        public EnumAIState state = EnumAIState.未启动;
    }


    [System.Serializable]
    public class XBHTConditionCurStateConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTConditionCurStateParam config;
    }
}
