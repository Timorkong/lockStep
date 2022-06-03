using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode, SceneNode]
    [Name("获取单位唯一ID")]
    [Description("获取场景中单位的唯一ID用于赋值")]
    [Category("参数")]
    public class BHTActionGetUnitID : ActionTask
    {
        [BlackboardOnly]
        [Name("Unit ID")]
        public BBParameter<int> unitID = new BBParameter<int>(0);

        [Name("单位场景唯一ID")]
        public int uniqueID = 0;

        public BHTActionGetUnitID()
        {
            detailType = EnumBTNodeAction.AINODE_GET_UNIT_ID;
        }

        protected override string info
        {
            get
            {
                return $"获取单位 {uniqueID} 的唯一ID";
            }
        }

        public Pbe.AIActionGetUnitIDConfig ToProto()
        {
            var cfg = new Pbe.AIActionGetUnitIDConfig()
            {
                Bbp = new Pbe.BBParam()
                {
                    Name = unitID.name,
                    Type = XAIConfigTool.Type2TypeId(unitID.varType)
                },
                UniqueID = uniqueID
            };
            return cfg;
        }
    }
}
