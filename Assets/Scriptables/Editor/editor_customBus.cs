using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(audio_sc_settings))]
public class editor_customBus : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUILayout.LabelField("Volume Settings");
        SerializedProperty volSFX = property.FindPropertyRelative("vol");
        SerializedProperty nameBus = property.FindPropertyRelative("name");
    }
}
