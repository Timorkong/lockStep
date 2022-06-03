using AINodeCanvas.Framework;
using AIParadoxNotion;
using AIParadoxNotion.Design;


namespace AINodeCanvas.Tasks.Actions
{

    [ActorNode, SceneNode]
    [Name("设置黑板整数值")]
    [Category("黑板")]
    [Description("设置一个黑板的整数变量值，之后可以获取这个值用于判断")]
    public class SetInt : ActionTask
    {

        [BlackboardOnly]
        public BBParameter<int> valueA;
        public OperationMethod Operation = OperationMethod.Set;
        public BBParameter<int> valueB;

        public SetInt()
        {
            detailType = EnumBTNodeAction.AINODE_ACTION_SET_BB_INTEGER;
        }

        protected override string info {
            get { return valueA + OperationTools.GetOperationString(Operation) + valueB; }
        }

        protected override void OnExecute() {
            valueA.value = OperationTools.Operate(valueA.value, valueB.value, Operation);
            EndAction();
        }

        public Pbe.AIActionSetBBIntegerConfig ToProto()
        {
            Pbe.AIActionSetBBIntegerConfig cfg = new Pbe.AIActionSetBBIntegerConfig()
            {
                BbInteger = new Pbe.BBParam()
                {
                    Name = valueA.name,
                    Type = XAIConfigTool.Type2TypeId(valueA.varType)
                },
                Operation = System.Convert.ToInt32(Operation),
                Value = valueB.value
            };
            return cfg;
        }
    }
}