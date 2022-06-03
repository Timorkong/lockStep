using CityParadoxNotion.Design;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using LitJson;

namespace FlowCanvas.Nodes
{

    [Category("City/主城")]
    [Description("主城")]
    public class MainCityLinkNode : CityNode
    {
        [ShowButton("导入城市节点", "LoadCityNodes")]
        [SerializeField]
        [Name("主城ID")]
        public int CityID = 0;

        [SerializeField]
        [Name("主城中文名")]
        public string ZhCnCityName = "";

        [SerializeField]
        [NameAttribute("开放等级")]
        public int OpenLevel = 0;

        [SerializeField]
        [Name("背景音乐ID")]
        public int BGM = 0;

        [SerializeField]
        [ExposeField]
        [GatherPortsCallback]
        [MinValue(1)]
        [DelayedField]
        [HideInInspector]
        [Name("节点数量")]
        private int _portCount = 1;

        [SerializeField]
        [HideInInspector]
        [Name("传送门节点ID：")]
        public List<int> NodeList = new List<int>();

        public override string name
        {
            get { return "主城：" + ZhCnCityName; }
        }

        //public override void OnGraphStarted() { original = open; }
        //public override void OnGraphStoped() { open = original; }


        protected override void RegisterPorts()
        {
            if (_portCount > 0)
            {
                int extra = NodeList.Count - _portCount;
                if (extra > 0)
                {
                    NodeList.RemoveRange(_portCount, extra);
                }
                else if (extra < 0)
                {
                    List<int> list = new List<int>(new int[-extra]);
                    NodeList.AddRange(list);
                }

            }
            var ins = new List<ValueInput<int>>();
            for (var i = 0; i < _portCount; i++)
            {
                AddValueInput<int>(i.ToString() + "_" + NodeList[i].ToString());
                AddValueOutput<int>(i.ToString() + "_" + NodeList[i].ToString(), () => { return 0; });
            }
        }

        public void LoadCityNodes()
        {
            string path = "Assets/CityEditror/config_city_json/city_" + CityID + ".json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JsonData cityConfig = JsonMapper.ToObject(json);
                JsonData transferList = cityConfig["transferList"];

                int nodeCount = 0;
                foreach (JsonData transfer in transferList)
                {
                    nodeCount++;
                }
                _portCount = nodeCount;
                int extra = NodeList.Count - nodeCount;
                if (extra > 0)
                {
                    NodeList.RemoveRange(nodeCount, extra);
                }
                else if (extra < 0)
                {
                    List<int> list = new List<int>(new int[-extra]);
                    NodeList.AddRange(list);
                }
                nodeCount = 0;
                foreach (JsonData transfer in transferList)
                {
                    string nodeID = transfer["nodeID"].ToString();
                    NodeList[nodeCount] = int.Parse(nodeID);
                    nodeCount++;
                }
                GatherPorts();
            }
            else
            {
                EditorUtility.DisplayDialog("提示", "主城" + CityID + "配置不存在", "确认", string.Empty);
                return;
            }
            EditorUtility.DisplayDialog("提示", "导入节点信息成功", "确认", string.Empty);
        }
    }
}