/*
 * @Descripttion: 条件-倒地起身
 * @Author: kevinwu
 * @Date: 2019-12-06 16:49:07
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [Category("NotSupport")]
    [Name("倒地起身")]
    public class BHTPreconditionIsGetUp : ConditionTask<Transform>
    {
        public BHTPreconditionIsGetUp()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_FALL_AND_GET_UP;
        }

        protected override string info 
        {
            get { return string.Format("倒地起身"); }
        }
    }
}
