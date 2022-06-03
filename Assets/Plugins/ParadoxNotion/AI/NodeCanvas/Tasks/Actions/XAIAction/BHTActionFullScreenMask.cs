using AINodeCanvas.Framework;
using AIParadoxNotion.Design;

namespace AINodeCanvas.Tasks.Actions
{
    [SceneNode]
    [Name("开启全屏遮罩")]
    [Category("剧情")]
    [System.Serializable]
    public class BHTActionFullScreenMask : ActionTask
    {
        public BHTActionFullScreenMask()
        {
            detailType = EnumBTNodeAction.AINODE_FULL_SCREEN_MASK_SHOW;
        }

        [Name("持续时间 ms")]
        public int duration = 1000;

        protected override string info
        {
            get
            {
                return $"开启全屏遮罩  持续时间: {duration}毫秒";
            }
        }

        public Pbe.AIActionFullScreenMaskShow ToProto()
        {
            Pbe.AIActionFullScreenMaskShow cfg = new Pbe.AIActionFullScreenMaskShow()
            {
                Duration = duration
            };
            return cfg;
        }
    }

}
