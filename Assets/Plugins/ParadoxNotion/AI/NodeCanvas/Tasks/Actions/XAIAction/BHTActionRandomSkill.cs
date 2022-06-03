/*
 * @Descripttion: 随机一个技能
 * @Author: kevinwu
 * @Date: 2019-12-25 17:09:32
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("随机一个技能")]
    [Description("随机释放技能")]
    [Category("NotSupport")]
    [System.Serializable]
    public class BHTActionRandomSkill : ActionTask<Transform>
    {
        public BHTActionRandomSkill()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_RANDOM_SILL;
        }

        protected override string info {
            get { return string.Format("随机一个技能"); }
        }

        // protected override void OnExecute() {

        //     EndAction();
        // }

    }
}