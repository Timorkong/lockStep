using AINodeCanvas.Framework;
using UnityEngine;


namespace AINodeCanvas.BehaviourTrees
{

    ///The connection object for BehaviourTree nodes
    public class BTConnection : Connection
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