using CustomTimeline;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CustomVideoPlayableAsset))]
public class CustomVideoDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int fieldCount = 3;
        return fieldCount * EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Debug.Log("1111111");
        SerializedProperty targetProp = property.FindPropertyRelative("target");
        SerializedProperty videoClipProp = property.FindPropertyRelative("videoClip");
        SerializedProperty startFrameProp = property.FindPropertyRelative("startFrame");
        SerializedProperty playSpeedProp = property.FindPropertyRelative("playSpeed");
        SerializedProperty uvRectsProp = property.FindPropertyRelative("uvRects");
        SerializedProperty masksProp = property.FindPropertyRelative("masks");

        Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(singleFieldRect, videoClipProp);
        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, startFrameProp);
        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, playSpeedProp);

        switch (targetProp.floatValue)
        {
            case (int)e_VideoOutputType.Single:

                break;

            case (int)e_VideoOutputType.DoubleBlend:

                break;

            case (int)e_VideoOutputType.DoubleMask:
                singleFieldRect.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(singleFieldRect, uvRectsProp);
                singleFieldRect.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(singleFieldRect, masksProp);
                break;
        }
    }
}
