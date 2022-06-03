/*
 * @Descripttion: 进入巡逻状态
 * @Author: kevinwu
 * @Date: 2019-12-03 11:44:02
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("进入巡逻状态")]
    [Category("NotSupport")]
    [System.Serializable]
    public class BHTActionEnterPatrol : ActionTask<Transform>
    {
        public BHTActionEnterPatrol()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_ENTER_PATROL_STATE;
        }
        [Name("参数")]
        public XBHTEnterPatrolParam enterPatrolCfg = new XBHTEnterPatrolParam();
        protected override string info {

            get { 
                string tempString;

                if (enterPatrolCfg.patrolType == EnumXAIThinkType.DEFAULT)
                {
                    tempString = string.Format("进入巡逻状态\n" + "巡逻时间 [" + enterPatrolCfg.patrolTime + "s]\n" +
                    "巡逻范围 [" + "X:" + enterPatrolCfg.patrolX + ", Z:" + enterPatrolCfg.patrolZ + "]");
                } else
                {
                    tempString = string.Format("进入随机巡逻状态\n" + "巡逻时间 [" + enterPatrolCfg.patrolTime + "s]\n" +
                    "[" + "minX:" + enterPatrolCfg.minX + " , maxX " + enterPatrolCfg.maxX + "]\n"+ "[minX:" + 
                    enterPatrolCfg.minX + " , maxX " + enterPatrolCfg.maxX + "]\n");
                }
                return tempString;
            }

        }

        // protected override void OnExecute() {

        //     EndAction();
        // }

        public BHTActionEnterPatrolConfig ToJsonData()
        {
            BHTActionEnterPatrolConfig cfg = new BHTActionEnterPatrolConfig();
            cfg.config.patrolTime = this.enterPatrolCfg.patrolTime * BehaviourTreeDefine.TimeFrame;
            cfg.config.patrolX = this.enterPatrolCfg.patrolX.FloatToPBInt();
            cfg.config.patrolZ = this.enterPatrolCfg.patrolZ.FloatToPBInt();

            cfg.config.minX = this.enterPatrolCfg.minX.FloatToPBInt();
            cfg.config.minZ = this.enterPatrolCfg.minZ.FloatToPBInt();
            cfg.config.maxX = this.enterPatrolCfg.maxX.FloatToPBInt();
            cfg.config.maxZ = this.enterPatrolCfg.maxZ.FloatToPBInt();
            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTEnterPatrolParam
    {
        [Name("巡逻时间(s)")]
        [SerializeField]
        public int patrolTime = 0;

        [Name("迂回选择")]
        [SerializeField]
        public EnumXAIThinkType patrolType = EnumXAIThinkType.DEFAULT;


        [ShowIf("patrolType", (int)EnumXAIThinkType.DEFAULT)]
        [Name("巡逻范围X")]
        [SerializeField]
        public float patrolX = 0;

        [ShowIf("patrolType", (int)EnumXAIThinkType.DEFAULT)]
        [Name("巡逻范围Z")]
        [SerializeField]
        public float patrolZ = 0;


        [ShowIf("patrolType", (int)EnumXAIThinkType.RANDOM)]
        [Name("巡逻返回min_X")]
        [SerializeField]
        public float minX = 0;

        [ShowIf("patrolType", (int)EnumXAIThinkType.RANDOM)]
        [Name("巡逻返回max_X")]
        [SerializeField]
        public float maxX = 0;

        [ShowIf("patrolType", (int)EnumXAIThinkType.RANDOM)]
        [Name("巡逻返回min_Z")]
        [SerializeField]
        public float minZ = 0;

        [ShowIf("patrolType", (int)EnumXAIThinkType.RANDOM)]
        [Name("巡逻返回max_Z")]
        [SerializeField]
        public float maxZ = 0;

    }

    [System.Serializable]
    public class BHTActionEnterPatrolConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        [HideInInspector]
        public EnterStateParam stateParam = new EnterStateParam(EnumAIState.巡逻);
        public XBHTEnterPatrolParam config = new XBHTEnterPatrolParam();
    }
}