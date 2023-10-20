using CustomAttributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer( typeof( ReadOnlyAttribute ) )]
    public class ReadOnlyDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false; // Делаем поле только для чтения
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }
}
