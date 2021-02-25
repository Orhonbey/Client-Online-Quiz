using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UGPopupChild : UGMaster
{
    #region //----> Variable
    UGPopupChild m;
    public RUISetText popupTitle;
    public RUISetText popupText;
    public Action<CallbackResult> callback;
    /// <summary>
    /// Popup Açılmaya alteernatif Olarak kullanılır 
    /// </summary>
    public UGPopupChild OnOpen
    {
        get
        {
            Open();
            return this;
        }
    }
    #endregion
    #region //----> Method
    private void Awake()
    {
        m = this;
        myCanvasGroup = GetComponent<CanvasGroup>();
        myRectTransform = GetComponent<RectTransform>();
        _GetText();
    }
    private void _GetText()
    {
        //var popupTexts = transform.GetComponentsInChildren<RUISetText>();
        //foreach (var item in popupTexts)
        //{
        //    switch (item.textType)
        //    {
        //        case RUITextType.title:
        //            popupTitle = item;
        //            break;
        //        case RUITextType.text:
        //            popupText = item;
        //            break;
        //    }
        //}
    }
    /// <summary>
    /// Başlığı Değiştirmeye yarar
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    public UGPopupChild SetTitle(string title)
    {
        popupTitle.SetText(title);
        return m;
    }
    /// <summary>
    /// Açılır Menudeki Metini Değiştirmeye yarar.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public UGPopupChild SetText(string text)
    {
        popupText.SetText(text);
        return m;
    }
    public void CallbackInvoke(int result)
    {
        var resultEnum = (CallbackResult)result;
        callback.Invoke(resultEnum);
        callback = null;
        Close();
    }
    public UGPopupChild AddOnEndCallback(UnityAction callback)
    {
        dynamicOnEnd.AddListener(callback);
        return m;
    }
    public UGPopupChild AddCallback(Action<CallbackResult> callback)
    {
        this.callback += callback;
        return m;
    }
    #endregion
    public enum CallbackResult
    {
        no = 0,
        yes = 1
    }
}