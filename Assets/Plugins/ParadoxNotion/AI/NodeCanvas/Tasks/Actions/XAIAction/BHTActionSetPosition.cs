using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("设置位置")]
    [Category("指令")]
    [Description("设置位置")]
    public class BHTActionSetPosition : ActionTask
    {
        public BHTActionSetPosition()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_SET_POSITION;
        }

        [Name("世界坐标")]
        public Vector3 position = new Vector3();

        protected override string info
        {
            get
            {
                return $"设置位置 {position}";
            }
        }

        virtual public Pbe.AIActionSetPositionConfig ToProto()
        {
            var cfg = new Pbe.AIActionSetPositionConfig()
            {
                PosX = position.x,
                PosY = position.y,
                PosZ = position.z,
            };
            return cfg;
        }
    }

    [SceneNode]
    [Name("设置位置")]
    [Category("指令")]
    [Description("设置位置")]
    public class BHTActionSetPositionSN : BHTActionSetPosition
    {
        [BlackboardOnly]
        public BBParameter<int> unitID;

        protected override string info
        {
            get
            {
                return $"{unitID.name} 设置位置 {position}";
            }
        }

        override public Pbe.AIActionSetPositionConfig ToProto()
        {
            Pbe.AIActionSetPositionConfig cfg = new Pbe.AIActionSetPositionConfig
            {
                PosX = position.x,
                PosY = position.y,
                PosZ = position.z,
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
