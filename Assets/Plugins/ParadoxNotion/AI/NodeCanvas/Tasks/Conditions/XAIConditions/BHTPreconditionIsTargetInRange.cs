/*
 * @Descripttion: 条件-目标在攻击范围
 * @Author: kevinwu
 * @Date: 2019-12-25 20:09:02
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [Category("NotSupport")]
    [Name("目标在攻击范围")]
    public class BHTPreconditionIsTargetInRange : ConditionTask<Transform>
    {
        public BHTPreconditionIsTargetInRange()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_TARGET_INRANGE;
        }

        protected override string info 
        {
            get { return string.Format("目标在攻击范围"); }
        }
    }
}
