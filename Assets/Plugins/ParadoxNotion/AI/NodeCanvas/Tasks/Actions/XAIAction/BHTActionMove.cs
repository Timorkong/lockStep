/*
 * @Descripttion: AI行为-移动
 * @Author: kevinwu
 * @Date: 2019-11-20 14:40:44
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{
    public enum EnumAbsoluteOrRelative
    {
        绝对坐标 = 0,
        相对位置 = 1,
    }

    [ActorNode]
    [Name("移动")]
    [Category("指令")]
    [Description("移动坐标类型分为2种：1、绝对坐标. 2、相对距离")]
    public class BHTActionMove : ActionTask
    {
        public BHTActionMove()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_MOVE;
        }

        [Name("坐标类型")]
        public EnumAbsoluteOrRelative abOrRel = EnumAbsoluteOrRelative.绝对坐标;

        [Name("坐标(万分比)")]
        [ShowIf("abOrRel", 0)]
        public Vector2 pos = Vector2.zero;

        [Name("相对X轴距离(万分比)")]
        [ShowIf("abOrRel", 1)]
        public int distanceX = 0;

        [Name("移动方式")]
        public EnumXAIMoveType moveType = EnumXAIMoveType.RUN;

        protected override string info 
        {
            get 
            { 
                if(abOrRel == EnumAbsoluteOrRelative.绝对坐标)
                    return string.Format("移动 绝对坐标：" + pos.x + "," + pos.y);
                else
                    return string.Format("移动 相对x距离：" + distanceX);
            }
        }

        virtual public Pbe.AIActionMoveConfig ToProto()
        {
            var cfg = new Pbe.AIActionMoveConfig()
            {
                AbsoluteOrRelative = System.Convert.ToInt32(abOrRel),
                MoveType = System.Convert.ToInt32(moveType)
            };
            if(abOrRel == EnumAbsoluteOrRelative.绝对坐标)
            {
                cfg.ArX = System.Convert.ToInt32(pos.x);
                cfg.ArZ = System.Convert.ToInt32(pos.y);
            }
            else
            {
                cfg.ArX = distanceX;
                cfg.ArZ = 0;
            }
            return cfg;
        }

        protected override void OnExecute() {
            EndAction();
        }

    }

    [SceneNode]
    [Name("移动")]
    [Category("指令")]
    [Description("移动坐标类型分为2种：1、绝对坐标. 2、相对距离")]
    public class BHTActionMoveSN : BHTActionMove
    {
        [BlackboardOnly]
        public BBParameter<int> unitID;

        override public Pbe.AIActionMoveConfig ToProto()
        {
            var cfg = new Pbe.AIActionMoveConfig()
            {
                AbsoluteOrRelative = System.Convert.ToInt32(abOrRel),
                MoveType = System.Convert.ToInt32(moveType),
                UID = new Pbe.BBParam()
                {
                    Name = unitID.name,
                    Type = XAIConfigTool.Type2TypeId(unitID.varType)
                }
            };
            if (abOrRel == EnumAbsoluteOrRelative.绝对坐标)
            {
                cfg.ArX = System.Convert.ToInt32(pos.x);
                cfg.ArZ = System.Convert.ToInt32(pos.y);
            }
            else
            {
                cfg.ArX = distanceX;
                cfg.ArZ = 0;
            }
            return cfg;
        }
    }

}