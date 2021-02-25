using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor Yazılacak .
/// </summary>
public class UGPanelChild : UGMaster
{
    #region //----> Variable
    [Tooltip("Geriye gidelecek olan yapının Id si")]
    public string backId;
    #endregion
    public override void Open(float closeTime = 0)
    {
        base.Open(closeTime);
        UGPanel.instance.activePanel = this;
    }
    public override void AnimationBegin()
    {
        base.AnimationBegin();
        UGFixPanel.FixOpen(Id);
        UGBackgroundPanel.Open(Id);
    }
}
