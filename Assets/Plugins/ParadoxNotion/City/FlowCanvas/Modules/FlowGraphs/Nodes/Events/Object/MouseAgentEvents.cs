using CityParadoxNotion.Design;
using CityNodeCanvas.Framework;
using UnityEngine;

namespace FlowCanvas.Nodes{

	[Name("Mouse")]
	[Category("Events/Object")]
	[Description("Called when mouse based operations happen on target collider")]
	public class MouseAgentEvents : MessageEventNode<Collider> {

		private FlowOutput onEnter;
		private FlowOutput onOver;
		private FlowOutput onExit;
		private FlowOutput onDown;
		private FlowOutput onUp;
		private FlowOutput onDrag;
		
		private Collider receiver;
		private RaycastHit hit;

		protected override string[] GetTargetMessageEvents(){
			return new string[]{"OnMouseEnter", "OnMouseOver", "OnMouseExit", "OnMouseDown", "OnMouseUp", "OnMouseDrag"};
		}

		protected override void RegisterPorts(){
			onDown  = AddFlowOutput("Down");
			onUp    = AddFlowOutput("Up");
			onEnter = AddFlowOutput("Enter");
			onOver  = AddFlowOutput("Over");
			onExit  = AddFlowOutput("Exit");
			onDrag  = AddFlowOutput("Drag");
			AddValueOutput<Collider>("Receiver", ()=>{ return receiver; });
			AddValueOutput<RaycastHit>("Info", ()=>{ return hit; });
		}

		void OnMouseEnter(CityParadoxNotion.Services.MessageRouter.MessageData msg){
			this.receiver = ResolveReceiver(msg.receiver);
			StoreHit();
			onEnter.Call(new Flow());
		}

		void OnMouseOver(CityParadoxNotion.Services.MessageRouter.MessageData msg){
			this.receiver = ResolveReceiver(msg.receiver);
			StoreHit();
			onOver.Call(new Flow());
		}

		void OnMouseExit(CityParadoxNotion.Services.MessageRouter.MessageData msg){
			this.receiver = ResolveReceiver(msg.receiver);
			StoreHit();
			onExit.Call(new Flow());
		}

		void OnMouseDown(CityParadoxNotion.Services.MessageRouter.MessageData msg){
			this.receiver = ResolveReceiver(msg.receiver);
			StoreHit();
			onDown.Call(new Flow());
		}

		void OnMouseUp(CityParadoxNotion.Services.MessageRouter.MessageData msg){
			this.receiver = ResolveReceiver(msg.receiver);
			StoreHit();
			onUp.Call(new Flow());
		}

		void OnMouseDrag(CityParadoxNotion.Services.MessageRouter.MessageData msg){
			this.receiver = ResolveReceiver(msg.receiver);
			StoreHit();
			onDrag.Call(new Flow());
		}

		void StoreHit(){
			if (Camera.main != null){
				Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
			}
		}
	}
}