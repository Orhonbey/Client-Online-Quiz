using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FixPanel Fix panelleri Yöneten Kullanıcı Tarafından Tetkilenmesi gerekmeyen bir yapı
/// </summary>
[DefaultExecutionOrder(-145)]
public class UGFixPanel : MonoBehaviour
{
    #region //----> Variable
    public static UGFixPanel instance;
    public Dictionary<string, UGFix> fixPanels;
    #endregion
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        fixPanels = new Dictionary<string, UGFix>();
        var panels = GetComponentsInChildren<UGFix>();
        foreach (var item in panels)
        {
            fixPanels.Add(item.fixPanelId, item);
        }
    }
    /// <summary>
    /// Panel Fixlemeye yarayan Fonksiyondur aktif edilecek olan Panel Id varilier .
    /// </summary>
    /// <param name="activePanelId">aktif edilecek olan Panel Idsi verilir .</param>
    public static void FixOpen(string activePanelId)
    {
        foreach (var item in instance.fixPanels)
        {
            item.Value.ShotDown(activePanelId);
        }
    }
    public static UGFix Get(string id)
    {
        if (instance.fixPanels.TryGetValue(id,out var fix))
        {
            return fix;
        }
        return null;
    }
}
