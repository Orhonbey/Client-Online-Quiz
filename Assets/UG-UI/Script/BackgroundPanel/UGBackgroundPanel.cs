using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Bu panel Altındaki Paneller Background için kullanılır 
/// Fix Paneli temel Alır .
/// </summary>
[DefaultExecutionOrder(-125)]
public class UGBackgroundPanel : MonoBehaviour
{
    #region //----> Variable
    public static UGBackgroundPanel instance;
    public Dictionary<string, UGBackgroundFix> fixPanels;
    #endregion
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        transform.SetAsFirstSibling();
        fixPanels = new Dictionary<string, UGBackgroundFix>();
        var panels = GetComponentsInChildren<UGBackgroundFix>();
        foreach (var item in panels)
        {
            fixPanels.Add(item.fixPanelId, item);
        }
    }
    /// <summary>
    /// Panel Fixlemeye yarayan Fonksiyondur aktif edilecek olan Panel Id varilier .
    /// </summary>
    /// <param name="activePanelId">aktif edilecek olan Panel Idsi verilir .</param>
    public static void Open(string activePanelId)
    {
        foreach (var item in instance.fixPanels)
        {
            item.Value.ShotDown(activePanelId);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static UGBackgroundFix Get(string id)
    {
        if (instance.fixPanels.TryGetValue(id, out var fix))
        {
            return fix;
        }
        return null;
    }
}
