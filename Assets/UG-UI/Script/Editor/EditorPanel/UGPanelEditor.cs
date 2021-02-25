using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(UGPanel))]
public class UGPanelEditor : Editor
{
    string[] panelIDs;
    int panelIdIndex = 0;
    UGPanel ugPanel;
    private void OnEnable()
    {
        ugPanel = target as UGPanel;
        var panels = ugPanel.GetComponentsInChildren<UGPanelChild>();
        panelIDs = new string[panels.Length];
        for (int i = 0; i < panels.Length; i++)
        {
            panelIDs[i] = panels[i].Id;
            if (panels[i].Id.Equals(ugPanel.startPanel))
            {
                panelIdIndex = i;
            }
        }
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("UG UI Panel Inspector Hoş Geldiniz !", MessageType.None,true);
        EditorGUILayout.Space();
        panelIdIndex = EditorGUILayout.Popup("Start Panel ID :",panelIdIndex, panelIDs);
        if (panelIDs.Length > 0)
        {
            ugPanel.startPanel = panelIDs[panelIdIndex];
        }
        EditorUtility.SetDirty(target);
    }
}
