using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("冒泡")]
    [Category("剧情")]
    [Description("头上冒泡泡")]
    public class BHTActionBubble : ActionTask
    {
        public BHTActionBubble()
        {
            detailType = EnumBTNodeAction.AINODE_TEXT_BUBBLE;
        }

        [Name("冒泡ID")]
        public int talkID = 0;

        protected override string info
        {
            get
            {
                return string.Format("泡泡: ID " + talkID);
            }
        }

        virtual public Pbe.AIActionBubbleConfig ToProto()
        {
            var cfg = new Pbe.AIActionBubbleConfig
            {
                TalkID = talkID
            };
            return cfg;
        }
    }

    [SceneNode]
    [Name("冒泡")]
    [Category("剧情")]
    [Description("头上冒泡泡")]
    public class BHTActionBubbleSN : BHTActionBubble
    {
        [BlackboardOnly]
        public BBParameter<int> unitID;

        override public Pbe.AIActionBubbleConfig ToProto()
        {
            var cfg = new Pbe.AIActionBubbleConfig
            {
                TalkID = talkID,
                UID = new Pbe.BBParam()
                {
                    Name = unitID.name,
                    Type = XAIConfigTool.Type2TypeId(unitID.varType)
                }
            };
            return cfg;
        }
    }
}
