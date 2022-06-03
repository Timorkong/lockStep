/*
 * @Descripttion: 条件-面向目标
 * @Author: colecai
 * @Date: 2020-11-24 17:16:16
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [Category("NotSupport")]
    [Name("AI智力判断")]
    public class BHTPreconditionIntelligence : ConditionTask<Transform>
    {
        public BHTPreconditionIntelligence()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_INTELLIGENCE;
        }

        [Name("参数")]
        public XBHTConditionIntelligenceConig intelligenceConfig = new XBHTConditionIntelligenceConig();

        protected override string info
        {
            get
            {
                return string.Format( intelligenceConfig.compareType.ToString() + intelligenceConfig.intelligence);
            }
        }

        public BHTPreconditionIntelligenceConig ToJsonData()
        {
            BHTPreconditionIntelligenceConig cfg = new BHTPreconditionIntelligenceConig();
            cfg.config.compareType = intelligenceConfig.compareType;
            cfg.config.intelligence = intelligenceConfig.intelligence;
            return cfg;
        }

    }


    [System.Serializable]
    public class XBHTConditionIntelligenceConig
    {
        [Name("比较级")]
        public EnumBTNodeCompareSign compareType = EnumBTNodeCompareSign.大于;

        [Name("智力值")]
        public int intelligence = 0;
    }

    [System.Serializable]
    public class BHTPreconditionIntelligenceConig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTConditionIntelligenceConig config = new XBHTConditionIntelligenceConig();
    }

}
