/*
 * @Descripttion: 执行迂回靠近或远离
 * @Author: kevinwu
 * @Date: 2019-11-20 11:59:12
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;
using AINodeCanvas.Tasks.Conditions;


namespace AINodeCanvas.Tasks.Actions
{

    [Name("执行迂回靠近或远离")]
    [Category("NotSupport")]
    public class BHTActionClose : ActionTask<Transform>
    {
        public BHTActionClose()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_CLOSE;
        }


        [Name("参数")]
        public XBHTActionCloseConfig closeConfig = new XBHTActionCloseConfig();

        protected override string info
        {
            get
            {
                string tempString = "进入迂回状态,";
                if (closeConfig.limitNode == 0)
                {
                    tempString += "不打断";
                }
                else
                {
                    tempString += "被限制节点[" + closeConfig.limitNode + "]打断";
                }
                return tempString;
            }
        }

        public BHTActionCloseConfig ToJsonData()
        {
            BHTActionCloseConfig cfg = new BHTActionCloseConfig();
            cfg.config.limitNode = closeConfig.limitNode;
            cfg.config.breakType = closeConfig.breakType;
            return cfg;
        }
    }


    [System.Serializable]
    public class XBHTActionCloseConfig
    {
        [Name("打断条件(0不打断)")]
        [SerializeField]
        public int limitNode = 0;

        [Name("打断类型")]
        [SerializeField]
        public EnumBreakType breakType = EnumBreakType.绝对距离;
    }


    [System.Serializable]
    public class BHTActionCloseConfig : AINodeCanvas.Framework.Internal.BaseNodeData
    {
        public XBHTActionCloseConfig config = new XBHTActionCloseConfig();
        public XBHTConditionXandYConig limitConfigAbsolute = new XBHTConditionXandYConig();
        public XBHTConditionDistanceConig limitConfigRelative = new XBHTConditionDistanceConig();
    }
}