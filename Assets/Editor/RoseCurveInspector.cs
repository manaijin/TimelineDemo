using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(RoseCurve))]
public class RoseCurveInspector : ImageEditor
{
    SerializedProperty m_SplitNum;
    SerializedProperty m_n;

    protected override void OnEnable()
    {
        m_SplitNum = serializedObject.FindProperty("splitNum");
        m_n = serializedObject.FindProperty("n");
        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_SplitNum);
        EditorGUILayout.PropertyField(m_n);
        serializedObject.ApplyModifiedProperties();
        base.OnInspectorGUI();
    }
}
