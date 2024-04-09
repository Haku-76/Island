using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DrinksDataSO))]
public class DrinksDataDrawer : PropertyDrawer
{
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Draw the enum field
        EditorGUI.PropertyField(position, property.FindPropertyRelative("drinksType"), label);

        // Draw additional properties based on the enum value
        position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        SerializedProperty type = property.FindPropertyRelative("drinksType");
        switch ((DrinksType)type.enumValueIndex)
        {
            case DrinksType.wine:
                EditorGUI.PropertyField(position, property.FindPropertyRelative("alcohol"), new GUIContent("alcohol Value"));
                break;
            case DrinksType.water:
                EditorGUI.PropertyField(position, property.FindPropertyRelative("taste"), new GUIContent("taste"));
                break;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Calculate the height based on the enum value
        float baseHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        SerializedProperty type = property.FindPropertyRelative("drinksType");
        switch ((DrinksType)type.enumValueIndex)
        {
            case DrinksType.wine:
            case DrinksType.water:
                return baseHeight * 2; // Enum field + additional field
            default:
                return baseHeight;
        }
    }
}
