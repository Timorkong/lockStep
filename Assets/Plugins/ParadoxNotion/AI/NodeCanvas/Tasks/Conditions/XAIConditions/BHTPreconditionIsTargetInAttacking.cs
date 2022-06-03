/*
 * @Descripttion: 条件-目标正在攻击
 * @Author: kevinwu
 * @Date: 2019-12-25 17:43:37
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [Category("NotSupport")]
    [Name("目标正在攻击")]
    public class BHTPreconditionIsTargetInAttacking : ConditionTask<Transform>
    {
        public BHTPreconditionIsTargetInAttacking()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_TARGET_ATTACKING;
        }

        protected override string info 
        {
            get { return string.Format("目标正在攻击"); }
        }
    }
}
