using AINodeCanvas.Framework;
using AIParadoxNotion.Design;

namespace AINodeCanvas.Tasks.Actions
{
    [SceneNode]
    [Name("开启黑幕")]
    [Category("剧情")]
    [System.Serializable]
    public class BHTActionBlackBarShow : ActionTask
    {
        public BHTActionBlackBarShow()
        {
            detailType = EnumBTNodeAction.AINODE_BLACK_BAR_SHOW;
        }

        [Name("持续时间")]
        public int duration = 1000;

        public Pbe.AIActionBlackBarConfig ToProto()
        {
            Pbe.AIActionBlackBarConfig cfg = new Pbe.AIActionBlackBarConfig()
            {
                Show = true,
                Duration = duration
            };
            return cfg;
        }
    }

}
