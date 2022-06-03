/*
 * @Descripttion: 寻找一个目标
 * @Author: kevinwu
 * @Date: 2019-11-20 11:59:12
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("找一个目标")]
    [Category("NotSupport")]
    public class BHTActionFindTarget : ActionTask<Transform>
    {
        public BHTActionFindTarget()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_FIND_TARGET;
        }
    }
}