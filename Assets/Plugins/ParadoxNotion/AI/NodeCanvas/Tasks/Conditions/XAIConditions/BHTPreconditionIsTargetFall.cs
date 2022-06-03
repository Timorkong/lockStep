/*
 * @Descripttion: 条件-目标倒地 
 * @Author: kevinwu
 * @Date: 2019-12-25 20:12:42
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [Category("NotSupport")]
    [Name("目标倒地")]
    public class BHTPreconditionIsTargetFall : ConditionTask<Transform>
    {
        public BHTPreconditionIsTargetFall()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_TARGET_FALL;
        }

        protected override string info 
        {
            get { return string.Format("目标倒地"); }
        }
    }
}
