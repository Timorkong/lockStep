/*
 * @Descripttion: 条件-血量对比
 * @Author: kevinwu
 * @Date: 2019-11-20 16:08:13
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [ActorNode, SceneNode]
    [Category("条件判断")]
    [Name("判断当前血量")]
    public class BHTPreconditionHP : ConditionTask
    {
        public BHTPreconditionHP()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_HP;
        }

        [Name("目标类型")]
        public EnumBTNodeTarget targetType = EnumBTNodeTarget.AINODE_TARGET_SELF;

        [Name("目标黑板ID")]
        [ShowIf("targetType", 1)]
        public BBParameter<int> targetID = new BBParameter<int>(0);

        [Name("比较方式")]
        public EnumBTNodeCompareSign compareType = EnumBTNodeCompareSign.不等于;

        [Name("数值类型")]
        public EnumBTNodeValueRefer valueRefer = EnumBTNodeValueRefer.具体数值;

        [Name("设定值")]
        [ShowIf("valueRefer", 0)]
        public int valueHP = 0;

        [Name("设定值")]
        [ShowIf("valueRefer", 1)]
        [SliderField(0, 100)]
        public int percentHP = 0;

        protected override string info 
        {
            get 
            {
                if (valueRefer == EnumBTNodeValueRefer.具体数值)
                {
                    if(targetType == EnumBTNodeTarget.AINODE_TARGET_SELF)
                        return string.Format("判断当前血量(自身)" + compareType.ToString() + valueHP);
                    else
                        return string.Format("判断当前血量(" + targetID.name + ")" + compareType.ToString() + valueHP);
                }
                else
                {
                    if (targetType == EnumBTNodeTarget.AINODE_TARGET_SELF)
                        return string.Format("判断当前血量(自身)" + compareType.ToString() + percentHP + "%");
                    else
                        return string.Format("判断当前血量(" + targetID.name + ")" + compareType.ToString() + percentHP + "%");
                }
            }
        }

        public BHTPreconditionHPConfig ToJsonData()
        {
            BHTPreconditionHPConfig cfg = new BHTPreconditionHPConfig();
            cfg.config.compareType = compareType;
            cfg.config.hpLimit = valueHP;
            return cfg;
        }

        protected override bool OnCheck()
        {
            if (compareType == EnumBTNodeCompareSign.不等于)
                return valueHP != 0;
            return false;
        }

        public Pbe.AIConditionCheckHPConfig ToProto()
        {
            var cfg = new Pbe.AIConditionCheckHPConfig()
            {
                TargetType = System.Convert.ToInt32(targetType),
                CompareType = System.Convert.ToInt32(compareType),
                ValueRefer = System.Convert.ToInt32(valueRefer)
            };
            if(targetType == EnumBTNodeTarget.AINODE_TARGET_TARGET)
            {
                cfg.TargetID = new Pbe.BBParam()
                {
                    Name = targetID.name,
                    Type = XAIConfigTool.Type2TypeId(targetID.varType)
                };
            }
            if(valueRefer == EnumBTNodeValueRefer.具体数值)
            {
                cfg.HpValueOrPercent = valueHP;
            }
            else
            {
                cfg.HpValueOrPercent = percentHP;
            }
            return cfg;
        }

    }

    [System.Serializable]
    public class XBHTConditionHPConig
    {

        [Name("比较方式")]
        public EnumBTNodeCompareSign compareType = EnumBTNodeCompareSign.不等于;


        [Name("hp血量")]
        public float hpLimit = 0;
    }


    [System.Serializable]
    public class BHTPreconditionHPConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTConditionHPConig config = new XBHTConditionHPConig();
    }
}
