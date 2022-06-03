using AINodeCanvas.Framework;
using AIParadoxNotion.Design;

namespace AINodeCanvas.Tasks.Actions
{
    [SceneNode]
    [Name("关闭黑幕")]
    [Category("剧情")]
    [System.Serializable]
    public class BHTActionBlackBarHide : ActionTask
    {
        public BHTActionBlackBarHide()
        {
            detailType = EnumBTNodeAction.AINODE_BLACK_BAR_HIDE;
        }

        [Name("持续时间")]
        public int duration = 1000;

        public Pbe.AIActionBlackBarConfig ToProto()
        {
            Pbe.AIActionBlackBarConfig cfg = new Pbe.AIActionBlackBarConfig()
            {
                Show = false,
                Duration = duration
            };
            return cfg;
        }
    }

}
