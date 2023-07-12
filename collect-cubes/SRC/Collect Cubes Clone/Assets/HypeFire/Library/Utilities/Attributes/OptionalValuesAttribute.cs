#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using UnityEngine;

namespace HypeFire.Library.Utilities.Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public class OptionalValuesAttribute : PropertyAttribute
	{
		public float[] FloatOptions { get; private set; }
		public int[] IntOptions { get; private set; }

		public OptionalValuesAttribute(params float[] options)
		{
			FloatOptions = options;
		}

		public OptionalValuesAttribute(params int[] options)
		{
			IntOptions = options;
		}
	}

#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(OptionalValuesAttribute))]
	public class OptionalValuesDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			OptionalValuesAttribute optionalValuesAttribute = attribute as OptionalValuesAttribute;
			float[] floatOptions = optionalValuesAttribute.FloatOptions;
			int[] intOptions = optionalValuesAttribute.IntOptions;

			EditorGUI.BeginProperty(position, label, property);

			if (property.propertyType == SerializedPropertyType.Float)
			{
				if (floatOptions != null && floatOptions.Length > 0)
				{
					string[] floatOptionLabels = GetFloatOptionLabels(floatOptions);
					int selectedIndex =
						GetSelectedFloatOptionIndex(property.floatValue, floatOptions);
					int newSelectedIndex = EditorGUI.Popup(position, label.text, selectedIndex,
						floatOptionLabels);
					property.floatValue = floatOptions[newSelectedIndex];
				}
				else
				{
					EditorGUI.LabelField(position, label.text,
						"OptionalValuesAttribute is not configured correctly for float fields.");
				}
			}
			else if (property.propertyType == SerializedPropertyType.Integer)
			{
				if (intOptions != null && intOptions.Length > 0)
				{
					string[] intOptionLabels = GetIntOptionLabels(intOptions);
					int selectedIndex = GetSelectedIntOptionIndex(property.intValue, intOptions);
					int newSelectedIndex = EditorGUI.Popup(position, label.text, selectedIndex,
						intOptionLabels);
					property.intValue = intOptions[newSelectedIndex];
				}
				else
				{
					EditorGUI.LabelField(position, label.text,
						"OptionalValuesAttribute is not configured correctly for int fields.");
				}
			}
			else
			{
				EditorGUI.LabelField(position, label.text,
					"OptionalValuesAttribute can only be applied to float or int fields.");
			}

			EditorGUI.EndProperty();
		}

		private string[] GetFloatOptionLabels(float[] options)
		{
			string[] optionLabels = new string[options.Length];
			for (int i = 0; i < options.Length; i++)
			{
				optionLabels[i] = options[i].ToString();
			}

			return optionLabels;
		}

		private string[] GetIntOptionLabels(int[] options)
		{
			string[] optionLabels = new string[options.Length];
			for (int i = 0; i < options.Length; i++)
			{
				optionLabels[i] = options[i].ToString();
			}

			return optionLabels;
		}

		private int GetSelectedFloatOptionIndex(float value, float[] options)
		{
			for (int i = 0; i < options.Length; i++)
			{
				if (Mathf.Approximately(value, options[i]))
				{
					return i;
				}
			}

			return 0;
		}

		private int GetSelectedIntOptionIndex(int value, int[] options)
		{
			for (int i = 0; i < options.Length; i++)
			{
				if (value == options[i])
				{
					return i;
				}
			}

			return 0;
		}
	}
#endif
}