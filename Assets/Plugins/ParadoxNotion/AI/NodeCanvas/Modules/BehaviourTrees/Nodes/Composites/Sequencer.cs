using System.Collections.Generic;
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;
using System;

namespace AINodeCanvas.BehaviourTrees
{

    // [Category("组合节点")]
    [Name("顺序节点", EnumCompositesPriority.Sequencer)]
    [Description("顺序节点会依次访问子节点。每个子节点成功之后便轮到下一个，直到最后。如果所有子节点都成功，则向顺序节点返回成功；其间任何一个子节点返回失败，就会立即向顺序节点返回失败的结果")]
    // [Name("Sequencer", 10)]
    // [Category("Composites")]
    
    // [Description("Execute the child nodes in order or randonly and return Success if all children return Success, else return Failure\nIf is Dynamic, higher priority child status is revaluated. If a child returns Failure the Sequencer will bail out immediately in Failure too.")]
    // [Icon("Sequencer")]
    [Color("00aa33")]
    [Serializable]
    public class Sequencer : BTComposite
    {
        [HideInInspector]
        public bool dynamic;
        [HideInInspector]
        public bool random;

        private int lastRunningNodeIndex = 0;

        public Sequencer()
        {
            _NodeType = EnumBTNodeType.AINODE_TYPE_SEQUEUE_CONTROL;
        }

        protected override Status OnExecute(Component agent, IBlackboard blackboard) {

            for ( var i = dynamic ? 0 : lastRunningNodeIndex; i < outConnections.Count; i++ ) {

                // if ( i < lastRunningNodeIndex ) {
                //     outConnections[i].Reset();
                // }

                status = outConnections[i].Execute(agent, blackboard);

                switch ( status ) {
                    case Status.Running:

                        if ( dynamic && i < lastRunningNodeIndex ) {
                            outConnections[lastRunningNodeIndex].Reset();
                        }

                        lastRunningNodeIndex = i;
                        return Status.Running;

                    case Status.Failure:

                        if ( dynamic && i < lastRunningNodeIndex ) {
                            for ( var j = i + 1; j <= lastRunningNodeIndex; j++ ) {
                                outConnections[j].Reset();
                            }
                        }

                        return Status.Failure;
                }
            }

            return Status.Success;
        }

        protected override void OnReset() {
            lastRunningNodeIndex = 0;
            if ( random ) {
                outConnections = Shuffle(outConnections);
            }
        }

        public override void OnChildDisconnected(int index) {
            if ( index != 0 && index == lastRunningNodeIndex ) {
                lastRunningNodeIndex--;
            }
        }

        public override void OnGraphStarted() {
            OnReset();
        }

        //Fisher-Yates shuffle algorithm
        private List<Connection> Shuffle(List<Connection> list) {
            for ( var i = list.Count - 1; i > 0; i-- ) {
                var j = (int)Mathf.Floor(UnityEngine.Random.value * ( i + 1 ));
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }

            return list;
        }

        /////////////////////////////////////////
        /////////GUI AND EDITOR STUFF////////////
        /////////////////////////////////////////
#if UNITY_EDITOR

        protected override void OnNodeGUI() {
            if ( dynamic )
                GUILayout.Label("<b>DYNAMIC</b>");
            if ( random )
                GUILayout.Label("<b>RANDOM</b>");
        }

#endif
    }
}