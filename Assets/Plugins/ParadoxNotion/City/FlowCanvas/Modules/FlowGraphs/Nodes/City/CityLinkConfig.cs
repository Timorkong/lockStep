using FlowCanvas.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityNodeCanvas.Editor
{
    public class CityLinkConfig
    {
        [Serializable]
        public class MapLinkConfig
        {
            public List<MapLinkNode> nodes = new List<MapLinkNode>();
            public List<MapConnectionNode> connections = new List<MapConnectionNode>() ;

        }

        [SerializeField]
        public class MapLinkNode
        {
            public int CityID = 0;
            public string uid ="";
            public int RoomType;
            public string ZhCnCityName = "";
            public int OpenLevel ;
            public int BGM ;
            public int MenuID = 0;
            public int ChapterId = 0;
            public List<int> NodeList = new List<int>() ;
        }

        [SerializeField]
        public class MapConnectionNode
        {
            public int _type;
            public string srcNode;
            public string targetNode;
            public string _sourcePortName;
            public string _targetPortName;
        }

        //导出逻辑
        public static int GetRoomType(string str)
        {
            if (str == "FlowCanvas.Nodes.MainCityLinkNode")
            {
                return (int)CityNodeType.MainCityNode;
            } else if (str == "FlowCanvas.Nodes.MiniRoomLinkNode")
            {
                return (int)CityNodeType.MiniRoomNode;
            } else if (str == "FlowCanvas.Nodes.ModuleLinkNode")
            {
                return (int)CityNodeType.ModuleNode;
            }
            return -1;
        }
    }



}