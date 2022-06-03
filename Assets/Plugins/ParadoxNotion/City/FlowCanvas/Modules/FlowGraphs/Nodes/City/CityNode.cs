using CityParadoxNotion.Design;

namespace FlowCanvas.Nodes{

	[Category("City")]
	[Color("bf7fff")]
	[ContextDefinedInputs(typeof(Flow))]
	[ContextDefinedOutputs(typeof(Flow))]
	abstract public class CityNode : FlowNode { }

	public enum CityNodeType
	{
		MainCityNode,
		MiniRoomNode,
		ModuleNode
	}
}