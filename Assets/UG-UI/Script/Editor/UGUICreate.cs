using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// UG Game UI Sistemi
/// Sunal Orhon Tarafından Yapılmıştır .
/// 
/// </summary>
public class UGUICreate : Editor
{
    [MenuItem("Unito Games/Create/UI/Canvas", false, 1)]
    [MenuItem("GameObject/Unito Games/Create/UI/Canvas", false, 0)]
    public static void CreateCanvas()
    {
        //----> Canvas Olşturuldu 
        GameObject canvas = new GameObject();
        canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<UGCanvas>();
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();
        var ugCanvas = canvas.GetComponent<Canvas>();
        canvas.name = "UG Canvas";
        var rCScaler = canvas.GetComponent<CanvasScaler>();
        rCScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        rCScaler.referenceResolution = new Vector2(1920, 1080);
        //----> UG Panel Create
        GameObject ugPanel = new GameObject();
        ugPanel.AddComponent<UGPanel>();
        var ugPRect = ugPanel.AddComponent<RectTransform>();
        ugPRect.sizeDelta = ugCanvas.pixelRect.size;
        ugPRect.anchorMax = Vector2.one;
        ugPRect.anchorMin = Vector2.zero;
        ugPRect.anchoredPosition = ugCanvas.GetComponent<RectTransform>().anchoredPosition;
        ugPanel.name = "UGPanel";
        ugPanel.transform.SetParent(canvas.transform);
        //---->Fix Panel
        GameObject ugFix = new GameObject();
        ugFix.name = "UGFixPanel";
        ugFix.AddComponent<UGFixPanel>();
        var ugFRect = ugFix.AddComponent<RectTransform>();
        ugFRect.sizeDelta = ugCanvas.pixelRect.size;
        ugFRect.anchorMax = Vector2.one;
        ugFRect.anchorMin = Vector2.zero;
        ugFRect.anchoredPosition = ugCanvas.GetComponent<RectTransform>().anchoredPosition;
        ugFix.transform.SetParent(canvas.transform);
        //----> Background
        GameObject ugBackgroundFix = new GameObject();
        ugBackgroundFix.name = "UGBackgroundPanel";
        ugBackgroundFix.AddComponent<UGBackgroundPanel>();
        var ugBackgroundRect = ugBackgroundFix.AddComponent<RectTransform>();
        ugBackgroundRect.sizeDelta = ugCanvas.pixelRect.size;
        ugBackgroundRect.anchorMax = Vector2.one;
        ugBackgroundRect.anchorMin = Vector2.zero;
        ugBackgroundRect.anchoredPosition = ugCanvas.GetComponent<RectTransform>().anchoredPosition;
        ugBackgroundFix.transform.SetParent(canvas.transform);
        //----> Popup
        GameObject ugPopup = new GameObject();
        ugPopup.AddComponent<UGPopup>();
        var ugPoRect = ugPopup.AddComponent<RectTransform>();
        ugPoRect.sizeDelta = ugCanvas.pixelRect.size;
        ugPoRect.anchorMax = Vector2.one;
        ugPoRect.anchorMin = Vector2.zero;
        ugPoRect.anchoredPosition = ugCanvas.GetComponent<RectTransform>().anchoredPosition;
        ugPopup.name = "UGPopup";
        ugPopup.transform.SetParent(canvas.transform);
        //----> Waiting
        GameObject waiting = new GameObject();
        waiting.AddComponent<UGWaiting>();
        var waitRect = waiting.AddComponent<RectTransform>();
        waitRect.sizeDelta = ugCanvas.pixelRect.size;
        waitRect.anchorMax = Vector2.one;
        waitRect.anchorMin = Vector2.zero;
        waitRect.anchoredPosition = ugCanvas.GetComponent<RectTransform>().anchoredPosition;
        waiting.name = "UGWaiting";
        waiting.transform.SetParent(canvas.transform);
    }
    [MenuItem("Unito Games/Create/UI/UGPanel", false, 1)]
    [MenuItem("GameObject/Unito Games/Create/UI/UGPanel", false, 0)]
    public static void CreatePanel()
    {
        var c = FindObjectOfType<UGPanel>();
        if (c == null)
        {
            Debug.Log("Lütfen ilk olarak UG Canvas Oluşturunuz !!");
            return;
        }
        GameObject ugPanel = new GameObject("UGPanel-Panel");
        ugPanel.transform.SetParent(c.transform);
        ugPanel.AddComponent<UGPanelChild>();
        var ugPRect = ugPanel.AddComponent<RectTransform>();
        ugPRect.localScale = Vector3.one;
        ugPRect.localPosition = Vector3.zero;
    }
    [MenuItem("Unito Games/Create/UI/UGFixPanel", false,1)]
    [MenuItem("GameObject/Unito Games/Create/UI/UGFixPanel", false,0)]
    public static void CreateFixPanel()
    {
        var c = FindObjectOfType<UGFixPanel>();
        if (c == null)
        {
            Debug.Log("Lütfen ilk olarak UG Canvas Oluşturunuz !!");
            return;
        }
        GameObject ugFixPanel = new GameObject("UGFixPanel-Fix");
        ugFixPanel.transform.SetParent(c.transform);
        ugFixPanel.AddComponent<UGFix>();
        var ugRect = ugFixPanel.AddComponent<RectTransform>();
        ugRect.localScale = Vector3.one;
        ugRect.localPosition = Vector3.zero;
    }
    [MenuItem("Unito Games/Create/UI/UGPopup", false, 1)]
    [MenuItem("GameObject/Unito Games/Create/UI/UGPopup", false, 0)]
    public static void CreateUGPopup()
    {
        var c = FindObjectOfType<UGPopup>();
        if (c == null)
        {
            Debug.Log("Lütfen ilk olarak UG Canvas Oluşturunuz !!");
            return;
        }
        GameObject ugFixPanel = new GameObject("UGPopup-Popup");
        ugFixPanel.transform.SetParent(c.transform);
        ugFixPanel.AddComponent<UGPopupChild>();
        var ugRect = ugFixPanel.AddComponent<RectTransform>();
        ugRect.localScale = Vector3.one;
        ugRect.localPosition = Vector3.zero;
    }

    [MenuItem("Unito Games/Create/UI/UGBackgroundFix", false, 1)]
    [MenuItem("GameObject/Unito Games/Create/UI/UGBackgroundFix", false, 0)]
    public static void CreateBackgroundFix()
    {
        var c = FindObjectOfType<UGBackgroundPanel>();
        if (c == null)
        {
            Debug.Log("Lütfen ilk olarak UG Canvas Oluşturunuz !!");
            return;
        }
        GameObject ugFix = new GameObject("UGBackground-Fix");
        ugFix.transform.SetParent(c.transform);
        ugFix.AddComponent<UGBackgroundFix>();
        var ugRect = ugFix.AddComponent<RectTransform>();
        ugRect.localScale = Vector3.one;
        ugRect.localPosition = Vector3.zero;
        ugFix.transform.SetAsFirstSibling();
    }

}
