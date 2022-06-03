using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;
using System.Collections.Generic;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("获取范围内怪物数量")]
    [Description("获取半径对应圆范围内指定配置ID的怪物数量")]
    [Category("参数")]
    public class BHTActionGetMonsterCountInRange : ActionTask
    {
        public BHTActionGetMonsterCountInRange()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_GET_MONSTER_COUNT_IN_RANGE;
        }

        [Name("半径(万分比)")]
        public int radius = 0;

        [Name("怪物配置IDs")]
        public List<int> monsterIdList = new List<int>();

        [Name("获取的数量")]
        public BBParameter<int> count = new BBParameter<int>();

        protected override string info
        {
            get
            {
                return string.Format("获取范围内怪物数量: 半径 " + (float)radius / 10000.0);
            }
        }

        public Pbe.AIActionGetMonsterCountInRange ToProto()
        {
            var cfg = new Pbe.AIActionGetMonsterCountInRange()
            {
                Radius = radius,
                Count = new Pbe.BBParam()
                {
                    Name = count.name,
                    Type = XAIConfigTool.Type2TypeId(count.varType)
                }
            };
            foreach(var id in monsterIdList)
            {
                cfg.MonsterID.Add(id);
            }
            return cfg;
        }
    }
}
