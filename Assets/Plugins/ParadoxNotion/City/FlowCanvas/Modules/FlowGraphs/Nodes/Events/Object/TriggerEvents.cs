using CityNodeCanvas.Framework;
using CityParadoxNotion.Design;
using UnityEngine;


namespace FlowCanvas.Nodes{

	[Name("Trigger")]
	[Category("Events/Object")]
	[Description("Called when Trigger based event happen on target")]
	public class TriggerEvents : MessageEventNode<Collider> {

		private FlowOutput onEnter;
		private FlowOutput onStay;
		private FlowOutput onExit;
		private Collider receiver;
		private GameObject other;


		protected override string[] GetTargetMessageEvents(){
			return new string[]{ "OnTriggerEnter", "OnTriggerStay", "OnTriggerExit" };
		}

		protected override void RegisterPorts(){
			onEnter = AddFlowOutput("Enter");
			onStay = AddFlowOutput("Stay");
			onExit = AddFlowOutput("Exit");
			AddValueOutput<Collider>("Receiver", ()=> { return receiver; });
			AddValueOutput<GameObject>("Other", ()=> { return other; });
		}

		void OnTriggerEnter(CityParadoxNotion.Services.MessageRouter.MessageData<Collider> msg){
			this.receiver = ResolveReceiver(msg.receiver);
			this.other = msg.value.gameObject;
			onEnter.Call(new Flow());
		}

		void OnTriggerStay(CityParadoxNotion.Services.MessageRouter.MessageData<Collider> msg){
			this.receiver = ResolveReceiver(msg.receiver);
			this.other = msg.value.gameObject;
			onStay.Call(new Flow());
		}

		void OnTriggerExit(CityParadoxNotion.Services.MessageRouter.MessageData<Collider> msg){
			this.receiver = ResolveReceiver(msg.receiver);
			this.other = msg.value.gameObject;
			onExit.Call(new Flow());
		}
	}
}