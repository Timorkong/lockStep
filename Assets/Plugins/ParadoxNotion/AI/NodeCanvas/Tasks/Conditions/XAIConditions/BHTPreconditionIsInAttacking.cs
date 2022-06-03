/*
 * @Descripttion: 条件-自己正在攻击
 * @Author: kevinwu
 * @Date: 2019-12-25 17:39:59
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [Category("NotSupport")]
    [Name("自己正在攻击")]
    public class BHTPreconditionIsInAttacking : ConditionTask<Transform>
    {
        public BHTPreconditionIsInAttacking()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_IS_ATTACKING;
        }

        protected override string info 
        {
            get { return string.Format("自己正在攻击"); }
        }
    }
}
