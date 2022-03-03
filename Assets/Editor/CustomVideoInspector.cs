using CustomTimeline;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomVideoPlayableAsset))]
public class CustomVideoInspector : Editor
{
    SerializedProperty targetProp;
    SerializedProperty videoClipProp;
    SerializedProperty startTimeProp;
    SerializedProperty playSpeedProp;
    SerializedProperty masksProp;
    SerializedProperty cycleProp;

    private GUILayoutOption[] options;

    private void OnEnable()
    {
        targetProp = serializedObject.FindProperty("target");
        videoClipProp = serializedObject.FindProperty("videoClip");
        startTimeProp = serializedObject.FindProperty("startTime");
        playSpeedProp = serializedObject.FindProperty("playSpeed");
        masksProp = serializedObject.FindProperty("masks");
        cycleProp = serializedObject.FindProperty("cycle");
        options = new GUILayoutOption[2] { GUILayout.Width(100), GUILayout.Height(5) };
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawProperty(targetProp);
        DrawProperty(videoClipProp);
        DrawProperty(startTimeProp);
        DrawProperty(playSpeedProp);

        switch (targetProp.enumValueIndex)
        {
            case (int)e_VideoOutputType.Single:

                break;

            case (int)e_VideoOutputType.DoubleBlend:
                DrawProperty(cycleProp);
                break;

            case (int)e_VideoOutputType.DoubleMask:
                DrawProperty(masksProp);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawProperty(SerializedProperty p)
    {
        EditorGUILayout.LabelField("", options);
        EditorGUILayout.PropertyField(p);
    }
}

