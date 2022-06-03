/*
 * @Descripttion: 随机一招
 * @Author: kevinwu
 * @Date: 2019-12-25 17:09:23
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("随机一招")]
    [Description("随机释放一个普攻或技能")]
    [Category("NotSupport")]
    [System.Serializable]
    public class BHTActionRandomAttack : ActionTask<Transform>
    {
        public BHTActionRandomAttack()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_RANDOM_ATTACK;
        }

        protected override string info {
            get { return string.Format("随机一招"); }
        }

        // protected override void OnExecute() {

        //     EndAction();
        // }

    }
}