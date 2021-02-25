using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UGPanelChild))]
public class UGPanelChildEditor : Editor
{
    UGPanelChild m;
    UGCanvas mainCanvas;
    bool isFirst;

    private void OnEnable()
    {
        m = target as UGPanelChild;
        mainCanvas = m.transform.root.GetComponent<UGCanvas>();
        isFirst = false;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying || !Application.isEditor)
        {
            return;
        }
        if (mainCanvas.isEditMode && !isFirst)
        {
            isFirst = true;
            var panels = FindObjectsOfType<UGMaster>();
            foreach (var item in panels)
            {
                if (!item.Id.Equals(m.Id))
                {
                    item.GetComponent<CanvasGroup>().alpha = 0;
                }
            }
        }
        m.GetComponent<CanvasGroup>().alpha = 1;
    }
}

