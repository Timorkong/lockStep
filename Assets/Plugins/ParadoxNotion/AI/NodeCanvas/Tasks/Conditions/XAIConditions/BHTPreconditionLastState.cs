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
    [Name("上一次状态")]
    public class BHTPreconditionLastState : ConditionTask<Transform>
    {
        public BHTPreconditionLastState()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_LAST_STATE;
        }

        [Name("参数")]
        public XBHTConditionLastStateParam curStateConfig = new XBHTConditionLastStateParam();

        protected override string info 
        {
            get { return string.Format("上一次状态等于[" + curStateConfig.state + "]"); }
        }

        public XBHTConditionLastStateConfig ToJsonData()
        {
            XBHTConditionLastStateConfig cfg = new XBHTConditionLastStateConfig();
            cfg.config = this.curStateConfig;
            return cfg;
        }

    }

    [System.Serializable]
    public class XBHTConditionLastStateParam
    {
        [Name("指定等于上一次状态")]
        public EnumAIState state = EnumAIState.未启动;
    }


    [System.Serializable]
    public class XBHTConditionLastStateConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTConditionLastStateParam config;
    }
}
