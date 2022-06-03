using CityParadoxNotion.Design;
using System.Collections.Generic;
using UnityEngine;

namespace FlowCanvas.Nodes
{

    [Category("City/主城")]
    [Description("打开模块")]
    [Color("FA0F0F")]
    public class ModuleLinkNode : CityNode
    {
        [Name("模块名字")]
        public string strMenuName = "";

        [SerializeField]
        [Name("模块ID")]
        public int MenuID = 0;

        [SerializeField]
        [Name("关卡章节ID")]
        public int ChapterId = 0;

        [SerializeField]
        [ExposeField]
        [GatherPortsCallback]
        [MinValue(1)]
        [DelayedField]
        [Name("模块节点数")]
        private int _portCount = 1;

        public override string name
        {
            get { return "打开模块：" + strMenuName; }
        }

        //public override void OnGraphStarted() { original = open; }
        //public override void OnGraphStoped() { open = original; }

        protected override void RegisterPorts()
        {
            var ins = new List<ValueInput<int>>();
            for (var i = 0; i < _portCount; i++)
            {
                AddValueInput<int>(i.ToString());
                AddValueOutput<int>(i.ToString(), () => { return 0; });
            }
        }
    }
}