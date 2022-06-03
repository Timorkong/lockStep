using AINodeCanvas.Framework;
using AIParadoxNotion;
using AIParadoxNotion.Design;


namespace AINodeCanvas.Tasks.Conditions
{
    [ActorNode, SceneNode]
    [Name("对比整数值")]
    [Category("黑板")]
    [Description("获取黑板整数值和指定值进行对比")]
    public class CheckInt : ConditionTask
    {

        [BlackboardOnly]
        public BBParameter<int> valueA;
        public CompareMethod checkType = CompareMethod.EqualTo;
        public BBParameter<int> valueB;

        public CheckInt()
        {
            detailType = EnumBTNodeCondition.AINODE_CONDITION_CHECK_BB_INTEGER;
        }

        protected override string info {
            get { return valueA + OperationTools.GetCompareString(checkType) + valueB; }
        }

        protected override bool OnCheck() {
            return OperationTools.Compare((int)valueA.value, (int)valueB.value, checkType);
        }

        public Pbe.AIConditionCheckBBIntegerConfig ToProto()
        {
            Pbe.AIConditionCheckBBIntegerConfig cfg = new Pbe.AIConditionCheckBBIntegerConfig()
            {
                BbInteger = new Pbe.BBParam()
                {
                    Name = valueA.name,
                    Type = XAIConfigTool.Type2TypeId(valueA.varType)
                },
                CheckType = System.Convert.ToInt32(checkType),
                Value2Check = valueB.value
            };
            return cfg;
        }
    }
}