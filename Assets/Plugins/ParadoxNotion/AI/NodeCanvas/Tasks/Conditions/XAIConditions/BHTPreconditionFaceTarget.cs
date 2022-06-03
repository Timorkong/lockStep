/*
 * @Descripttion: 条件-面向目标
 * @Author: colecai
 * @Date: 2020-11-24 17:16:16
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [Category("NotSupport")]
    [Name("面朝目标")]
    public class BHTPreconditionFaceTarget : ConditionTask<Transform>
    {
        public BHTPreconditionFaceTarget()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_FACE_TARGET;
        }

        protected override string info 
        {
            get { return string.Format("面向目标"); }
        }
    }
}
