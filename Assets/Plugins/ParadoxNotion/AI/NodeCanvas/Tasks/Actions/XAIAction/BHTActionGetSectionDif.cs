using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode, SceneNode]
    [Name("获取关卡难度")]
    [Description("获取关卡难度（1-普通；2 - 冒险；3 - 勇士；4 - 王者）")]
    [Category("参数")]
    public class BHTActionGetSectionDif : ActionTask
    {
        [BlackboardOnly]
        public BBParameter<int> ownerID = new BBParameter<int>(0);

        public BHTActionGetSectionDif()
        {
            detailType = EnumBTNodeAction.AINODE_GET_SECTION_DIF;
        }

        protected override string info
        {
            get
            {
                return "获取当前关卡难度";
            }
        }

        public Pbe.AIActionGetSectionDifConfig ToProto()
        {
            Pbe.AIActionGetSectionDifConfig cfg = new Pbe.AIActionGetSectionDifConfig()
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
}
