using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{
    [SceneNode]
    [Name("停止剧情")]
    [Category("剧情")]
    public class BHTActionEndStory : ActionTask
    {
        public BHTActionEndStory()
        {
            detailType = EnumBTNodeAction.AINODE_END_STORY;
        }

    }
}