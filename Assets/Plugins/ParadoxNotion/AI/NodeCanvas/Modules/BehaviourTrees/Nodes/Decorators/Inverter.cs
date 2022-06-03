/*
 * @Descripttion: 修饰-结果取反
 * @Author: kevinwu
 * @Date: 2019-11-27 09:53:28
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.BehaviourTrees
{

    [Name("结果取反")]
    [Category("修饰|限制节点")]
    // [Category("Decorators")]
    [Description("成功返回失败，失败返回成功")]
    // [Icon("Remap")]
    [Color("770033")]
    [System.Serializable]
    public class Inverter : BTDecorator
    {

        public Inverter()
        {
            detailType = EnumBTNodeDecorator.AINODE_DECORATOR_REVERSAL;
        }

        protected override Status OnExecute(Component agent, IBlackboard blackboard) {

            if ( decoratedConnection == null )
                return Status.Optional;

            status = decoratedConnection.Execute(agent, blackboard);

            switch ( status ) {
                case Status.Success:
                    return Status.Failure;
                case Status.Failure:
                    return Status.Success;
            }

            return status;
        }
    }
}