using System.Collections.Generic;
using UnityEngine;
using AINodeCanvas.BehaviourTrees;
using AINodeCanvas.Tasks.Actions;
using AINodeCanvas.Tasks.Conditions;
using System.Text;

namespace AINodeCanvas.Framework.Internal
{
    //行为树json数据
    ///The model used to serialize and deserialize graphs. This class serves no other purpose
    // [System.Serializable]
    public class GraphSerializationData
    {
        public const float FRAMEWORK_VERSION = 2.92f;

        [System.NonSerialized]
        public float version;

        // [System.NonSerialized]
        public System.Type type;

        [System.NonSerialized]
        public string name = string.Empty;

        [System.NonSerialized]
        public string category = string.Empty;

        [System.NonSerialized]
        public string comments = string.Empty;

        [System.NonSerialized]
        public Vector2 translation = default(Vector2);
        //
        // [System.NonSerialized]
        public float zoomFactor = 1f;

        [SerializeField]
        public List<Node> nodes = new List<Node>();
        [SerializeField]
        public List<Connection> connections = new List<Connection>();

        // [System.NonSerialized]
        public List<CanvasGroup> canvasGroups = null;

        // [System.NonSerialized]
        public BlackboardSource localBlackboard = null;

        // [System.NonSerialized]
        public object derivedData = null;
        //required
        public GraphSerializationData() { }

        //Construct
        public GraphSerializationData(Graph graph)
        {

            this.version = FRAMEWORK_VERSION;
            this.type = graph.GetType();
            this.category = graph.category;
            this.comments = graph.comments;
            this.translation = graph.translation;
            this.zoomFactor = graph.zoomFactor;
            this.nodes = graph.allNodes;
            this.canvasGroups = graph.canvasGroups;
            this.localBlackboard = graph.localBlackboard;

            //connections are serialized seperately and not part of their parent node
            var structConnections = new List<Connection>();
            for (var i = 0; i < nodes.Count; i++)
            {
                for (var j = 0; j < nodes[i].outConnections.Count; j++)
                {
                    structConnections.Add(nodes[i].outConnections[j]);
                }
            }

            this.connections = structConnections;

            //serialize derived data
            this.derivedData = graph.OnDerivedDataSerialization();
        }

        ///MUST reconstruct before using the data
        public void Reconstruct(Graph graph)
        {

            //check serialization versions here in the future if needed

            //re-link connections for deserialization
            for (var i = 0; i < this.connections.Count; i++)
            {
                connections[i].sourceNode.outConnections.Add(connections[i]);
                connections[i].targetNode.inConnections.Add(connections[i]);
            }

            //re-set the node's owner and ID
            for (var i = 0; i < this.nodes.Count; i++)
            {
                nodes[i].graph = graph;
                nodes[i].ID = i;
            }

            //deserialize derived data
            graph.OnDerivedDataDeserialization(derivedData);
        }

    }

    [System.Serializable]
    public class BHTNodeConnection
    {
        //父亲节点ID
        public int fatherID;
        //子节点ID
        public int childID;
    }

    [System.Serializable]
    public class BBParams
    {
        public string name;
        public int type;
    }

    [System.Serializable]
    public class AITreeConfig
    {
        //需要导出的节点数据
        public static List<BaseNodeData> nodeConfigs = new List<BaseNodeData>();

        [System.NonSerialized]
        private static List<Node> allNodes;
        [System.NonSerialized]
        private static List<Connection> connections = new List<Connection>();

        #region GetAITreeJson
        public static string GetAITreeConfig(List<Node> nodes, int treeID, IBlackboard blackboard)
        {
            StringBuilder builder = new StringBuilder();
/*            var structConnections = new List<Connection>();
            for (var i = 0; i < nodes.Count; i++)
            {
                for (var j = 0; j < nodes[i].outConnections.Count; j++)
                {
                    structConnections.Add(nodes[i].outConnections[j]);
                }
            }
            connections = structConnections;*/
            List<Node> limitNodes = getAllLimitNode(nodes);
            nodeConfigs.Clear();

            builder.Append("{\n");
            builder.Append("\"treeID\":" + treeID + ",\n");
            builder.Append("\"nodes\":");
            builder.Append("\n[\n");
            PreNodeToJsonNodeInfo(nodes, builder, limitNodes);
            builder.Append("]\n");

            //节点链接
            builder.Append(",\n");
            builder.Append("\"nodeConnections\":");
            builder.Append("\n[\n");
            PreNodeToJsonNodeConnection(nodes, builder);
            builder.Append("]\n");

            //黑板变量
            builder.Append(",\n");
            builder.Append("\"bbVars\":");
            builder.Append("\n[\n");
            PreNodeToJsonBlackboard(builder, blackboard);
            builder.Append("]\n");
            builder.Append("}\n");
            gNodeId = 0;
            return builder.ToString();
        }
        #endregion

        public static Pbe.AITree GetAITreeProto(List<Node> nodes, int treeID, IBlackboard blackboard)
        {
            Pbe.AITree aiTree = new Pbe.AITree();
            aiTree.TreeID = treeID;

            List<Node> limitNodes = getAllLimitNode(nodes);
            nodeConfigs.Clear();

            PreNodeToProtoNodeInfo(aiTree, nodes, limitNodes);

            PreNodeToProtoNodeConnection(aiTree, nodes);

            PreNodeToProtoBlackboard(aiTree, blackboard);

            gNodeId = 0;
            return aiTree;
        }

        public static Pbe.AITree GetAITreeProto(List<Node> nodes, int treeID, BBParameter[] bbParameters)
        {
            Pbe.AITree aiTree = new Pbe.AITree();
            aiTree.TreeID = treeID;

            List<Node> limitNodes = getAllLimitNode(nodes);
            nodeConfigs.Clear();

            PreNodeToProtoNodeInfo(aiTree, nodes, limitNodes);

            PreNodeToProtoNodeConnection(aiTree, nodes);

            PreNodeToProtoBlackboard(aiTree, bbParameters);

            gNodeId = 0;
            return aiTree;
        }

        //获取所有的条件限制节点
        public static List<Node> getAllLimitNode(List<Node> allNodes)
        {
            List<Node> limitsNode = new List<Node>();
            for (int i = 0; i < allNodes.Count; i++)
            {
                if (allNodes[i].name.Equals("条件限制"))
                {
                    allNodes[i].BeforeID = allNodes[i].ID;
                    limitsNode.Add(allNodes[i]);
                }
            }
            return limitsNode;
        }

        //递归调用输出NodeInfo
        static int gNodeId = 0; //全局节点ID
        #region PreNodeJson
        public static void PreNodeToJsonNodeInfo(List<Node> allNodes, StringBuilder builder,List<Node> limitNodes)
        {
            int step = 0;
            for (int i = 0; i < allNodes.Count; i++)
            {
                //针对subTree的json格式导出
                if (allNodes[i].name.Equals("SUBTREE"))
                {
/*                    for (int j = 0; j < ((SubTree)allNodes[i]).subTree.allNodes.Count; j++)
                    {
                        //更改子树的节点ID
                        ((SubTree)allNodes[i]).subTree.allNodes[j].ID = allNodes[i].ID + allSubTreeNode;
                        allSubTreeNode++;
                    }
                    allSubTreeNode--;*/
                    allNodes[i].ID = gNodeId;
                    PreNodeToJsonNodeInfo(((SubTree)allNodes[i]).subTree.allNodes, builder, limitNodes);
                }
                else
                {
                    // builder.Append(JsonUtility.ToJson(allNodes[i], true));
                    //if (!isSubTree) allNodes[i].ID += allSubTreeNode;
                    BHTreeNodeFactory(allNodes[i]._NodeType, allNodes[i], ref builder,ref step, limitNodes);
                    builder.Append("\n");
                    builder.Append(",");
                }
            }
        }

        //递归调用输出节点关系 
        public static void PreNodeToJsonNodeConnection(List<Node> nodes, StringBuilder builder)
        {
            for (var i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].name.Equals("SUBTREE"))
                {
                    PreNodeToJsonNodeConnection(((SubTree)nodes[i]).subTree.allNodes, builder);
                }
                else
                {
                    for (var j = 0; j < nodes[i].outConnections.Count; j++)
                    {
                        int subCount = 0;
                        Node targetNode = nodes[i].outConnections[j].targetNode;
                        if (targetNode.name.Equals("执行行为") && ((targetNode as ActionNode).task as ActionTask).detailType == EnumBTNodeAction.AINODE_COMBINE)
                        {
                            for (int k = 0; k < ((targetNode as ActionNode).task as ActionList).actions.Count; k++)
                            {
                                BHTNodeConnection nodeConn = new BHTNodeConnection();
                                nodeConn.fatherID = nodes[i].outConnections[j].sourceNode.ID;
                                nodeConn.childID = nodes[i].outConnections[j].targetNode.ID + subCount;
                                builder.Append(JsonUtility.ToJson(nodeConn, true));
                                subCount++;
                            }
                        }
                        else
                        {
                            BHTNodeConnection nodeConn = new BHTNodeConnection();
                            nodeConn.fatherID = nodes[i].outConnections[j].sourceNode.ID;
                            nodeConn.childID = nodes[i].outConnections[j].targetNode.ID;
                            builder.Append(JsonUtility.ToJson(nodeConn, true));
                        }
                    }
                }
            }
        }
        #endregion

        public static void PreNodeToProtoNodeInfo(Pbe.AITree aiTree, List<Node> allNodes, List<Node> limitNodes)
        {
            int step = 0;
            for(int i = 0; i < allNodes.Count; ++i)
            {
                if(allNodes[i].name.Equals("SUBTREE"))
                {
                    allNodes[i].ID = gNodeId;
                    PreNodeToProtoNodeInfo(aiTree, ((SubTree)allNodes[i]).subTree.allNodes, limitNodes);
                }
                else
                {
                    BHTreeNodeProtoFactory(aiTree, allNodes[i], ref step, limitNodes);
                }
            }
        }

        public static void PreNodeToJsonBlackboard(StringBuilder builder, IBlackboard blackboard)
        {
            var vars = blackboard.variables;
            foreach(var v in vars)
            {
                BBParams bbp = new BBParams();
                bbp.name = v.Key;
                bbp.type = XAIConfigTool.Type2TypeId(v.Value.varType);
                builder.Append(JsonUtility.ToJson(bbp, true));
            }
        }

        public static void PreNodeToProtoNodeConnection(Pbe.AITree aiTree, List<Node> nodes)
        {
            for(var i = 0; i < nodes.Count; ++i)
            {
                if (nodes[i].name.Equals("SUBTREE"))
                {
                    PreNodeToProtoNodeConnection(aiTree, ((SubTree)nodes[i]).subTree.allNodes);
                }
                else
                {
                    for (var j = 0; j < nodes[i].outConnections.Count; j++)
                    {
                        int subCount = 0;
                        Node targetNode = nodes[i].outConnections[j].targetNode;
                        if (targetNode.name.Equals("执行行为") && ((targetNode as ActionNode).task as ActionTask).detailType == EnumBTNodeAction.AINODE_COMBINE)
                        {
                            for (int k = 0; k < ((targetNode as ActionNode).task as ActionList).actions.Count; k++)
                            {
                                Pbe.AINodeConnection nodeConn = new Pbe.AINodeConnection();
                                nodeConn.FatherID = nodes[i].outConnections[j].sourceNode.ID;
                                nodeConn.ChildID = nodes[i].outConnections[j].targetNode.ID + subCount;
                                aiTree.NodeConnections.Add(nodeConn);
                                subCount++;
                            }
                        }
                        else
                        {
                            Pbe.AINodeConnection nodeConn = new Pbe.AINodeConnection();
                            nodeConn.FatherID = nodes[i].outConnections[j].sourceNode.ID;
                            nodeConn.ChildID = nodes[i].outConnections[j].targetNode.ID;
                            aiTree.NodeConnections.Add(nodeConn);
                        }
                    }
                }
            }
        }

        public static void PreNodeToProtoBlackboard(Pbe.AITree aiTree, BBParameter[] bbParameters)
        {
            List<string> info = new List<string>();
            foreach (var b in bbParameters)
            {
                if (!info.Contains(b.name))
                {
                    info.Add(b.name);

                    Pbe.BBParam bbp = new Pbe.BBParam();
                    bbp.Name = b.name;
                    bbp.Type = XAIConfigTool.Type2TypeId(b.varType);
                    aiTree.BbVars.Add(bbp);
                }
            }
        }

        public static void PreNodeToProtoBlackboard(Pbe.AITree aiTree, IBlackboard blackboard)
        {
            var vars = blackboard.variables;
            foreach(var v in vars)
            {
                Pbe.BBParam bbp = new Pbe.BBParam();
                bbp.Name = v.Key;
                bbp.Type = XAIConfigTool.Type2TypeId(v.Value.varType);
                aiTree.BbVars.Add(bbp);
            }
        }

        public static void BHTreeNodeProtoFactory(Pbe.AITree aiTree, Node node, ref int step, List<Node> limitNodes)
        {
            switch(node._NodeType)
            {
                case EnumBTNodeType.AINODE_TYPE_ACTION:
                    BHTreeActionProtoFactory(aiTree, node, ref step, limitNodes);
                    break;
                //条件 ConditionNode
                case EnumBTNodeType.AINODE_TYPE_CONDITION:
                    BHTreeConditionProtoFactory(aiTree, node, ref step);
                    node.ID = gNodeId - 1;
                    break;
                //顺序
                case EnumBTNodeType.AINODE_TYPE_SEQUEUE_CONTROL:
                //选择
                case EnumBTNodeType.AINODE_TYPE_SELECT_CONTROL:
                    Pbe.AITreeNode childNode = new Pbe.AITreeNode();
                    childNode.NodeType = System.Convert.ToInt32(node._NodeType);
                    childNode.NodeID = gNodeId++;
                    aiTree.Nodes.Add(childNode);
                    node.ID = gNodeId - 1;
                    break;
                //二元选择其实就是 true or false 条件选择，所以除了判断方式不一样，其他的都和 ConditionNode 一致
                case EnumBTNodeType.AINODE_TYPE_BINARY_SELECT_CONTROL:
                    BHTreeBinarySelectorProtoFactory(aiTree, node, ref step);
                    node.ID = gNodeId - 1;
                    break;
                //概率选择
                case EnumBTNodeType.AINODE_TYPE_RANDOM_SELECT_CONTROL:
                    Pbe.AITreeNode randNode = new Pbe.AITreeNode();
                    randNode.NodeType = System.Convert.ToInt32(node._NodeType);
                    randNode.ProbabilitySelectorConfig = (node as ProbabilitySelector).ToProto();
                    randNode.NodeID = gNodeId++;
                    aiTree.Nodes.Add(randNode);
                    node.ID = gNodeId - 1;
                    break;
                //修饰decorator
                case EnumBTNodeType.AINODE_TYPE_DECORATOR:
                    BHTreeDecoratorProtoFactory(aiTree, node, ref step);
                    node.ID = gNodeId - 1;
                    break;
                //并行
                case EnumBTNodeType.AINODE_TYPE_PARALLEL:
                    Pbe.AITreeNode parallelNode = new Pbe.AITreeNode();
                    parallelNode.NodeType = System.Convert.ToInt32(node._NodeType);
                    parallelNode.ParallelConfig = (node as Parallel).ToProto();
                    parallelNode.NodeID = gNodeId++;
                    aiTree.Nodes.Add(parallelNode);
                    node.ID = gNodeId - 1;
                    break;
            }
        }

        public static void BHTreeDecoratorProtoFactory(Pbe.AITree aiTree, Node node, ref int step)
        {
            Pbe.AITreeNode decoratorNode = new Pbe.AITreeNode();
            decoratorNode.Deco = new Pbe.AIDecorator();
            decoratorNode.NodeType = System.Convert.ToInt32(node._NodeType);
            decoratorNode.NodeID = gNodeId++;

            EnumBTNodeDecorator detailType = (node as BTDecorator).detailType;
            decoratorNode.Deco.DetailType = System.Convert.ToInt32(detailType);
            switch (detailType)
            {
                case EnumBTNodeDecorator.AINODE_DECORATOR_REPEAT:
                    decoratorNode.Deco.RepeaterConfig = (node as Repeater).ToProto();
                    aiTree.Nodes.Add(decoratorNode);
                    break;
            }
        }

        #region NodeFactoryJson
        public static void BHTreeNodeFactory(EnumBTNodeType _nodeType,Node _node, ref StringBuilder builder, ref int step, List<Node> limitNodes)
        {
            switch (_nodeType)
            {
                //行为
                case EnumBTNodeType.AINODE_TYPE_ACTION:
                    BHTreeActionFactory(_node, ref builder,ref step,limitNodes);
                    break;
                //条件
                case EnumBTNodeType.AINODE_TYPE_CONDITION:
                    BHTreeConditionFactory(_node, ref builder,ref step);
                    _node.ID = gNodeId - 1;
                    break;
                //二元选择其实就是 true or false 条件选择，所以除了判断方式不一样，其他的都和 ConditionNode 一致
                case EnumBTNodeType.AINODE_TYPE_BINARY_SELECT_CONTROL:
                    BHTreeConditionFactory(_node, ref builder, ref step);
                    _node.ID = gNodeId - 1;
                    break;
                //顺序
                case EnumBTNodeType.AINODE_TYPE_SEQUEUE_CONTROL:
                //选择
                case EnumBTNodeType.AINODE_TYPE_SELECT_CONTROL:
                    BaseNodeData childNode = new BaseNodeData();
                    childNode.nodeType = _node._NodeType;
                    //childNode.nodeID = _node.ID + step;
                    childNode.nodeID = gNodeId++;
                    builder.Append(JsonUtility.ToJson(childNode, true));
                    _node.ID = gNodeId - 1;
                    break;
                //概率选择
                case EnumBTNodeType.AINODE_TYPE_RANDOM_SELECT_CONTROL:
                    BHTProbabilitySelectorConfig randConfig = (_node as ProbabilitySelector).ToJsonData();
                    randConfig.nodeType = _node._NodeType;
                    //randConfig.nodeID = _node.ID + step;
                    randConfig.nodeID = gNodeId++;
                    builder.Append(JsonUtility.ToJson(randConfig, true));
                    _node.ID = gNodeId - 1;
                    break;
                //修饰
                case EnumBTNodeType.AINODE_TYPE_DECORATOR:
                    BHTreeDecoratorFactory(_node, ref builder,ref step);
                    _node.ID = gNodeId - 1;
                    break;
            }
        }

        //限制节点
        public static void BHTreeDecoratorFactory(Node _node, ref StringBuilder builder,ref int step)
        {
            EnumBTNodeDecorator _detailType = (_node as BTDecorator).detailType;
            switch (_detailType)
            {
                case EnumBTNodeDecorator.AINODE_DECORATOR_REVERSAL:
                    BaseNodeData childNode = new BaseNodeData();
                    AITreeConfig.ConvertDecoratorNodeData(childNode, _node,ref step);
                    builder.Append(JsonUtility.ToJson(childNode, true));
                    break;
                case EnumBTNodeDecorator.AINODE_DECORATOR_REPEAT:
                    BHTDecoratorRepeatConfig repeatCfg = (_node as Repeater).ToJsonData();
                    AITreeConfig.ConvertDecoratorNodeData(repeatCfg, _node, ref step);
                    builder.Append(JsonUtility.ToJson(repeatCfg, true));
                    break;
                case EnumBTNodeDecorator.AINODE_DECORATOR_TIME_INTERVAL:
                    BHTDecoratorTimeIntervalConfig timeIntervalCfg = (_node as TimeInterval).ToJsonData();
                    AITreeConfig.ConvertDecoratorNodeData(timeIntervalCfg, _node, ref step);
                    builder.Append(JsonUtility.ToJson(timeIntervalCfg, true));
                    break;
            }
        }

        public static void ConvertDecoratorNodeData(BaseNodeData nodeData, Node _node,ref int step)
        {
            EnumBTNodeDecorator _detailType = (_node as BTDecorator).detailType;
            int detailType = System.Convert.ToInt32(_detailType);
            nodeData.nodeType = _node._NodeType;
            //nodeData.nodeID = _node.ID + step;
            nodeData.nodeID = gNodeId++;
            nodeData.detailType = detailType;
        }
        #endregion

        public static void BHTreeBinarySelectorProtoFactory(Pbe.AITree aiTree, Node node, ref int step)
        {
            if((node as BinarySelector).task == null)
            {
                Debug.LogError("左右选择的条件为空");
                return;
            }
            Pbe.AITreeNode conditionNode = new Pbe.AITreeNode();
            conditionNode.Task = new Pbe.AITask();
            conditionNode.NodeType = System.Convert.ToInt32(node._NodeType);
            var conditionTask = (node as BinarySelector).task as ConditionTask;
            BHTreeFillConditionNodeProto(aiTree, conditionNode, conditionTask, ref step);
        }

        public static void BHTreeConditionProtoFactory(Pbe.AITree aiTree, Node node, ref int step)
        {
            if ((node as ConditionNode).condition == null)
            {
                Debug.LogError("条件不能为空");
                return;
            }
            Pbe.AITreeNode conditionNode = new Pbe.AITreeNode();
            conditionNode.Task = new Pbe.AITask();
            conditionNode.NodeType = System.Convert.ToInt32(node._NodeType);
            var conditionTask = (node as ConditionNode).task as ConditionTask;
            BHTreeFillConditionNodeProto(aiTree, conditionNode, conditionTask, ref step);
        }

        private static void BHTreeFillConditionNodeProto(Pbe.AITree aiTree, Pbe.AITreeNode conditionNode, ConditionTask conditionTask, ref int step)
        {
            EnumBTNodeCondition detailType = conditionTask.detailType;
            switch (detailType)
            {
                case EnumBTNodeCondition.AINODE_CONDITION_HP:
                    conditionNode.Task.CheckHPConfig = (conditionTask as BHTPreconditionHP).ToProto();
                    AITreeConfig.ConvertConditionNodeProto(conditionNode, conditionTask, ref step);
                    aiTree.Nodes.Add(conditionNode);
                    break;
                case EnumBTNodeCondition.AINODE_CONDITION_TARGET_ALIVE: //目标活着
                    AITreeConfig.ConvertConditionNodeProto(conditionNode, conditionTask, ref step);
                    aiTree.Nodes.Add(conditionNode);
                    break;
                //判断目标距离自己的X轴和Z轴距离
                case EnumBTNodeCondition.AINODE_CONDITION_DISTANCE_XZ:

                    conditionNode.Task.DistanceXYConfig = (conditionTask as BHTPreconditionXandZ).ToProto();
                    AITreeConfig.ConvertConditionNodeProto(conditionNode, conditionTask, ref step);
                    aiTree.Nodes.Add(conditionNode);
                    break;
                //与目标距离
                case EnumBTNodeCondition.AINODE_CONDITION_DISTANCE:
                    conditionNode.Task.DistanceTargetConfig = (conditionTask as BHTPreconditionDistance).ToProto();
                    AITreeConfig.ConvertConditionNodeProto(conditionNode, conditionTask, ref step);
                    aiTree.Nodes.Add(conditionNode);
                    break;
                //对比黑板整数值
                case EnumBTNodeCondition.AINODE_CONDITION_CHECK_BB_INTEGER:
                    conditionNode.Task.CheckBBIntegerConfig = (conditionTask as CheckInt).ToProto();
                    AITreeConfig.ConvertConditionNodeProto(conditionNode, conditionTask, ref step);
                    aiTree.Nodes.Add(conditionNode);
                    break;
                //是否有指定Buff
                case EnumBTNodeCondition.AINODE_CONDITION_HAS_BUFF:
                    conditionNode.Task.HasBuffConfig = (conditionTask as BHTPreconditionHasBuff).ToProto();
                    AITreeConfig.ConvertConditionNodeProto(conditionNode, conditionTask, ref step);
                    aiTree.Nodes.Add(conditionNode);
                    break;
                //是否在使用技能
                case EnumBTNodeCondition.AINODE_CONDITION_USING_SKILL:
                    conditionNode.Task.UsingSkillConfig = (conditionTask as BHTPreconditionUsingSkill).ToProto();
                    AITreeConfig.ConvertConditionNodeProto(conditionNode, conditionTask, ref step);
                    aiTree.Nodes.Add(conditionNode);
                    break;
                //是否可以释放技能
                case EnumBTNodeCondition.AINODE_CONDITION_CANUSE_SKILL:
                    conditionNode.Task.CanUseSkillConfig = (conditionTask as BHTPreconditionCanUseSkill).ToProto();
                    AITreeConfig.ConvertConditionNodeProto(conditionNode, conditionTask, ref step);
                    aiTree.Nodes.Add(conditionNode);
                    break;
                //检测单位状态
                case EnumBTNodeCondition.AINODE_CONDITION_UNIT_STATE:
                    conditionNode.Task.CheckUnitStateConfig = (conditionTask as BHTPreconditionUnitState).ToProto();
                    AITreeConfig.ConvertConditionNodeProto(conditionNode, conditionTask, ref step);
                    aiTree.Nodes.Add(conditionNode);
                    break;
                //判断伤害元素属性
                case EnumBTNodeCondition.AINODE_CONDITION_ELEMENT_DAMAGE:
                    conditionNode.Task.ElementDamageConfig = (conditionTask as BHTPreconditionElementDamage).ToProto();
                    AITreeConfig.ConvertConditionNodeProto(conditionNode, conditionTask, ref step);
                    aiTree.Nodes.Add(conditionNode);
                    break;
                case EnumBTNodeCondition.AINODE_CONDITION_ALWAYS_TRUE:  //总是返回真
                case EnumBTNodeCondition.AINODE_CONDITION_ALWAYS_FALSE: //总是返回假
                    AITreeConfig.ConvertConditionNodeProto(conditionNode, conditionTask, ref step);
                    aiTree.Nodes.Add(conditionNode);
                    break;
            }
        }


        #region ConditionFactoryJson
        //条件节点数据
        public static void BHTreeConditionFactory(Node _node, ref StringBuilder builder, ref int step)
        {
            if ((_node as ConditionNode).condition == null)
            {
                Debug.LogError("条件不能为空");
                return;
            }
            EnumBTNodeCondition _detailType = ((_node as ConditionNode).task as ConditionTask).detailType;
            switch (_detailType)
            {
                //与目标距离
                case EnumBTNodeCondition.AINODE_CONDITION_DISTANCE:
                    BHTPreconditionDistanceConig distanceCfg = ((_node as ConditionNode).task as BHTPreconditionDistance).ToJsonData();
                    AITreeConfig.ConvertConditionNodeData(distanceCfg, _node, ref step);
                    builder.Append(JsonUtility.ToJson(distanceCfg, true));
                    break;

                //AI运行时间
                case EnumBTNodeCondition.AINODE_CONDITION_RUNNING_TIME:
                    BHTPreconditionAIRunTickConig aIRunTickCfg = ((_node as ConditionNode).task as BHTPreconditionAIRunTick).ToJsonData();
                    AITreeConfig.ConvertConditionNodeData(aIRunTickCfg, _node, ref step);
                    builder.Append(JsonUtility.ToJson(aIRunTickCfg, true));
                    break;

                //血量
                case EnumBTNodeCondition.AINODE_CONDITION_HP:
                    BHTPreconditionHPConfig hpCfg = ((_node as ConditionNode).task as BHTPreconditionHP).ToJsonData();
                    AITreeConfig.ConvertConditionNodeData(hpCfg, _node, ref step);
                    builder.Append(JsonUtility.ToJson(hpCfg, true));
                    break;

                //当前状态
                case EnumBTNodeCondition.AINODE_CONDITION_NOW_STATE:
                    XBHTConditionCurStateConfig curStateCfg = ((_node as ConditionNode).task as BHTPreconditionCurState).ToJsonData();
                    AITreeConfig.ConvertConditionNodeData(curStateCfg, _node, ref step);
                    builder.Append(JsonUtility.ToJson(curStateCfg, true));
                    break;

                //上一次状态
                case EnumBTNodeCondition.AINODE_CONDITION_LAST_STATE:
                    XBHTConditionLastStateConfig lastStateCfg = ((_node as ConditionNode).task as BHTPreconditionLastState).ToJsonData();
                    AITreeConfig.ConvertConditionNodeData(lastStateCfg, _node, ref step);
                    builder.Append(JsonUtility.ToJson(lastStateCfg, true));
                    break;

                //敌人周边队友情况
                case EnumBTNodeCondition.AINODE_CONDITION_FRIND_INTARGET_RANGE:
                    XBHTConditionFriendsInTargetRangeConfig frindInTragetRangeCfg = ((_node as ConditionNode).task as BHTPreconditionFriendsNumInTargetRange).ToJsonData();
                    AITreeConfig.ConvertConditionNodeData(frindInTragetRangeCfg, _node, ref step);
                    builder.Append(JsonUtility.ToJson(frindInTragetRangeCfg, true));
                    break;

                case EnumBTNodeCondition.AINODE_CONDITION_IS_ATTACKING: //自己正在攻击
                case EnumBTNodeCondition.AINODE_CONDITION_TARGET_ATTACKING: //目标正在攻击
                case EnumBTNodeCondition.AINODE_CONDITION_TARGET_INRANGE: //目标在攻击范围
                case EnumBTNodeCondition.AINODE_CONDITION_TARGET_FALL:  //目标倒地
                case EnumBTNodeCondition.AINODE_CONDITION_HAVE_TARGET:  //有目标
                case EnumBTNodeCondition.AINODE_CONDITION_BEATTACK_FALL:    //自己已倒地
                case EnumBTNodeCondition.AINODE_CONDITION_TARGET_ALIVE: //目标活着
                case EnumBTNodeCondition.AINODE_CONDITION_FALL_AND_GET_UP: //倒地起身
                case EnumBTNodeCondition.AINODE_CONDITION_FACE_TARGET:   //面朝目标
                    BaseNodeData childNode = new BaseNodeData();
                    AITreeConfig.ConvertConditionNodeData(childNode, _node, ref step);
                    builder.Append(JsonUtility.ToJson(childNode, true));
                    break;
                //判断目标距离自己的X轴和Z轴距离
                case EnumBTNodeCondition.AINODE_CONDITION_DISTANCE_XZ:
                    BHTPreconditionXandZConig distanceXandZ = ((_node as ConditionNode).task as BHTPreconditionXandZ).ToJsonData();
                    AITreeConfig.ConvertConditionNodeData(distanceXandZ, _node, ref step);
                    builder.Append(JsonUtility.ToJson(distanceXandZ, true));
                    break;
                //判断AI智力
                case EnumBTNodeCondition.AINODE_CONDITION_INTELLIGENCE:
                    BHTPreconditionIntelligenceConig intelligenceConf = ((_node as ConditionNode).task as BHTPreconditionIntelligence).ToJsonData();
                    AITreeConfig.ConvertConditionNodeData(intelligenceConf, _node, ref step);
                    builder.Append(JsonUtility.ToJson(intelligenceConf, true));
                    break;
            }
        }

        public static void ConvertConditionNodeData(BaseNodeData nodeData, Node _node, ref int step)
        {
            EnumBTNodeCondition _detailType = ((_node as ConditionNode).task as ConditionTask).detailType;
            int detailType = System.Convert.ToInt32(_detailType);
            bool mask = ((_node as ConditionNode).task as ConditionTask).invert;
            nodeData.nodeType = _node._NodeType;
            //nodeData.nodeID = _node.ID + step;
            nodeData.nodeID = gNodeId++;
            nodeData.detailType = detailType;
            nodeData.mask = mask;
        }

        public static void ConvertConditionNodeData(BaseNodeData nodeData, Node _node)
        {
            EnumBTNodeCondition _detailType = ((_node as ConditionNode).task as ConditionTask).detailType;
            int detailType = System.Convert.ToInt32(_detailType);
            bool mask = ((_node as ConditionNode).task as ConditionTask).invert;
            nodeData.nodeType = _node._NodeType;
            //nodeData.nodeID = _node.ID;
            nodeData.nodeID = gNodeId++;
            nodeData.detailType = detailType;
            nodeData.mask = mask;
        }

        #endregion

        public static void ConvertConditionNodeProto(Pbe.AITreeNode treeNode, ConditionTask task, ref int step)
        {
            EnumBTNodeCondition enumDetailType = task.detailType;
            int detailType = System.Convert.ToInt32(enumDetailType);
            bool mask = task.invert;
            treeNode.NodeID = gNodeId++;
            treeNode.Task.DetailType = detailType;
            treeNode.Task.Invert = mask;
        }


        public static void ConvertActionNodeProto(Pbe.AITreeNode treeNode, Node node, ActionTask task, ref int step)
        {
            EnumBTNodeAction enumDetailType = task.detailType;
            int detailType = System.Convert.ToInt32(enumDetailType);
            treeNode.NodeType = System.Convert.ToInt32(node._NodeType);
            treeNode.NodeID = gNodeId++;
            treeNode.Task.DetailType = detailType;
        }


        public static void BHTreeActionProtoFactory(Pbe.AITree aiTree, Node node, ref int step, List<Node> limitNode)
        {
            if ((node as ActionNode).task == null)
            {
                Debug.LogError("行为不能为空");
                return;
            }

            List<ActionTask> taskList = new List<ActionTask>();
            EnumBTNodeAction detailType = ((node as ActionNode).task as ActionTask).detailType;
            node.ID = gNodeId;
            Pbe.AITreeNode actionNode = new Pbe.AITreeNode();
            actionNode.Task = new Pbe.AITask();
            actionNode.NodeType = System.Convert.ToInt32(node._NodeType);
            if (detailType == EnumBTNodeAction.AINODE_COMBINE)
            {
                taskList = ((node as ActionNode).task as ActionList).actions;
            }
            else
            {
                taskList.Add((node as ActionNode).task as ActionTask);
            }
            for(int i = 0; i < taskList.Count; ++i)
            {
                detailType = taskList[i].detailType;
                switch(detailType)
                {
                    //移动(剧情节点)
                    case EnumBTNodeAction.AINODE_ACTION_MOVE:
                        actionNode.Task.MoveConfig = (taskList[i] as BHTActionMove).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //添加buff
                    case EnumBTNodeAction.AINODE_ACTION_BUFF:
                        actionNode.Task.AddBuffConfig = (taskList[i] as BHTActionAddBuff).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //待机
                    case EnumBTNodeAction.AINODE_ACTION_IDLE:
                        actionNode.Task.IdleConfig = (taskList[i] as BHTActionIdle).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //保持距离跟随敌人
                    case EnumBTNodeAction.AINODE_FOLLOW_TARGET:
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //直接靠近
                    case EnumBTNodeAction.AINODE_DIRECTLY_APPROACH:
                        actionNode.Task.ApproachConfig = (taskList[i] as BHTActionDirectlyApproach).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //远离目标
                    case EnumBTNodeAction.AINODE_AWAY_TARGET:
                        actionNode.Task.AwayTargetConfig = (taskList[i] as BHTActionAwayTarget).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //游走
                    case EnumBTNodeAction.AINODE_WANDER:
                        actionNode.Task.WanderConfig = (taskList[i] as BHTActionWander).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //Z字靠近
                    case EnumBTNodeAction.AINODE_ZIG_APPROACH:
                        actionNode.Task.ZigApproachConfig = (taskList[i] as BHTActionZigApproach).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //设置朝向
                    case EnumBTNodeAction.AINODE_ACTION_SET_FACE:
                        actionNode.Task.SetFaceConfig = (taskList[i] as BHTActionSetFace).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //泡泡
                    case EnumBTNodeAction.AINODE_TEXT_BUBBLE:
                        actionNode.Task.BubbleConfig = (taskList[i] as BHTActionBubble).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //获取唯一ID                        
                    case EnumBTNodeAction.AINODE_GET_OWNER_ID:
                        actionNode.Task.GetOwnerIDConfig = (taskList[i] as BHTActionGetOwnerID).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //获取目标唯一ID
                    case EnumBTNodeAction.AINODE_GET_TARGET_ID:
                        actionNode.Task.GetTargetIDConfig = (taskList[i] as BHTActionGetTargetID).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //执行节点行为树
                    case EnumBTNodeAction.AINODE_ACTION_EXECUTE_TREE:
                        actionNode.Task.ExecuteTreeConfig = (taskList[i] as BHTActionExecuteTree).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //等待
                    case EnumBTNodeAction.AINODE_ACTION_WAIT:
                        actionNode.Task.WaitConfig = (taskList[i] as BHTActionWait).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //设置黑板整数值
                    case EnumBTNodeAction.AINODE_ACTION_SET_BB_INTEGER:
                        actionNode.Task.SetBBIntegerConfig = (taskList[i] as SetInt).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //释放技能
                    case EnumBTNodeAction.AINODE_ACTION_DO_SKILL:
                        actionNode.Task.DoSkillConfig = (taskList[i] as BHTActionDoSkill).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //执行动作
                    case EnumBTNodeAction.AINODE_ACTION_DO_ACTION:
                        actionNode.Task.DoActionConfig = (taskList[i] as BHTActionDoAction).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //添加 BuffInfo
                    case EnumBTNodeAction.AINODE_ACTION_ADD_BUFF_INFO:
                        actionNode.Task.AddBuffInfoConfig = (taskList[i] as BHTActionAddBuffInfo).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //删除 buff
                    case EnumBTNodeAction.AINODE_ACTION_REMOVE_BUFF:
                        actionNode.Task.RemoveBuffConfig = (taskList[i] as BHTActionRemoveBuff).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //删除 buffInfo 对应的 buff
                    case EnumBTNodeAction.AINODE_ACTION_REMOVE_BUFF_INFO:
                        actionNode.Task.RemoveBuffInfoConfig = (taskList[i] as BHTActionRemoveBuffInfo).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //获取单位唯一ID
                    case EnumBTNodeAction.AINODE_GET_UNIT_ID:
                        actionNode.Task.GetUnitIDConfig = (taskList[i] as BHTActionGetUnitID).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //获取范围内怪物数量
                    case EnumBTNodeAction.AINODE_ACTION_GET_MONSTER_COUNT_IN_RANGE:
                        actionNode.Task.GetRangedMonsterCountConfig = (taskList[i] as BHTActionGetMonsterCountInRange).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //剧情游荡
                    case EnumBTNodeAction.AINODE_ACTION_LOAF:
                        actionNode.Task.LoafConfig = (taskList[i] as BHTActionLoaf).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //自杀
                    case EnumBTNodeAction.AINODE_ACTION_SUICIDE:
                        actionNode.Task.SuicideConfig = (taskList[i] as BHTActionSuicide).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //开启黑幕
                    case EnumBTNodeAction.AINODE_BLACK_BAR_SHOW:
                        actionNode.Task.BlackBarConfig = (taskList[i] as BHTActionBlackBarShow).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //关闭黑幕
                    case EnumBTNodeAction.AINODE_BLACK_BAR_HIDE:
                        actionNode.Task.BlackBarConfig = (taskList[i] as BHTActionBlackBarHide).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //停止行为树
                    case EnumBTNodeAction.AINODE_END_STORY:
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //获取玩家唯一ID
                    case EnumBTNodeAction.AINODE_GET_PLAYER_UNIT_ID:
                        actionNode.Task.GetPlayerUnitIDConfig = (taskList[i] as BHTActionGetPlayerUnitID).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //找背靠近
                    case EnumBTNodeAction.AINODE_FINDBACK_APPROACH:
                        actionNode.Task.FindBackapproachConfig = (taskList[i] as BHTActionFindBackApproach).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //开启战斗引导
                    case EnumBTNodeAction.AINODE_START_BATTLE_TIPS:
                        actionNode.Task.BattleTipsConfig = (taskList[i] as BHTActionStartBattleTips).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //开启剧情对话
                    case EnumBTNodeAction.AINODE_SCENARIO:
                        actionNode.Task.ScenarioConfig = (taskList[i] as BHTActionScenario).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //设置按键状态
                    case EnumBTNodeAction.AINODE_BUTTON_STATE:
                        actionNode.Task.ButtonStateConfig = (taskList[i] as BHTActionButtonState).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //相机移动
                    case EnumBTNodeAction.AINODE_START_CAMERA_OFFSET:
                        actionNode.Task.StartCameraOffsetConfig = (taskList[i] as BHTActionStartCameraOffset).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //相机恢复
                    case EnumBTNodeAction.AINODE_CAMERA_RESET:
                        actionNode.Task.CameraResetConfig = (taskList[i] as BHTActionCameraReset).ToProto();   
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //设置位置
                    case EnumBTNodeAction.AINODE_ACTION_SET_POSITION:
                        actionNode.Task.SetPositionConfig = (taskList[i] as BHTActionSetPosition).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //获取怪物唯一ID
                    case EnumBTNodeAction.AINODE_GET_MONSTER_UNIT_ID:
                        actionNode.Task.GetMonsterUnitIDConfig = (taskList[i] as BHTActionGetMonsterUnitID).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //攻击序列
                    case EnumBTNodeAction.AINODE_ATTACK_LIST:
                        actionNode.Task.AttackListConfig = (taskList[i] as BHTActionAttackList).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //获取关卡难度
                    case EnumBTNodeAction.AINODE_GET_SECTION_DIF:
                        actionNode.Task.GetSectionDifConfig = (taskList[i] as BHTActionGetSectionDif).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //移除场景装饰物
                    case EnumBTNodeAction.AINODE_REMOVE_SCENE_ITEM:
                        actionNode.Task.RemoveSceneItemConfig = (taskList[i] as BHTActionRemoveSceneItem).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                    //开启全屏遮罩
                    case EnumBTNodeAction.AINODE_FULL_SCREEN_MASK_SHOW:
                        actionNode.Task.FullScreenMaskShowConfig = (taskList[i] as BHTActionFullScreenMask).ToProto();
                        AITreeConfig.ConvertActionNodeProto(actionNode, node, taskList[i], ref step);
                        aiTree.Nodes.Add(actionNode);
                        break;
                }
            }
        }

        #region ActionFactoryJson
        public static void ConvertActionNodeData(BaseNodeData nodeData, Node _node, ActionTask task, ref int step)
        {
            EnumBTNodeAction _detailType = task.detailType;
            int detailType = System.Convert.ToInt32(_detailType);
            nodeData.nodeType = _node._NodeType;
            //nodeData.nodeID = _node.ID + step;
            nodeData.nodeID = gNodeId++;
            nodeData.detailType = detailType;
        }
        //行为节点数据
        public static void BHTreeActionFactory(Node _node, ref StringBuilder builder,ref int step,List<Node> limitNode)
        {
            if ((_node as ActionNode).task == null)
            {
                Debug.LogError("行为不能为空");
                return;
            }

            List<ActionTask> taskList = new List<ActionTask>();
            EnumBTNodeAction _detailType = ((_node as ActionNode).task as ActionTask).detailType;
            _node.ID = gNodeId;
            if (_detailType == EnumBTNodeAction.AINODE_COMBINE)
            {
                taskList = ((_node as ActionNode).task as ActionList).actions;
            }
            else
            {
                taskList.Add((_node as ActionNode).task as ActionTask);
            }
            for (int i = 0; i < taskList.Count; i++)
            {
                _detailType = taskList[i].detailType;
                //if (i > 0) step++;
               // if (isSubTree) allSubTreeNode++;
                switch (_detailType)
                {
                    //进入状态
                    case EnumBTNodeAction.AINODE_ACTION_ENTER_STATE:
                        BHTActionEnterStateConfig enterStateCfg = (taskList[i] as BHTActionEnterState).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(enterStateCfg, _node,taskList[i],ref step);
                        builder.Append(JsonUtility.ToJson(enterStateCfg, true));
                        break;

                    //进入巡逻
                    case EnumBTNodeAction.AINODE_ACTION_ENTER_PATROL_STATE:
                        BHTActionEnterPatrolConfig enterPatrolCfg = (taskList[i] as BHTActionEnterPatrol).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(enterPatrolCfg, _node,taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(enterPatrolCfg, true));
                        break;
                    //巡逻
                    case EnumBTNodeAction.AINODE_ACTION_PATROL:
                        BHTActionPatrolConfig patrolCfg = (taskList[i] as BHTActionPatrol).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(patrolCfg, _node,taskList[i], ref step);
                        if (patrolCfg.config.limitNode != 0)
                        {
                            for (int j = 0; j < limitNode.Count; j++)
                            {
                                if (limitNode[j].BeforeID == patrolCfg.config.limitNode)
                                {
                                    EnumBTNodeCondition detailType = ((limitNode[j] as ConditionNode).task as ConditionTask).detailType;
                                    switch (detailType)
                                    {
                                        //判断目标距离自己的X轴和Z轴距离
                                        case EnumBTNodeCondition.AINODE_CONDITION_DISTANCE_XZ:
                                            BHTPreconditionXandZConig distanceXandZ = ((limitNode[j] as ConditionNode).task as BHTPreconditionXandZ).ToJsonData();
                                            patrolCfg.limitConfigAbsolute.xDistanceType = distanceXandZ.config.xDistanceType;
                                            patrolCfg.limitConfigAbsolute.xDistance = distanceXandZ.config.xDistance;
                                            patrolCfg.limitConfigAbsolute.zDistanceType = distanceXandZ.config.zDistanceType;
                                            patrolCfg.limitConfigAbsolute.zDistance = distanceXandZ.config.zDistance;
                                            patrolCfg.limitConfigAbsolute.calType = distanceXandZ.config.calType;
                                            builder.Append(JsonUtility.ToJson(patrolCfg, true));
                                            break;
                                        case EnumBTNodeCondition.AINODE_CONDITION_DISTANCE:
                                            BHTPreconditionDistanceConig distance = ((limitNode[j] as ConditionNode).task as BHTPreconditionDistance).ToJsonData();
                                            patrolCfg.limitConfigRelative.distanceType = distance.config.distanceType;
                                            patrolCfg.limitConfigRelative.compareType = distance.config.compareType;
                                            patrolCfg.limitConfigRelative.distance = distance.config.distance;
                                            builder.Append(JsonUtility.ToJson(patrolCfg, true));
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            builder.Append(JsonUtility.ToJson(patrolCfg, true));
                        }
                        break;
                    //攻击
                    case EnumBTNodeAction.AINODE_ACTION_SKILL:
                        BHTActionAttackConfig attackCfg = (taskList[i] as BHTActionAttack).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(attackCfg, _node,taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(attackCfg, true));
                        break;

                    //待机
                    case EnumBTNodeAction.AINODE_ACTION_IDLE:
                        BHTACtionIdleConfig idleCfg = (taskList[i] as BHTActionIdle).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(idleCfg, _node,taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(idleCfg, true));
                        break;

                    //停止
                    case EnumBTNodeAction.AINODE_TIME_STOP:
                        BHTActionTimeStopConfig timeStopCfg = (taskList[i] as BHTActionTimeStop).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(timeStopCfg, _node,taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(timeStopCfg, true));
                        break;

                    //保持距离跟随敌人
                    case EnumBTNodeAction.AINODE_FOLLOW_TARGET:
                        BHTActionFollowTargetConfig followTargetCfg = (taskList[i] as BHTActionFollowTarget).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(followTargetCfg, _node,taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(followTargetCfg, true));
                        break;

                    //靠近目标
                    case EnumBTNodeAction.AINODE_ACTION_CLOST_TARGET:
                        BHTActionCloseTargetConfig closeTargetCfg = (taskList[i] as BHTActionCloseTarget).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(closeTargetCfg, _node,taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(closeTargetCfg, true));
                        break;
                    //更换目标
                    case EnumBTNodeAction.AINODE_CHANGE_TARGET:
                    //移动
                    case EnumBTNodeAction.AINODE_ACTION_MOVE:
                    //躲避
                    case EnumBTNodeAction.AINODE_ACTION_DODGE:
                    //随机一招
                    case EnumBTNodeAction.AINODE_ACTION_RANDOM_ATTACK:
                    //随机一个技能
                    case EnumBTNodeAction.AINODE_ACTION_RANDOM_SILL:
                    //寻找目标
                    case EnumBTNodeAction.AINODE_ACTION_FIND_TARGET:
                        BaseNodeData childNode = new BaseNodeData();
                        AITreeConfig.ConvertActionNodeData(childNode, _node, taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(childNode, true));
                        break;
                    //循环靠近或远离
                    case EnumBTNodeAction.AINODE_ACTION_CYCLE_CLOSE:
                        BHTActionCycleCloseConfig cycleCloseCfg = (taskList[i] as BHTActionCycleClose).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(cycleCloseCfg, _node, taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(cycleCloseCfg, true));
                        break;
                    //随机释放技能(可配置)
                    case EnumBTNodeAction.AINODE_ACTION_RANDOM_SKILL_SET:
                        BHTActionRandomSkillSetConfig randomTargetCfg = (taskList[i] as BHTActionRandomSkillSet).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(randomTargetCfg, _node, taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(randomTargetCfg, true));
                        break;
                    //移动方向锁定
                    case EnumBTNodeAction.AINODE_MOVE_LOCK:
                        BHTActionMoveLockConfig moveLockCfg = (taskList[i] as BHTActionMoveLock).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(moveLockCfg, _node, taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(moveLockCfg, true));
                        break;
                    //思考
                    case EnumBTNodeAction.AINODE_ACTION_THINK:
                        BHTActionThinkConfig thinkCfg = (taskList[i] as BHTActionThink).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(thinkCfg, _node, taskList[i], ref step);
                        if (thinkCfg.config.limitNode != 0)
                        {
                            for (int j = 0; j < limitNode.Count; j++)
                            {
                                if (limitNode[j].BeforeID == thinkCfg.config.limitNode)
                                {
                                    EnumBTNodeCondition detailType = ((limitNode[j] as ConditionNode).task as ConditionTask).detailType;
                                    switch (detailType)
                                    {
                                        //判断目标距离自己的X轴和Z轴距离
                                        case EnumBTNodeCondition.AINODE_CONDITION_DISTANCE_XZ:
                                            BHTPreconditionXandZConig distanceXandZ = ((limitNode[j] as ConditionNode).task as BHTPreconditionXandZ).ToJsonData();
                                            thinkCfg.limitConfigAbsolute.xDistanceType = distanceXandZ.config.xDistanceType;
                                            thinkCfg.limitConfigAbsolute.xDistance = distanceXandZ.config.xDistance;
                                            thinkCfg.limitConfigAbsolute.zDistanceType = distanceXandZ.config.zDistanceType;
                                            thinkCfg.limitConfigAbsolute.zDistance = distanceXandZ.config.zDistance;
                                            thinkCfg.limitConfigAbsolute.calType = distanceXandZ.config.calType;
                                            builder.Append(JsonUtility.ToJson(thinkCfg, true));
                                            break;
                                        case EnumBTNodeCondition.AINODE_CONDITION_DISTANCE:
                                            BHTPreconditionDistanceConig distance = ((limitNode[j] as ConditionNode).task as BHTPreconditionDistance).ToJsonData();
                                            thinkCfg.limitConfigRelative.distanceType = distance.config.distanceType;
                                            thinkCfg.limitConfigRelative.compareType = distance.config.compareType;
                                            thinkCfg.limitConfigRelative.distance = distance.config.distance;
                                            builder.Append(JsonUtility.ToJson(thinkCfg, true));
                                            break;
                                    }
                                } 
                            }
                        } else
                        {
                            builder.Append(JsonUtility.ToJson(thinkCfg, true));
                        }
                        break;
                    //开始迂回靠近
                    case EnumBTNodeAction.AINODE_ACTION_CLOSE:
                        BHTActionCloseConfig closeCfg = (taskList[i] as BHTActionClose).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(closeCfg, _node, taskList[i], ref step);
                        if (closeCfg.config.limitNode != 0)
                        {
                            for (int j = 0; j < limitNode.Count; j++)
                            {
                                if (limitNode[j].BeforeID == closeCfg.config.limitNode)
                                {
                                    EnumBTNodeCondition detailType = ((limitNode[j] as ConditionNode).task as ConditionTask).detailType;
                                    switch (detailType)
                                    {
                                        //判断目标距离自己的X轴和Z轴距离
                                        case EnumBTNodeCondition.AINODE_CONDITION_DISTANCE_XZ:
                                            BHTPreconditionXandZConig distanceXandZ = ((limitNode[j] as ConditionNode).task as BHTPreconditionXandZ).ToJsonData();
                                            closeCfg.limitConfigAbsolute.xDistanceType = distanceXandZ.config.xDistanceType;
                                            closeCfg.limitConfigAbsolute.xDistance = distanceXandZ.config.xDistance;
                                            closeCfg.limitConfigAbsolute.zDistanceType = distanceXandZ.config.zDistanceType;
                                            closeCfg.limitConfigAbsolute.zDistance = distanceXandZ.config.zDistance;
                                            closeCfg.limitConfigAbsolute.calType = distanceXandZ.config.calType;
                                            builder.Append(JsonUtility.ToJson(closeCfg, true));
                                            break;
                                        case EnumBTNodeCondition.AINODE_CONDITION_DISTANCE:
                                            BHTPreconditionDistanceConig distance = ((limitNode[j] as ConditionNode).task as BHTPreconditionDistance).ToJsonData();
                                            closeCfg.limitConfigRelative.distanceType = distance.config.distanceType;
                                            closeCfg.limitConfigRelative.compareType = distance.config.compareType;
                                            closeCfg.limitConfigRelative.distance = distance.config.distance;
                                            builder.Append(JsonUtility.ToJson(closeCfg, true));
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            builder.Append(JsonUtility.ToJson(closeCfg, true));
                        }
                        break;
                    //随机显示文字框
                    case EnumBTNodeAction.AINODE_TEXT_BUBBLE:
                        BHTActionTextBubbleConfig textBubbleConfig = (taskList[i] as BHTActionTextBubble).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(textBubbleConfig, _node, taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(textBubbleConfig, true));
                        break;
                    //直接靠近
                    case EnumBTNodeAction.AINODE_DIRECTLY_APPROACH:
                        BHTDirectlyApproachConfig approachConfig = (taskList[i] as BHTActionDirectlyApproach).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(approachConfig, _node, taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(approachConfig, true));
                        break;
                    //远离目标
                    case EnumBTNodeAction.AINODE_AWAY_TARGET:
                        BHTAwayTargetConfig awayTargetConfig = (taskList[i] as BHTActionAwayTarget).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(awayTargetConfig, _node, taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(awayTargetConfig, true));
                        break;
                    //游走
                    case EnumBTNodeAction.AINODE_WANDER:
                        BHTWanderConfig wanderConfig = (taskList[i] as BHTActionWander).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(wanderConfig, _node, taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(wanderConfig, true));
                        break;
                    //Z字靠近
                    case EnumBTNodeAction.AINODE_ZIG_APPROACH:
                        BHTZigApproachConfig zigApproachConfig = (taskList[i] as BHTActionZigApproach).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(zigApproachConfig, _node, taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(zigApproachConfig, true));
                        break;
                    //设置朝向
                    case EnumBTNodeAction.AINODE_ACTION_SET_FACE:
                        BHTSetFaceConfig setFaceConfig = (taskList[i] as BHTActionSetFace).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(setFaceConfig, _node, taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(setFaceConfig, true));
                        break;
                    //获取自身唯一ID
                    case EnumBTNodeAction.AINODE_GET_OWNER_ID:
                        BHTGetOwnerIDConfig getOwnerIDConfig = (taskList[i] as BHTActionGetOwnerID).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(getOwnerIDConfig, _node, taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(getOwnerIDConfig, true));
                        break;
                    //获取目标唯一ID
                    case EnumBTNodeAction.AINODE_GET_TARGET_ID:
                        BHTGetTargetIDConfig getTargetIDConfig = (taskList[i] as BHTActionGetTargetID).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(getTargetIDConfig, _node, taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(getTargetIDConfig, true));
                        break;
                    //找背靠近
                    case EnumBTNodeAction.AINODE_FINDBACK_APPROACH:
                        BHTFindBackApproachConfig findBackApproachConfig = (taskList[i] as BHTActionFindBackApproach).ToJsonData();
                        AITreeConfig.ConvertActionNodeData(findBackApproachConfig, _node, taskList[i], ref step);
                        builder.Append(JsonUtility.ToJson(findBackApproachConfig, true));
                        break;
                }
            }
        }
        #endregion
    }


    [System.Serializable]
    public class BaseNodeData
    {
        [HideInInspector]
        public int nodeID = -1;  //节点ID
        [HideInInspector]
        public EnumBTNodeType nodeType;    //节点类型
        [HideInInspector]
        public int detailType = 0;  //节点数据类型-行为节点

        [HideInInspector]
        public bool mask = false;    //条件取反，只针对于条件节点
        public BaseNodeData()
        {
        }
    }

    [System.Serializable]
    public class DistanceXandZConf
    {
        public EnumBTNodeCompareSign xDistanceType = EnumBTNodeCompareSign.等于;
        public float xDistance = 0;
        public EnumBTNodeCompareSign zDistanceType = EnumBTNodeCompareSign.等于;
        public float zDistance = 0;
        public EnumBTNodeCoditionCal calType = EnumBTNodeCoditionCal.且;
        public DistanceXandZConf()
        {
        }
    }


}