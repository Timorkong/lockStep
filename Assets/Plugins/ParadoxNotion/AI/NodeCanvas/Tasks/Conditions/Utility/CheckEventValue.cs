using System.Linq;
using AINodeCanvas.Framework;
using AIParadoxNotion;
using AIParadoxNotion.Design;
using AIParadoxNotion.Services;
using UnityEngine;
using Logger = AIParadoxNotion.Services.Logger;

namespace AINodeCanvas.Tasks.Conditions
{

    [Category("✫ Utility")]
    [Description("Check if an event is received and it's value is equal to specified value, then return true for one frame")]
    [EventReceiver("OnCustomEvent")]
    public class CheckEventValue<T> : ConditionTask<GraphOwner>
    {

        [RequiredField]
        public BBParameter<string> eventName;
        [Name("Compare Value To")]
        public BBParameter<T> value;

        protected override string info { get { return string.Format("Event [{0}].value == {1}", eventName, value); } }
        protected override bool OnCheck() { return false; }
        public void OnCustomEvent(EventData receivedEvent) {
            if ( isActive && receivedEvent.name.ToUpper() == eventName.value.ToUpper() ) {

                var receivedValue = receivedEvent.value;
                if ( object.Equals(receivedValue, value.value) ) {

#if UNITY_EDITOR
                    if ( AINodeCanvas.Editor.Prefs.logEvents ) {
                        Logger.Log(string.Format("Event Received from ({0}): '{1}'", agent.gameObject.name, receivedEvent.name), "Event", this);
                    }
#endif

                    YieldReturn(true);
                }
            }
        }
    }
}