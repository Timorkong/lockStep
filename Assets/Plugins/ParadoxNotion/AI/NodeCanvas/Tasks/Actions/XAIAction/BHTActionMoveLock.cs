using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("移动方向锁定")]
    [Category("NotSupport")]
    public class BHTActionMoveLock : ActionTask<Transform>
    {
        public BHTActionMoveLock()
        {
            detailType = EnumBTNodeAction.AINODE_MOVE_LOCK;
        }

        [Name("参数")]
        public MoveLockConf moveconfig = new MoveLockConf();

        protected override string info {
            get { return string.Format("移动方向: [" + moveconfig.moveDire + "]\n" + "移动类型: [" + moveconfig.moveType + "]\n"
                + "持续时间: [" + moveconfig.tickNum + "]"
                ); }
        }

        protected override void OnExecute() {

            EndAction();
        }

        public BHTActionMoveLockConfig ToJsonData()
        {
            BHTActionMoveLockConfig cfg = new BHTActionMoveLockConfig();
            cfg.config = this.moveconfig;
            return cfg;
        }

    }

    [System.Serializable]
    public class MoveLockConf
    {
        [Name("选择移动方向")]
        public EnumBTNodeDirction moveDire = EnumBTNodeDirction.右上;

        [Name("移动方式")]
        public EnumXAIMoveType moveType = EnumXAIMoveType.WALK;

        [Name("移动时间(s)")]
        [SerializeField]
        public float tickNum = 0;
    }

    [System.Serializable]
    public class BHTActionMoveLockConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
       public MoveLockConf config;
    }

}