﻿using System.Collections;
using CityParadoxNotion.Design;

namespace FlowCanvas.Nodes{

	[Description("Stops and cease execution of the FlowSript")]
	public class Finish : FlowControlNode {
		protected override void RegisterPorts(){
			var c = AddValueInput<bool>("Success");
			AddFlowInput("In", (f)=> { graph.Stop(c.value); });
		}
	}
}