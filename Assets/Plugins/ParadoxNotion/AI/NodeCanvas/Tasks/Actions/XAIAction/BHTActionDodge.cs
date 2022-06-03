/*
 * @Descripttion: 躲避
 * @Author: kevinwu
 * @Date: 2019-12-25 17:26:03
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("躲避")]
    [Description("Y轴已经出一定的身位距离")]
    [Category("NotSupport")]
    [System.Serializable]
    public class BHTActionDodge : ActionTask<Transform>
    {
        public BHTActionDodge()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_DODGE;
        }

        protected override string info {
            get { return string.Format("躲避"); }
        }

        // protected override void OnExecute() {

        //     EndAction();
        // }

    }
}