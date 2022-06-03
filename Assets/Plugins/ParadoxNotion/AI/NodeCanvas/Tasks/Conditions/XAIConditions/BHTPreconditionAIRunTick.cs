/*
 * @Descripttion: 条件-AI已经运行时间
 * @Author: kevinwu
 * @Date: 2019-12-02 14:42:47
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [Category("NotSupport")]
    [Name("AI已运行时间")]
    [System.Serializable]
    public class BHTPreconditionAIRunTick : ConditionTask<Transform>
    {
        public BHTPreconditionAIRunTick()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_RUNNING_TIME;
        }

        [Name("参数")]
        public XBHTConditionAIRunTickConig aiRunTickConfig = new XBHTConditionAIRunTickConig();

        protected override string info 
        {
            get 
            {
                return string.Format("AI已运行时间" + aiRunTickConfig.compareType.ToString() + aiRunTickConfig.runTick + ""); 
            }
        }

        public BHTPreconditionAIRunTickConig ToJsonData()
        {
            BHTPreconditionAIRunTickConig cfg = new BHTPreconditionAIRunTickConig();
            cfg.config.compareType = this.aiRunTickConfig.compareType;
            cfg.config.runTick = Mathf.Floor(this.aiRunTickConfig.runTick * BehaviourTreeDefine.TimeFrame);
            return cfg;
        }

    }

    [System.Serializable]
    public class XBHTConditionAIRunTickConig
    {
        [Name("比较级")]
        public EnumBTNodeCompareSign compareType = EnumBTNodeCompareSign.等于;

        [Name("时间(秒)")]
        public float runTick = 0;
    }

    [System.Serializable]
    public class BHTPreconditionAIRunTickConig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
       public XBHTConditionAIRunTickConig config = new XBHTConditionAIRunTickConig();
    }

}
