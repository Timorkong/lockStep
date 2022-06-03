using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("获取目标唯一ID")]
    [Description("获取AI的目标唯一ID用于赋值")]
    [Category("参数")]
    [System.Serializable]
    public class BHTActionGetTargetID : ActionTask
    {
        [BlackboardOnly]
        public BBParameter<int> targetID = new BBParameter<int>(0);

        public BHTActionGetTargetID()
        {
            detailType = EnumBTNodeAction.AINODE_GET_TARGET_ID;
        }

        protected override string info
        {
            get
            {
                return string.Format("获取AI的目标唯一ID");
            }
        }

        public BHTGetTargetIDConfig ToJsonData()
        {
            BHTGetTargetIDConfig cfg = new BHTGetTargetIDConfig();
            cfg.config.targetID = targetID;
            return cfg;
        }

        public Pbe.AIActionGetTargetIDConfig ToProto()
        {
            Pbe.AIActionGetTargetIDConfig cfg = new Pbe.AIActionGetTargetIDConfig()
            {
                Bbp = new Pbe.BBParam()
                {
                    Name = targetID.name,
                    Type = XAIConfigTool.Type2TypeId(targetID.varType)
                } 
            };
            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTGetTargetIDConfig
    {
        [Name("ID")]
        [SerializeField]
        public BBParameter<int> targetID = new BBParameter<int>(0);
    }


    [System.Serializable]
    public class BHTGetTargetIDConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTGetTargetIDConfig config = new XBHTGetTargetIDConfig();
    }
}
