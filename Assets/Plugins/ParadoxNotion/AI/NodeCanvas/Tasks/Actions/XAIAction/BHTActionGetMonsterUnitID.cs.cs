using AINodeCanvas.Framework;
using AIParadoxNotion.Design;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode, SceneNode]
    [Name("获取怪物唯一ID")]
    [Description("根据Monster表ID获取怪物唯一ID")]
    [Category("参数")]
    public class BHTActionGetMonsterUnitID : ActionTask
    {
        [BlackboardOnly]
        [Name("Unit ID")]
        public BBParameter<int> unitID = new BBParameter<int>(0);

        [Name("Monster表ID")]
        public int monsterTableID = 0;

        public BHTActionGetMonsterUnitID()
        {
            detailType = EnumBTNodeAction.AINODE_GET_MONSTER_UNIT_ID;
        }

        protected override string info
        {
            get
            {

                return $"获取表ID为：{monsterTableID}的怪物的唯一ID";
            }
        }

        public Pbe.AIActionGetMonsterUnitIDConfig ToProto()
        {
            var cfg = new Pbe.AIActionGetMonsterUnitIDConfig()
            {
                Bbp = new Pbe.BBParam()
                {
                    Name = unitID.name,
                    Type = XAIConfigTool.Type2TypeId(unitID.varType)
                },
                MonsterTableID = monsterTableID,
            };
            return cfg;
        }
    }
}
