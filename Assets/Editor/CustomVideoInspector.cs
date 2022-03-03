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
    SerializedProperty startFrameProp;
    SerializedProperty playSpeedProp;
    SerializedProperty uvRectsProp;
    SerializedProperty masksProp;

    private GUILayoutOption[] options;

    private void OnEnable()
    {
        targetProp = serializedObject.FindProperty("target");
        videoClipProp = serializedObject.FindProperty("videoClip");
        startFrameProp = serializedObject.FindProperty("startFrame");
        playSpeedProp = serializedObject.FindProperty("playSpeed");
        uvRectsProp = serializedObject.FindProperty("uvRects");
        masksProp = serializedObject.FindProperty("masks");
        options = new GUILayoutOption[2] { GUILayout.Width(100), GUILayout.Height(5) };
    }
    public override void OnInspectorGUI()
    {
        DrawProperty(targetProp);
        DrawProperty(videoClipProp);
        DrawProperty(startFrameProp);
        DrawProperty(playSpeedProp);

        switch (targetProp.enumValueIndex)
        {
            case (int)e_VideoOutputType.Single:

                break;

            case (int)e_VideoOutputType.DoubleBlend:

                break;

            case (int)e_VideoOutputType.DoubleMask:
                DrawProperty(uvRectsProp);
                DrawProperty(masksProp);
                break;
        }
    }

    private void DrawProperty(SerializedProperty p)
    {
        EditorGUILayout.LabelField("", options);
        EditorGUILayout.PropertyField(p);
    }
}

