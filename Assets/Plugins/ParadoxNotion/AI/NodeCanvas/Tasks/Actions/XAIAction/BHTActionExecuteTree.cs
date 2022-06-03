using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("执行节点行为树")]
    [Category("剧情")]
    [System.Serializable]
    public class BHTActionExecuteTree : ActionTask
    {
        public BHTActionExecuteTree()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_EXECUTE_TREE;
        }

        [Name("参数")]
        public XBHTExecuteTreeConfig exeTreeConfig = new XBHTExecuteTreeConfig();

        protected override string info
        {
            get
            {
                return $"执行行为树 {exeTreeConfig.treeId}";
            }
        }

        virtual public Pbe.AIActionExecuteTreeConfig ToProto()
        {
            Pbe.AIActionExecuteTreeConfig cfg = new Pbe.AIActionExecuteTreeConfig()
            {
                TreeId = exeTreeConfig.treeId
            };
            return cfg;
        }

    }

    [SceneNode]
    [Name("执行节点行为树")]
    [Category("剧情")]
    [System.Serializable]
    public class BHTActionExecuteTreeSN : BHTActionExecuteTree
    {
        [BlackboardOnly]
        public BBParameter<int> unitID;

        protected override string info
        {
            get
            {
                return $"{unitID.name} 执行行为树 {exeTreeConfig.treeId}";
            }
        }

        override public Pbe.AIActionExecuteTreeConfig ToProto()
        {
            Pbe.AIActionExecuteTreeConfig cfg = new Pbe.AIActionExecuteTreeConfig()
            {
                TreeId = exeTreeConfig.treeId,
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
    public class XBHTExecuteTreeConfig
    {
        [Name("行为树 ID")]
        [SerializeField]
        public int treeId = 0;
    }
}
