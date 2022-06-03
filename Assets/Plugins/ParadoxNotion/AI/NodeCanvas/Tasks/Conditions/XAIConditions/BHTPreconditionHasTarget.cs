/*
 * @Descripttion: 条件-有目标
 * @Author: kevinwu
 * @Date: 2019-11-20 17:16:16
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [Category("NotSupport")]
    [Name("有目标")]
    public class BHTPreconditionHasTarget : ConditionTask<Transform>
    {
        public BHTPreconditionHasTarget()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_HAVE_TARGET;
        }

        protected override string info 
        {
            get { return string.Format("有目标"); }
        }
    }
}
