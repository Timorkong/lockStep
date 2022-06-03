using AINodeCanvas.Framework;


namespace AINodeCanvas.DialogueTrees
{

    public class DTConnection : Connection
    {

        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR

        public override AIParadoxNotion.PlanarDirection direction {
            get { return AIParadoxNotion.PlanarDirection.Vertical; }
        }

#endif
        ///----------------------------------------------------------------------------------------------

    }
}