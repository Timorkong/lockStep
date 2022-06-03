/*
 * @Descripttion: AI行为-文字框
 * @Author: colecai
 * @Date: 2020-12-15 10:58:21
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("随机显示指定组气泡框")]
    [Description("AI输出关联的文字框")]
    [Category("NotSupport")]
    [System.Serializable]
    public class BHTActionTextBubble : ActionTask<Transform>
    {
        public BHTActionTextBubble()
        {
            detailType = EnumBTNodeAction.AINODE_TEXT_BUBBLE;
        }
        [Name("参数")]
        public XBHTTextBubbleConfig textBubbleConfig = new XBHTTextBubbleConfig();

        protected override string info {
            get { return string.Format("随机选择" + this.textBubbleConfig.groupID); }
        }

        public BHTActionTextBubbleConfig ToJsonData()
        {
            BHTActionTextBubbleConfig cfg = new BHTActionTextBubbleConfig();
            cfg.config.groupID = textBubbleConfig.groupID;
            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTTextBubbleConfig
    {
        [Name("指定monster的组ID")]
        [SerializeField]
        public int groupID = 0;
    }

    [System.Serializable]
    public class BHTActionTextBubbleConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTTextBubbleConfig config = new XBHTTextBubbleConfig();
    }
}