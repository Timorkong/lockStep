using CityParadoxNotion.Serialization;
using CityParadoxNotion.Serialization.FullSerializer;

namespace CityNodeCanvas.Framework.Internal {

	public class MissingVariableType : Variable<object>, IMissingRecoverable{

		[fsProperty]
		public string missingType{get;set;}
		[fsProperty]
		public string recoveryState{get;set;}		
	}
}