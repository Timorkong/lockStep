using System.Collections.Generic;
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.BehaviourTrees
{

    [Category("组合节点")]
    [Name("选择节点", EnumCompositesPriority.Selector)]
    [Description("选择节点会在任何一个子节点返回成功时就返回成功并且不再继续运行后续的子节点。相应的，当所有子节点都失败时，选择节点才会返回失败")]
    // [Name("Selector", 9)]
    // [Category("Composites")]
    // [Description("Execute the child nodes in order or randonly until the first that returns Success and return Success as well. If none returns Success, then returns Failure.\nIf is Dynamic, then higher priority children Status are revaluated and if one returns Success the Selector will select that one and bail out immediately in Success too")]
    // [Icon("Selector")]
    [Color("a373ff")]
    public class Selector : BTComposite
    {
        [HideInInspector]
        public bool dynamic;
        [HideInInspector]
        public bool random;

        private int lastRunningNodeIndex;

        public Selector()
        {
            _NodeType = EnumBTNodeType.AINODE_TYPE_SELECT_CONTROL;
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

                    case Status.Success:

                        if ( dynamic && i < lastRunningNodeIndex ) {
                            for ( var j = i + 1; j <= lastRunningNodeIndex; j++ ) {
                                outConnections[j].Reset();
                            }
                        }

                        return Status.Success;
                }
            }

            return Status.Failure;
        }

        protected override void OnReset() {
            lastRunningNodeIndex = 0;
            if ( random ) {
                outConnections = Shuffle(outConnections);
            }
        }

        public override void OnChildDisconnected(int index) {
            if ( index != 0 && index == lastRunningNodeIndex )
                lastRunningNodeIndex--;
        }

        public override void OnGraphStarted() {
            OnReset();
        }

        //Fisher-Yates shuffle algorithm
        private List<Connection> Shuffle(List<Connection> list) {
            for ( var i = list.Count - 1; i > 0; i-- ) {
                var j = (int)Mathf.Floor(Random.value * ( i + 1 ));
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