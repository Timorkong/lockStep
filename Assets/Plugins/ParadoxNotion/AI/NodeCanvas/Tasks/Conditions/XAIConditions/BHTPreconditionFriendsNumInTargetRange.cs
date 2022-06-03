/*
 * @Descripttion: 条件 -- 判断敌人周边的队友
 * @Author: kevinwu
 * @Date: 2019-12-26 17:00:50
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [Category("NotSupport")]
    [Description("判断目标前/目标后都有超过多少个队友在攻击/不攻击目标")]
    [Name("敌人周边队友情况")]
    [System.Serializable]
    public class BHTPreconditionFriendsNumInTargetRange : ConditionTask<Transform>
    {
        public BHTPreconditionFriendsNumInTargetRange()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_FRIND_INTARGET_RANGE;
        }

        [Name("参数")]
        public XBHTConditionFriendsInTargetRange friendsInTargetRangeConfig = new XBHTConditionFriendsInTargetRange();

        protected override string info 
        {
            get 
            {
                return string.Format("" + friendsInTargetRangeConfig.compareType.ToString() + "有超过\n" + friendsInTargetRangeConfig.frindsNum +
                "个队友" + friendsInTargetRangeConfig.attackState ); 
            }
        }

        public XBHTConditionFriendsInTargetRangeConfig ToJsonData()
        {
            XBHTConditionFriendsInTargetRangeConfig cfg = new XBHTConditionFriendsInTargetRangeConfig();
            cfg.config = this.friendsInTargetRangeConfig;
            return cfg;
        }

    }

    [System.Serializable]
    public class XBHTConditionFriendsInTargetRange
    {
        [Name("与目标进行对比")]
        public EnumInTargetRange compareType = EnumInTargetRange.在目标身前;

        [Name("队友数量")]
        public int frindsNum = 0;

        [Name("攻击状态")]
        public EnumAttackingState attackState = EnumAttackingState.在攻击;
    }

    [System.Serializable]
    public class XBHTConditionFriendsInTargetRangeConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
       public XBHTConditionFriendsInTargetRange config;
    }

}
