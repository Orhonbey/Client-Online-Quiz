using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bu Yapı Bir Panel Yapısı Sayıla bilir.
/// Hangi panelde Bu Obje Aktif olacak Onu Belirten bir yapıdır .
/// Buna Fix Diyoruz .
/// </summary>
[RequireComponent(typeof( CanvasGroup))]
public class UGFix : MonoBehaviour
{
    #region //----> Variable
    public bool isAllPanelOpen;
    public string fixPanelId;
    public List<string> panelIds = new List<string>();
    public CanvasGroup myCanvasGroup;
    public bool isActive;
    #endregion
    #region //----> My Method
    private void Awake()
    {
        myCanvasGroup = GetComponent<CanvasGroup>();
    }
    /// <summary>
    /// Fix UI Verilen Id ile Açıkmı kalacak yoksa kapalımı kalacak onu buluyoruz .
    /// </summary>
    /// <param name="panelId"></param>
    /// <returns></returns>
    public bool ShotDown(string panelId)
    {
        if (isAllPanelOpen)
        {
            if (!isActive)
                Open();
            return true;
        }
        if (panelIds.Contains(panelId))
        {
            Open();
            return true;
        }
        else
        {
            Close();
        }
        return false;
    }
    public void Close()
    {
        myCanvasGroup.blocksRaycasts = false;
        myCanvasGroup.interactable = false;
        myCanvasGroup.LeanAlpha(0, 0);
        isActive = false;
    }
    public void Open()
    {
        myCanvasGroup.LeanAlpha(1, 0);
        myCanvasGroup.interactable = true;
        myCanvasGroup.blocksRaycasts = true;
        isActive = true;
    }
    #endregion
}
