using AINodeCanvas.Framework;
using AIParadoxNotion.Design;


namespace AINodeCanvas.Tasks.Conditions
{

    [Category("✫ Utility")]
    [Description("Simply use to debug return true or false by inverting the condition if needed")]
    public class DebugCondition : ConditionTask
    {

        protected override bool OnCheck() {
            return true;
        }

        ////////////////////////////////////////
        ///////////GUI AND EDITOR STUFF/////////
        ////////////////////////////////////////
#if UNITY_EDITOR

        protected override void OnTaskInspectorGUI() {
            if ( UnityEngine.Application.isPlaying && UnityEngine.GUILayout.Button("Tick True") ) {
                YieldReturn(true);
            }
        }

#endif

    }
}