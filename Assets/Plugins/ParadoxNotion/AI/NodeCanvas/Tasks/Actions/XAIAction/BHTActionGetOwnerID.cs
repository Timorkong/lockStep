using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("获取唯一ID")]
    [Description("获取AI所有者的唯一ID用于赋值")]
    [Category("参数")]
    [System.Serializable]
    public class BHTActionGetOwnerID : ActionTask
    {
        [BlackboardOnly]
        public BBParameter<int> ownerID = new BBParameter<int>(0);

        public BHTActionGetOwnerID()
        {
            detailType = EnumBTNodeAction.AINODE_GET_OWNER_ID;
        }

        protected override string info
        {
            get
            {
                return string.Format("获取AI的OwnerID");
            }
        }

        public BHTGetOwnerIDConfig ToJsonData()
        {
            BHTGetOwnerIDConfig cfg = new BHTGetOwnerIDConfig();
            cfg.config.ownerID = ownerID;
            return cfg;
        }

        public Pbe.AIActionGetOwnerIDConfig ToProto()
        {
            Pbe.AIActionGetOwnerIDConfig cfg = new Pbe.AIActionGetOwnerIDConfig()
            {
                Bbp = new Pbe.BBParam()
                {
                    Name = ownerID.name,
                    Type = XAIConfigTool.Type2TypeId(ownerID.varType)
                }
            };
            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTGetOwnerIDConfig
    {
        [Name("ID")]
        [SerializeField]
        public BBParameter<int> ownerID = new BBParameter<int>(0);
    }


    [System.Serializable]
    public class BHTGetOwnerIDConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTGetOwnerIDConfig config = new XBHTGetOwnerIDConfig();
    }
}
