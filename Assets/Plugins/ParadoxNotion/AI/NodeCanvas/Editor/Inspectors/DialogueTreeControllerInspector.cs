#if UNITY_EDITOR 

using AINodeCanvas.Framework;
using AINodeCanvas.DialogueTrees;
using AIParadoxNotion.Design;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace AINodeCanvas.Editor
{

    [CustomEditor(typeof(DialogueTreeController))]
    public class DialogueTreeControllerInspector : GraphOwnerInspector
    {

        private DialogueTreeController controller {
            get { return target as DialogueTreeController; }
        }

        protected override void OnExtraOptions() {
            if ( controller.graph != null ) {
                DialogueTreeInspector.ShowActorParameters((DialogueTree)controller.graph);
            }
        }
    }
}

#endif