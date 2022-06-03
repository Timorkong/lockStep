using AINodeCanvas.Framework;
using AINodeCanvas.Framework.Internal;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.BehaviourTrees
{
    [Category("暂不开放")]
    // [Category("Composites")]
    // [Category("组合节点")]
    [Name("开关节点", EnumCompositesPriority.Switch)]
    [Description("根据提供的值或枚举执行一个子节点，并返回状态。如果设置为动态获取，当在运行子节点时收到修改的枚举值，则立即中断当前子节点并且修改新节点")]
    // [Description("Executes one child based on the provided int or enum and return it's status. If set to Dynamic and 'case' change while a child is running, that child will be interrupted before the new child is executed.")]
    [Icon("IndexSwitcher")]
    [Color("b3ff7f")]
    public class Switch : BTComposite
    {

        public enum CaseSelectionMode
        {
            IndexBased = 0,
            EnumBased = 1
        }

        public enum OutOfRangeMode
        {
            ReturnFailure,
            LoopIndex
        }

        public bool dynamic;

        public CaseSelectionMode selectionMode = CaseSelectionMode.IndexBased;

        [ShowIf("selectionMode", 0)]
        public BBParameter<int> intCase;
        [ShowIf("selectionMode", 0)]
        public OutOfRangeMode outOfRangeMode = OutOfRangeMode.LoopIndex;

        [ShowIf("selectionMode", 1)]
        [BlackboardOnly]
        public BBObjectParameter enumCase = new BBObjectParameter(typeof(System.Enum));

        private int current;
        private int runningIndex;

        protected override Status OnExecute(Component agent, IBlackboard blackboard) {

            if ( outConnections.Count == 0 ) {
                return Status.Optional;
            }

            if ( status == Status.Resting || dynamic ) {

                if ( selectionMode == CaseSelectionMode.IndexBased ) {
                    current = intCase.value;
                    if ( outOfRangeMode == OutOfRangeMode.LoopIndex ) {
                        current = Mathf.Abs(current) % outConnections.Count;
                    }

                } else {
                    current = (int)enumCase.value;
                }

                if ( runningIndex != current ) {
                    outConnections[runningIndex].Reset();
                }

                if ( current < 0 || current >= outConnections.Count ) {
                    return Status.Failure;
                }
            }

            status = outConnections[current].Execute(agent, blackboard);

            if ( status == Status.Running ) {
                runningIndex = current;
            }

            return status;
        }


        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR

        public override string GetConnectionInfo(int i) {
            if ( selectionMode == CaseSelectionMode.EnumBased ) {
                if ( enumCase.value == null ) {
                    return "*Null Enum*";
                }
                var enumNames = System.Enum.GetNames(enumCase.value.GetType());
                if ( i >= enumNames.Length ) {
                    return "*Never*";
                }
                return enumNames[i];
            }
            return i.ToString();
        }

        protected override void OnNodeGUI() {
            if ( dynamic ) {
                GUILayout.Label("<b>DYNAMIC</b>");
            }
            GUILayout.Label(selectionMode == CaseSelectionMode.IndexBased ? ( "Current = " + intCase.ToString() ) : enumCase.ToString());
        }

        protected override void OnNodeInspectorGUI() {
            base.OnNodeInspectorGUI();
            if ( selectionMode == CaseSelectionMode.EnumBased ) {
                if ( enumCase.value != null ) {
                    GUILayout.BeginVertical("box");
                    foreach ( var s in System.Enum.GetNames(enumCase.value.GetType()) ) {
                        GUILayout.Label(s);
                    }
                    GUILayout.EndVertical();
                }
            }
        }

#endif
    }
}