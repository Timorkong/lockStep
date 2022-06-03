using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode]
    [Name("待机")]
    [Category("指令")]

    public class BHTActionIdle : ActionTask
    {
        public BHTActionIdle()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_IDLE;
        }

        [Name("参数")]
        public XBHTIdleConfig idleConfig = new XBHTIdleConfig();
        protected override string info {
            get { return string.Format("待机:[" + idleConfig.idleType + "]"); }
        }

        // protected override void OnExecute() {

        //     EndAction();
        // }

        public BHTACtionIdleConfig ToJsonData()
        {
            BHTACtionIdleConfig cfg = new BHTACtionIdleConfig();
            cfg.config = this.idleConfig;
            return cfg;
        }

        public Pbe.AIActionIdleConfig ToProto()
        {
            Pbe.AIActionIdleConfig cfg = new Pbe.AIActionIdleConfig()
            {
                IdleType = (int)idleConfig.idleType
            };
            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTIdleConfig
    {   
        [Name("待机播放动作")]
        public EnumSkillActionType idleType = EnumSkillActionType.IDLE_01;
    }

    [System.Serializable]
    public class BHTACtionIdleConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTIdleConfig config;
    }

}