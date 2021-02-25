using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UGPopup altındaki RUIPopupChild yöneten ekrana ufak bildirimleri ve hataları bamamız için kullana bileceğimiz yapılardır .
/// </summary>
/// 
[DefaultExecutionOrder(-135)]
public class UGPopup : MonoBehaviour
{
    #region //----> Variable
    UGPopup m;
    public static Dictionary<string, UGPopupChild> popupList = new Dictionary<string, UGPopupChild>();
    public static UGPopupChild activePopup;
    #endregion
    #region //----> Method
    private void Awake()
    {
        m = this;
    }
    private void Start()
    {
        GetChild();
    }
    private void GetChild()
    {
        var childs = GetComponentsInChildren<UGPopupChild>();
        popupList.Clear();
        foreach (var item in childs)
        {
            item.ShotDown();
            popupList.Add(item.Id, item);
        }
    }
    /// <summary>
    /// Popup Döndüren yapı
    /// </summary>
    /// <param name="panelId">Popup Id'si</param>
    /// <returns></returns>
    public static UGPopupChild Get(string panelId)
    {
        if (popupList.TryGetValue(panelId,out var value))
        {
            return value;
        }
        return null;
    }
    #endregion
}
