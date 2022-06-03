using AIParadoxNotion;
using AINodeCanvas.Framework;
using System.Linq;

namespace AINodeCanvas.BehaviourTrees
{

    /// Base class for BehaviourTree Decorator nodes.
    abstract public class BTDecorator : BTNode
    {
        public BTDecorator()
        {
            _NodeType = EnumBTNodeType.AINODE_TYPE_DECORATOR;
        }


        [UnityEngine.HideInInspector]
        [UnityEngine.SerializeField]
        public EnumBTNodeDecorator detailType;

        sealed public override int maxOutConnections { get { return 1; } }
        sealed public override Alignment2x2 commentsAlignment { get { return Alignment2x2.Right; } }

        ///The decorated connection element
        protected Connection decoratedConnection {
            get { return outConnections.Count > 0 ? outConnections[0] : null; }
        }

        ///The decorated node element
        protected Node decoratedNode {
            get
            {
                var c = decoratedConnection;
                return c != null ? c.targetNode : null;
            }
        }


        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR

        protected override UnityEditor.GenericMenu OnContextMenu(UnityEditor.GenericMenu menu) {
            menu = base.OnContextMenu(menu);
            menu = AIParadoxNotion.Design.EditorUtils.GetTypeSelectionMenu(typeof(BTDecorator), (t) => { this.ReplaceWith(t); }, menu, "Replace");
            return menu;
        }

#endif

    }
}