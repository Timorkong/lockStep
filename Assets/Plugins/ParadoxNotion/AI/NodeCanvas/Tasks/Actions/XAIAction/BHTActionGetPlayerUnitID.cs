using AINodeCanvas.Framework;
using AIParadoxNotion.Design;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode, SceneNode]
    [Name("获取玩家唯一ID")]
    [Description("根据座位号获取玩家唯一ID")]
    [Category("参数")]
    public class BHTActionGetPlayerUnitID : ActionTask
    {
        [BlackboardOnly]
        [Name("Unit ID")]
        public BBParameter<int> unitID = new BBParameter<int>(0);

        [Name("座位号")]
        public int seat = 0;

        public BHTActionGetPlayerUnitID()
        {
            detailType = EnumBTNodeAction.AINODE_GET_PLAYER_UNIT_ID;
        }

        protected override string info
        {
            get
            {
                
                return $"获取玩家 {seat} 的唯一ID";
            }
        }

        public Pbe.AIActionGetPlayerUnitIDConfig ToProto()
        {
            var cfg = new Pbe.AIActionGetPlayerUnitIDConfig()
            {
                Bbp = new Pbe.BBParam()
                {
                    Name = unitID.name,
                    Type = XAIConfigTool.Type2TypeId(unitID.varType)
                },
                Seat = seat
            };
            return cfg;
        }
    }
}
