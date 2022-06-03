/*
 * @Descripttion: 条件-已经倒地
 * @Author: kevinwu
 * @Date: 2019-11-20 17:16:16
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [Category("NotSupport")]
    [Name("被打倒地")]
    public class BHTPreconditionIsFall : ConditionTask<Transform>
    {
        public BHTPreconditionIsFall()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_BEATTACK_FALL;
        }

        protected override string info 
        {
            get { return string.Format("已倒地"); }
        }
    }
}
