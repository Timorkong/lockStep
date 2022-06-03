/*
 * @Descripttion: 条件-和目标距离
 * @Author: kevinwu
 * @Date: 2019-11-20 16:34:13
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [Category("NotSupport")]
    [Name("距离目标")]
    [System.Serializable]
    public class BHTPreconditionDistance : ConditionTask<Transform>
    {
        public BHTPreconditionDistance()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_DISTANCE;
        }

        [Name("参数")]
        public XBHTConditionDistanceConig distanceConfig = new XBHTConditionDistanceConig();

        protected override string info 
        {
            get 
            {
                if (distanceConfig.distanceType == EnumXAIDistanceType.距离固定值)
                {
                    return string.Format("距离目标" + distanceConfig.compareType.ToString() + distanceConfig.distance + ""); 
                }
                else
                {
                    return string.Format("在自己攻击范围"); 
                }
            }
        }

        public BHTPreconditionDistanceConig ToJsonData()
        {
            BHTPreconditionDistanceConig cfg = new BHTPreconditionDistanceConig();
            cfg.config.distanceType = distanceConfig.distanceType;
            cfg.config.compareType = distanceConfig.compareType;
            cfg.config.distance = distanceConfig.distance.FloatToPBInt();
            return cfg;
        }

        public Pbe.AIConditionDistanceTarget ToProto()
        {
            Pbe.AIConditionDistanceTarget cfg = new Pbe.AIConditionDistanceTarget();
            cfg.DistanceType = System.Convert.ToInt32(distanceConfig.distanceType);
            cfg.CompareType = System.Convert.ToInt32(distanceConfig.compareType);
            cfg.Distance = distanceConfig.distance.FloatToPBInt();
            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTConditionDistanceConig
    {
        [Name("判断类型")]
        public EnumXAIDistanceType distanceType = EnumXAIDistanceType.距离固定值;

        [ShowIf("distanceType", 2)]
        [Name("比较级")]
        public EnumBTNodeCompareSign compareType = EnumBTNodeCompareSign.等于;

        [ShowIf("distanceType", 2)]
        [Name("距离值")]
        public float distance = 0;
    }

    [System.Serializable]
    public class BHTPreconditionDistanceConig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
       public XBHTConditionDistanceConig config = new XBHTConditionDistanceConig();
    }

}
