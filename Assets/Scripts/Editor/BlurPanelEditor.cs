using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;

#pragma warning disable 0649
[CustomEditor(typeof(BlurPanel))]
public class BlurPanelEditor : ImageEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("time"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("delay"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("step"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("animate"));

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
