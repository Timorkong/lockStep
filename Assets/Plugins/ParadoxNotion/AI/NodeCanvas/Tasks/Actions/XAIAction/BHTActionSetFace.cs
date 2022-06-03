using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("设置朝向")]
    [Description("设置AI的朝向")]
    [Category("指令")]
    [System.Serializable]
    public class BHTActionSetFace : ActionTask
    {
        public BHTActionSetFace()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_SET_FACE;
        }

        [Name("参数")]
        public XBHTSetFaceConfig setFaceConfig = new XBHTSetFaceConfig();

        protected override string info
        {
            get
            {
                return $"设置朝向 {setFaceConfig.faceDir}";
            }
        }

        public BHTSetFaceConfig ToJsonData()
        {
            BHTSetFaceConfig cfg = new BHTSetFaceConfig();
            cfg.config.faceDir = setFaceConfig.faceDir;
            return cfg;
        }

        virtual public Pbe.AIActionSetFaceConfig ToProto()
        {
            Pbe.AIActionSetFaceConfig cfg = new Pbe.AIActionSetFaceConfig
            {
                FaceDir = System.Convert.ToInt32(setFaceConfig.faceDir)
            };

            return cfg;
        }

    }

    [SceneNode]
    [Name("设置朝向")]
    [Description("设置AI的朝向")]
    [Category("指令")]
    [System.Serializable]
    public class BHTActionSetFaceSN : BHTActionSetFace
    {
        [BlackboardOnly]
        public BBParameter<int> unitID;

        protected override string info
        {
            get
            {
                return $"{unitID.name} 设置朝向 {setFaceConfig.faceDir}";
            }
        }

        override public Pbe.AIActionSetFaceConfig ToProto()
        {
            Pbe.AIActionSetFaceConfig cfg = new Pbe.AIActionSetFaceConfig
            {
                FaceDir = System.Convert.ToInt32(setFaceConfig.faceDir),
                UID = new Pbe.BBParam()
                {
                    Name = unitID.name,
                    Type = XAIConfigTool.Type2TypeId(unitID.varType)
                }
            };

            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTSetFaceConfig
    {

        [Name("朝向")]
        [SerializeField]
        public EnumFace faceDir = EnumFace.左;

    }

    [System.Serializable]
    public class BHTSetFaceConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTSetFaceConfig config = new XBHTSetFaceConfig();
    }
}