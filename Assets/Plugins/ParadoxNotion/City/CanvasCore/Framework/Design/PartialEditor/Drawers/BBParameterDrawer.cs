#if UNITY_EDITOR

using CityNodeCanvas.Framework;
using CityParadoxNotion.Design;
using UnityEngine;

namespace CityNodeCanvas.Editor {

    public class BBParameterDrawer : ObjectDrawer<BBParameter> {
        public override BBParameter OnGUI(GUIContent content, BBParameter instance) {
			var required = fieldInfo.IsDefined(typeof(RequiredFieldAttribute), true);
			var bbOnly = fieldInfo.IsDefined(typeof(BlackboardOnlyAttribute), true);
			var isNull = instance == null || instance.isNull;
			GUI.backgroundColor = required && isNull? Colors.lightRed : Color.white;
            instance = BBParameterEditor.ParameterField(content, instance, bbOnly, attributes);
			GUI.backgroundColor = Color.white;
			return instance;
        }
    }
}

#endif