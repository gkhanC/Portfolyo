using UnityEditor;
using UnityEngine;

namespace HypeFire.Library.Structures.Editor
{
#if UNITY_EDITOR

    [CustomPropertyDrawer((typeof(RotationField)))]
    public class RotationFieldEditor : PropertyDrawer
    {
        SerializedProperty rotationField;


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var percent = position.width / 550;
            var with = 330f * percent;

            var unitRect = new Rect(position.x, position.y, position.width - with, position.height);
            var nameRect = new Rect(position.x + (position.width - with), position.y, with,
                position.height);


            EditorGUI.LabelField(unitRect, property.name);
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative(nameof(RotationField.euler)),
                GUIContent.none);


            EditorGUI.EndProperty();
        }
    }

#endif
}