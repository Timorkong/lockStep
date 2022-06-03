/*
 * @Descripttion: 条件-目标活着
 * @Author: kevinwu
 * @Date: 2019-11-20 15:54:30
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Conditions
{
    [ActorNode]
    [Category("条件判断")]
    [Name("目标活着")]
    public class BHTPreconditionTargetAlive : ConditionTask
    {
        
        public BHTPreconditionTargetAlive()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_TARGET_ALIVE;
        }

        [HideInInspector]
        public bool isAlive = true;

        protected override string info 
        {
            get { return string.Format(isAlive ? "目标活着" : "目标没有活着"); }
        }
        protected override bool OnCheck() {
            return agent.gameObject.activeInHierarchy;
        }
    }
}
