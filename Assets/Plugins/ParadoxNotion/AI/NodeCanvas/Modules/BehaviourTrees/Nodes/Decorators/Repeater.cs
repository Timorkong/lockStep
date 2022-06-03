/*
 * @Descripttion: 修饰-循环执行
 * @Author: kevinwu
 * @Date: 2019-11-27 09:53:29
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.BehaviourTrees
{

    // [Name("Repeat")]
    [Category("修饰|限制节点")]
    [Name("循环执行")]
    // [Category("Decorators")]
    [Description("循环该子项x次或直到其返回指定状态为止，或者永远循环")]
    // [Description("Repeat the child either x times or until it returns the specified status, or forever")]
    // [Icon("Repeat")]
    [Color("770033")]
    [System.Serializable]
    public class Repeater : BTDecorator
    {

        public Repeater()
        {
            detailType = EnumBTNodeDecorator.AINODE_DECORATOR_REPEAT;
        }

        [Name("参数")]
        public XBHTDecoratorRepeatParam repeatConfig = new XBHTDecoratorRepeatParam();

        private int currentIteration = 1;

        protected override Status OnExecute(Component agent, IBlackboard blackboard) {

            if ( decoratedConnection == null ) {
                return Status.Optional;
            }

            // if ( decoratedConnection.status == Status.Success || decoratedConnection.status == Status.Failure ) {
            //     decoratedConnection.Reset();
            // }

            // status = decoratedConnection.Execute(agent, blackboard);

            // switch ( status ) {
            //     case Status.Resting:
            //         return Status.Running;
            //     case Status.Running:
            //         return Status.Running;
            // }

            // switch ( repeatConfig.repeaterMode ) {
            //     case RepeaterMode.循环执行特定次数:

            //         if ( currentIteration >= repeatConfig.repeatTimes.value ) {
            //             return status;
            //         }

            //         currentIteration++;
            //         break;

            //     case RepeaterMode.循环执行到成功或失败为止:

            //         if ( (int)status == (int)repeatConfig.repeatUntilStatus ) {
            //             return status;
            //         }
            //         break;
            // }

            return Status.Running;
        }

        protected override void OnReset() {
            currentIteration = 1;
        }

        public BHTDecoratorRepeatConfig ToJsonData()
        {
            BHTDecoratorRepeatConfig cfg = new BHTDecoratorRepeatConfig();
            cfg.config.repeaterMode = this.repeatConfig.repeaterMode;
            cfg.config.repeatTimes = this.repeatConfig.repeatTimes;
            cfg.config.repeatTick = Mathf.Floor(this.repeatConfig.repeatTick * BehaviourTreeDefine.TimeFrame);
            cfg.config.waitTick = Mathf.Floor(this.repeatConfig.waitTick * BehaviourTreeDefine.TimeFrame);
            cfg.config.repeatUntilStatus = this.repeatConfig.repeatUntilStatus;
            return cfg;
        }

        public Pbe.AIRepeaterConfig ToProto()
        {
            Pbe.AIRepeaterConfig cfg = new Pbe.AIRepeaterConfig()
            {
                RepeaterMode = System.Convert.ToInt32(repeatConfig.repeaterMode),
                RepeatTimes = repeatConfig.repeatTimes,
                RepeatUntilStatus = repeatConfig.repeatUntilStatus == RepeatUntilStatus.成功
            };
            return cfg;
        }

        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR

        protected override void OnNodeGUI() {

            if ( repeatConfig.repeaterMode == RepeaterMode.循环执行特定次数 ) 
            {
                GUILayout.Label(repeatConfig.repeatTimes + " 次");
                if ( Application.isPlaying )
                    GUILayout.Label("Iteration: " + currentIteration.ToString());

            } 
            else if ( repeatConfig.repeaterMode == RepeaterMode.循环执行到成功或失败为止 ) 
            {

                GUILayout.Label("直到 " + repeatConfig.repeatUntilStatus + " 为止");

            } 
            //else if ( repeatConfig.repeaterMode == RepeaterMode.循环执行固定时间 )
            //{
            //    GUILayout.Label("循环执行 " + this.repeatConfig.repeatTick + " 秒");
            //}
            //else if ( repeatConfig.repeaterMode == RepeaterMode.执行一次后等待固定时间 )
            //{
            //    GUILayout.Label("执行一次后等待 " + this.repeatConfig.waitTick + " 秒");
            //}
            else 
            {

                GUILayout.Label("(永久循环)");
            }
        }

#endif
    }

    [System.Serializable]
    public class XBHTDecoratorRepeatParam
    {
        [Name("循环模式")]
        public RepeaterMode repeaterMode = RepeaterMode.循环执行特定次数;

        [ShowIf("repeaterMode", 0)]
        [Name("循环次数")]
        public int repeatTimes = 1;

        [ShowIf("repeaterMode", 3)]
        [Name("循环时间(s)")]
        public float repeatTick = 1;

        [ShowIf("repeaterMode", 4)]
        [Name("循环一次后等待时间(s)")]
        public float waitTick = 1;

        [ShowIf("repeaterMode", 1)]
        [Name("循环中止退出")]
        public RepeatUntilStatus repeatUntilStatus = RepeatUntilStatus.成功;
    }

    [System.Serializable]
    public class BHTDecoratorRepeatConfig  : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTDecoratorRepeatParam config = new XBHTDecoratorRepeatParam();
    }


    public enum RepeaterMode
    {
        循环执行特定次数 = 0, //循环执行特定次数
        循环执行到成功或失败为止 = 1,    //循环执行到成功或失败为止
        永久循环 = 2,   //永久循环
    }


    public enum RepeatUntilStatus
    {
        失败 = 0,
        成功 = 1
    }

}