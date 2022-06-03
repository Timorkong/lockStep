using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode, SceneNode]
    [Name("等待")]
    [Category("剧情")]
    [System.Serializable]
    public class BHTActionWait : ActionTask
    {
        public BHTActionWait()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_WAIT;
        }

        [Name("参数(ms)")]
        public XBHTWaitConfig waitConfig = new XBHTWaitConfig();

        protected override string info
        {
            get
            {
                return string.Format("等待的时间为: " + (float)waitConfig.milliseconds / 1000.0 + " 秒");
            }
        }

        public Pbe.AIActionWaitConfig ToProto()
        {
            Pbe.AIActionWaitConfig cfg = new Pbe.AIActionWaitConfig()
            {
                Milliseconds = waitConfig.milliseconds
            };
            return cfg;
        }
    }

    [System.Serializable]
    public class XBHTWaitConfig
    {
        [Name("等待时间(ms)")]
        [SerializeField]
        public int milliseconds = 0;
    }
}
