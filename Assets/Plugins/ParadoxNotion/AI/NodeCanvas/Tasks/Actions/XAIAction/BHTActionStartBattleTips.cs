using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [SceneNode]
    [Name("开启战斗引导")]
    [Description("通过 BattleTips表ID 开启战斗引导")]
    [Category("指令")]
    public class BHTActionStartBattleTips : ActionTask
    {
        public BHTActionStartBattleTips()
        {
            detailType = EnumBTNodeAction.AINODE_START_BATTLE_TIPS;
        }

        [Name("战斗引导ID")]
        public int battleTipsID = 0;

        [Name("持续时间(ms)")]
        public int duration = 0;

        protected override string info
        {
            get
            {
                return $"开启战斗引导{battleTipsID} 持续时间{duration}毫秒";
            }
        }

        public Pbe.AIActionBattleTipsConfig ToProto()
        {
            Pbe.AIActionBattleTipsConfig cfg = new Pbe.AIActionBattleTipsConfig()
            {
                TipsID = battleTipsID,
                Duration = duration
            };
            return cfg;
        }
    }
}