using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePanel : MonoBehaviour
{
    public InputField userName;
    /// <summary>
    ///  Oyun Arama Buttonu 
    /// </summary>
    public void FindGame()
    {
        if (userName.text.Length > 0)
        {
            GameManager.instance.playerName = userName.text;
            GameManager.instance.findUserPanel.SetPanel(userName.text);
            UGPanel.Open("FindUser");
            /// Servera connection İsteği Yollanacak .
            GameManager.instance.connection.SendData("OnFindMatch", GameManager.instance.playerName);

        }
        else
        {
            UGPopup.Get("notification").SetText("Lütfen isim Giriniz !").SetTitle("Bildirim !").Open();
        }
    }
}
