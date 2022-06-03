using System.Collections.Generic;
using AINodeCanvas.Framework;
using AIParadoxNotion;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.BehaviourTrees
{

    /// Base class for BehaviourTree Composite nodes.
    abstract public class BTComposite : BTNode
    {

        public override string name { get { return base.name.ToUpper(); } }

        sealed public override int maxOutConnections { get { return -1; } }
        sealed public override Alignment2x2 commentsAlignment { get { return Alignment2x2.Right; } }

        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR

        protected override UnityEditor.GenericMenu OnContextMenu(UnityEditor.GenericMenu menu) {
            menu = base.OnContextMenu(menu);
            // menu = EditorUtils.GetTypeSelectionMenu(typeof(BTComposite), (t) => { this.ReplaceWith(t); }, menu, "Replace");
            menu = EditorUtils.GetTypeSelectionMenu(typeof(BTComposite), (t) => { this.ReplaceWith(t); }, menu, "更换节点类型");
            menu.AddItem(new GUIContent("Convert to SubTree"), false, () => { this.ConvertToSubTree(); });
            if ( outConnections.Count > 0 ) {
                menu.AddItem(new GUIContent("Duplicate Branch"), false, () => { this.DuplicateBranch(graph); });
                menu.AddItem(new GUIContent("Delete Branch"), false, () => { this.DeleteBranch(); });
            }
            return menu;
        }

#endif

    }
}