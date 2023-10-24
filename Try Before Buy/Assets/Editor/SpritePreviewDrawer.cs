using CustomAttributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(SpritePreviewAttribute))]
    public class SpritePreviewDrawer : PropertyDrawer
    {
        private static GUIStyle tempStyle = new GUIStyle();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var ident = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var spriteRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            property.objectReferenceValue = EditorGUI.ObjectField(spriteRect, property.name, property.objectReferenceValue, typeof(Sprite), false);

            // Если это не режим Repaint или свойство равно null, выходим
            if (Event.current.type != EventType.Repaint || property.objectReferenceValue == null)
                return;

            // Рисуем спрайт
            Sprite sp = property.objectReferenceValue as Sprite;

            // Задаем смещение справа от position.width
            float previewWidth = 64; // Ширина превью
            spriteRect.x = position.x + position.width - previewWidth;
            spriteRect.y += EditorGUIUtility.singleLineHeight + 4;
            spriteRect.width = previewWidth;
            spriteRect.height = 64;
            tempStyle.normal.background = sp.texture;
            tempStyle.Draw(spriteRect, GUIContent.none, false, false, false, false);

            EditorGUI.indentLevel = ident;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 70f;
        }
    }
}