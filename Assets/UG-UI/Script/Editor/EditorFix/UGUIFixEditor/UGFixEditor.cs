using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
[CustomEditor(typeof(UGFix))]
public class UGFixEditor : Editor
{
    UGFix ugFix;
    ReorderableList panelIds;
    SerializedProperty fixPanelId;
    SerializedProperty isAllPanelOpen;
    string[] ids;
    public void OnEnable()
    {
        ugFix = target as UGFix;
        fixPanelId = serializedObject.FindProperty("fixPanelId");
        isAllPanelOpen = serializedObject.FindProperty("isAllPanelOpen");
        panelIds = new ReorderableList(serializedObject, serializedObject.FindProperty("panelIds"));
        panelIds.drawHeaderCallback = rect =>
        {
            EditorGUI.LabelField(rect, "Active Panel Id", " : UGUI System");
        };
        panelIds.drawElementCallback = (Rect rect, int index, bool isActive, bool focus) =>
        {
            var element = panelIds.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(new Rect(rect), element);
        };
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(fixPanelId, true);
        EditorGUILayout.PropertyField(isAllPanelOpen, true);
        EditorGUILayout.Space();
        if (!ugFix.isAllPanelOpen)
        {
            panelIds.DoLayoutList();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
