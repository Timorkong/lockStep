using System.Collections.Generic;
using AINodeCanvas.Framework;
using AIParadoxNotion;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.BehaviourTrees
{
    [Category("组合节点")]
    [Name("平行节点", EnumCompositesPriority.Parallel)]
    [Description("同时执行所有子节点，并根据所选的并行策略，返回成功或失败。\n如果设置为重复，则子节点将重复执行，直到满足策略集或所有子节点都有机会执行至少一次")]    
    [Icon("Parallel")]
    [Color("ff64cb")]
    public class Parallel : BTComposite
    {

        public enum ParallelPolicy
        {
            FirstFailure,
            FirstSuccess,
            FirstSuccessOrFailure
        }

        public ParallelPolicy policy = ParallelPolicy.FirstFailure;
        [Name("Repeat")]
        public bool dynamic;

        private readonly List<Connection> finishedConnections = new List<Connection>();

        public Parallel()
        {
            _NodeType = EnumBTNodeType.AINODE_TYPE_PARALLEL;
        }

        protected override Status OnExecute(Component agent, IBlackboard blackboard) {

            var defferedStatus = Status.Resting;
            for ( var i = 0; i < outConnections.Count; i++ ) {

                if ( !dynamic && finishedConnections.Contains(outConnections[i]) ) {
                    continue;
                }

                if ( outConnections[i].status != Status.Running && finishedConnections.Contains(outConnections[i]) ) {
                    outConnections[i].Reset();
                }

                status = outConnections[i].Execute(agent, blackboard);

                if ( defferedStatus == Status.Resting ) {
                    if ( status == Status.Failure && ( policy == ParallelPolicy.FirstFailure || policy == ParallelPolicy.FirstSuccessOrFailure ) ) {
                        defferedStatus = Status.Failure;
                    }

                    if ( status == Status.Success && ( policy == ParallelPolicy.FirstSuccess || policy == ParallelPolicy.FirstSuccessOrFailure ) ) {
                        defferedStatus = Status.Success;
                    }
                }

                if ( status != Status.Running && !finishedConnections.Contains(outConnections[i]) ) {
                    finishedConnections.Add(outConnections[i]);
                }
            }

            if ( defferedStatus != Status.Resting ) {
                ResetRunning();
                return defferedStatus;
            }

            if ( finishedConnections.Count == outConnections.Count ) {
                ResetRunning();
                switch ( policy ) {
                    case ParallelPolicy.FirstFailure:
                        return Status.Success;
                    case ParallelPolicy.FirstSuccess:
                        return Status.Failure;
                }
            }

            return Status.Running;
        }

        protected override void OnReset() {
            finishedConnections.Clear();
        }

        void ResetRunning() {
            for ( var i = 0; i < outConnections.Count; i++ ) {
                if ( outConnections[i].status == Status.Running ) {
                    outConnections[i].Reset();
                }
            }
        }

        public Pbe.AIParallelConfig ToProto()
        {
            var cfg = new Pbe.AIParallelConfig()
            {
                Policy = System.Convert.ToInt32(policy),
                Repeated = dynamic
            };
            return cfg;
        }

        ////////////////////////////////////////
        ///////////GUI AND EDITOR STUFF/////////
        ////////////////////////////////////////
#if UNITY_EDITOR

        protected override void OnNodeGUI() {
            GUILayout.Label(( dynamic ? "<b>REPEAT</b>\n" : "" ) + policy.ToString().SplitCamelCase());
        }

#endif
    }
}