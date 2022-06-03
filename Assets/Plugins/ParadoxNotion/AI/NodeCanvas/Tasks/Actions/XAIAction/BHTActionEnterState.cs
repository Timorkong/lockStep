/*
 * @Descripttion: AI行为-进入指定状态
 * @Author: kevinwu
 * @Date: 2019-12-06 16:35:28
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("进入状态")]
    [Category("NotSupport")]
    [System.Serializable]
    public class BHTActionEnterState : ActionTask<Transform>
    {
        public BHTActionEnterState()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_ENTER_STATE;
        }
        [Name("参数")]
        public XBHTEnterStateParam enterStateCfg = new XBHTEnterStateParam();


        protected override string info 
        {
            get 
            { 
                if (enterStateCfg.state == EnumAIState.移动)
                {
                    return string.Format("进入[" + enterStateCfg.state + "]状态\n" + 
                    enterStateCfg.moveDirction + enterStateCfg.moveAction + enterStateCfg.distance); 
                }
                else
                {
                    return string.Format("进入[" + enterStateCfg.state + "]状态"); 
                }
            }
        }

        // protected override void OnExecute() {

        //     EndAction();
        // }

        public BHTActionEnterStateConfig ToJsonData()
        {
            BHTActionEnterStateConfig cfg = new BHTActionEnterStateConfig();
            cfg.config.state = this.enterStateCfg.state;
            cfg.config.moveType = this.enterStateCfg.moveType;
            cfg.config.moveAction = this.enterStateCfg.moveAction;
            cfg.config.moveDirction = this.enterStateCfg.moveDirction;
            cfg.config.distance = this.enterStateCfg.distance;//.FloatToPBInt();
            //这里要提前进行下转换，当斜着走的时候，2a的平方等于固定距离，直接转换，避免游戏内出现小数
           /* if (cfg.config.moveDirction == EnumBTNodeDirction.右上 || 
                cfg.config.moveDirction == EnumBTNodeDirction.右下 ||
                cfg.config.moveDirction == EnumBTNodeDirction.左上 ||
                cfg.config.moveDirction == EnumBTNodeDirction.左下 )
                {
                    cfg.config.distance = Mathf.Floor(Mathf.Sqrt(cfg.config.distance/ 2.0f));
                }*/
            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTEnterStateParam
    {
        [Name("进入状态")]
        [SerializeField]
        public EnumAIState state = EnumAIState.未启动;

        [ShowIf("state", 1)]
        [Name("选择移动动作")]
        public EnumXAIMoveType moveType = EnumXAIMoveType.WALK;

        [ShowIf("state", 1)]
        [Name("选择移动类型")]
        public EnumXAIMoveAction moveAction = EnumXAIMoveAction.移动固定距离;

        [ShowIf("state", 1)]
        [Name("移动固定距离")]
        [RequiredField]
        public float distance = 0;

        [ShowIf("moveAction", 1)]
        [Name("移动方向")]
        public EnumBTNodeDirction moveDirction = EnumBTNodeDirction.向左;
    }

    [System.Serializable]
    public class BHTActionEnterStateConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        [HideInInspector]
        public XBHTEnterStateParam config = new XBHTEnterStateParam();
    }
}