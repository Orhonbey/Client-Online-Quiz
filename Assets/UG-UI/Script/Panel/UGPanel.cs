using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UGPanel is the structure that manages and enables transactions on them.
/// </summary>
[DefaultExecutionOrder(-150)]
public class UGPanel : MonoBehaviour
{
    #region //----> Variable
    public static UGPanel instance;
    /// <summary>
    /// Oluşturulan ve Yönetilecek olan Panelleri Tutacak Olan Dictinary
    /// </summary>
    public Dictionary<string, UGPanelChild> panelList = new Dictionary<string, UGPanelChild>();
    /// <summary>
    /// aktif Panel Barındırıyoruz 
    /// </summary>
    public UGPanelChild activePanel;
    [Header("Raks Game UI")]
    public string startPanel;
    bool isLoading;
    #endregion
    #region //----> Method
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        GetChild();
    }
    private void GetChild()
    {
        var childs = GetComponentsInChildren<UGPanelChild>();
        panelList.Clear();
        foreach (var item in childs)
        {
            panelList.Add(item.Id, item);
            if (!startPanel.Equals(item.Id))
                item.ShotDown();
            else
                item.Open();
        }
    }
    /// <summary>
    /// Open the panel with ID .
    /// active panel close.
    /// </summary>
    /// <param name="panelId"></param>
    /// <returns></returns>
    public static UGPanelChild Open(string panelId)
    {
        if (instance.activePanel != null && !instance.activePanel.Id.Equals(panelId))
        {
            instance.activePanel.Close();
        }
        if (instance.panelList.TryGetValue(panelId, out var panel))
        {
            panel.Open();

            return panel;
        }
        return null;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="panelId"></param>
    /// <returns></returns>
    public static UGPanelChild Close(string panelId)
    {
        if (instance.activePanel == null)
        {
            Debug.LogError("Active Panel Not found");
            return null;
        }
        if (instance.activePanel.Id.Equals(panelId))
        {
            instance.activePanel.Close();
            return instance.activePanel;
        }
        return null;
    }
    /// <summary>
    /// Panel return with Id 
    /// </summary>
    /// <param name="panelId">Panel Id</param>
    /// <returns></returns>
    public static UGPanelChild Get(string panelId)
    {
        if (instance.panelList.TryGetValue(panelId, out var panel))
        {
            return panel;
        }
        return null;
    }
    #endregion
}
