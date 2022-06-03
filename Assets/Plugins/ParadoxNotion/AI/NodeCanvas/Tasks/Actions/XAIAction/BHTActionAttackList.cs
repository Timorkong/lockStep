using System.Collections.Generic;
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;

namespace AINodeCanvas.Tasks.Actions
{
    [System.Serializable]
    public struct AttackConfig
    {
        [Name("延迟时间(ms)")]
        [MinValue(0)]
        public int delay;

        [Name("技能ID")]
        public int skillID;

        [Name("持续时间(ms)")]
        [MinValue(100)]
        public int duration;
    }

    [ActorNode]
    [Name("攻击序列")]
    [Category("指令")]
    [System.Serializable]
    public class BHTActionAttackList : ActionTask
    {
        public BHTActionAttackList()
        {
            detailType = EnumBTNodeAction.AINODE_ATTACK_LIST;
        }

        [Name("技能列表")]
        public List<AttackConfig> attackList = new List<AttackConfig>();

        protected override string info
        {
            get
            {
                var str = "攻击序列[";
                for (var i = 0; i < attackList.Count; i++)
                {
                    if (i > 0) str += ",";
                    str += attackList[i].skillID.ToString();
                }
                str += "]";
                return str;
            }
        }

        public Pbe.AIActionAttackListConfig ToProto()
        {
            Pbe.AIActionAttackListConfig cfg = new Pbe.AIActionAttackListConfig();
            for (var i = 0; i < attackList.Count; i++)
            {
                cfg.AttackList.Add(new Pbe.AIAttackNode()
                {
                    SkillID = attackList[i].skillID,
                    Delay = attackList[i].delay,
                    Duration = attackList[i].duration
                });
            }
            return cfg;
        }
    }
}