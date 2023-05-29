using UnityEngine;

namespace _Scripts.Attributes
{
	public class ConditionalHideAttribute : PropertyAttribute
	{
		public string ConditionalSourceField = "";
		public bool HideInInspector = false;
		
		public ConditionalHideAttribute(string conditionalSourceField)
		{
			ConditionalSourceField = conditionalSourceField;
			HideInInspector = false;
		}

		public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
		{
			ConditionalSourceField = conditionalSourceField;
			HideInInspector = hideInInspector;
		}
	}
}