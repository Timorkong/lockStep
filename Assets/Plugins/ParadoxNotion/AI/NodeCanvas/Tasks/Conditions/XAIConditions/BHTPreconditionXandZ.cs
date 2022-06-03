/*
 * @Descripttion: 条件-有目标
 * @Author: coleCai
 * @Date: 2020-09-18 10:16:16
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [ActorNode]
    [Category("条件判断")]
    [Name("判断自身和目标X和Z轴距离")]
    public class BHTPreconditionXandZ : ConditionTask
    {
        static string[] op = new string[] { "＝", "≠", "＞", "＜", "≥", "≤" };

        public BHTPreconditionXandZ()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_DISTANCE_XZ;
        }
        [Name("参数")]
        public XBHTConditionXandYConig distanceConfig = new XBHTConditionXandYConig();

        protected override string info
        {
            get
            {
                return $"X轴距离 {op[(int)distanceConfig.xDistanceType]} {distanceConfig.xDistance / 10000.0f}" +
                  $" {distanceConfig.calType} " +
                  $"Z轴距离 {op[(int)distanceConfig.zDistanceType]} {distanceConfig.zDistance / 10000.0f}";
            }
        }

        public BHTPreconditionXandZConig ToJsonData()
        {
            BHTPreconditionXandZConig cfg = new BHTPreconditionXandZConig();
            cfg.config.xDistanceType = distanceConfig.xDistanceType;
            cfg.config.xDistance = distanceConfig.xDistance.FloatToPBInt();

            cfg.config.zDistanceType = distanceConfig.zDistanceType;
            cfg.config.zDistance = distanceConfig.zDistance.FloatToPBInt();
            cfg.config.calType = distanceConfig.calType;
            return cfg;
        }

        public Pbe.AIConditionDistanceXYConfig ToProto()
        {
            Pbe.AIConditionDistanceXYConfig cfg = new Pbe.AIConditionDistanceXYConfig();
            cfg.XDistanceType = (int)distanceConfig.xDistanceType;
            cfg.XDistance = distanceConfig.xDistance;
            cfg.ZDistanceType = (int)distanceConfig.zDistanceType;
            cfg.ZDistance = distanceConfig.zDistance;
            cfg.CalType = (int)distanceConfig.calType;
            cfg.FaceOnly = distanceConfig.faceOnly;
            return cfg;
        }
    }


    [System.Serializable]
    public class XBHTConditionXandYConig
    {

        [Name("X轴条件")]
        public EnumBTNodeCompareSign xDistanceType = EnumBTNodeCompareSign.等于;

        [Name("X轴距离")]
        public float xDistance = 0;

        [Name("Z轴条件")]
        public EnumBTNodeCompareSign zDistanceType = EnumBTNodeCompareSign.等于;

        [Name("Z轴距离")]
        public float zDistance = 0;

        [Name("两者条件关系")]
        public EnumBTNodeCoditionCal calType = EnumBTNodeCoditionCal.且;

        [Name("只判断面朝方向")]
        public bool faceOnly = false;
    }


    [System.Serializable]
    public class BHTPreconditionXandZConig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTConditionXandYConig config = new XBHTConditionXandYConig();
    }

}
