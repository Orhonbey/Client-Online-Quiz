using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindUserPanel : MonoBehaviour
{
    public Text userName;
    public Text findMatchTime;
    public Text enemyName;

    public UGUIScaleEffect enemyProfile;

    public int findUserTime = 15;

    /// <summary>
    /// Panel üzerindeki belli işlemleri set etmek için kullanılır .
    /// </summary>
    public void SetPanel(string userName)
    {
        this.userName.text = userName;
        int counter = 0;
        LeanTween.value(counter, findUserTime, findUserTime).setOnUpdate((float x) =>
        {
            findMatchTime.text = ((int)x).ToString();
        }).setOnComplete(()=> {
            enemyProfile.gameObject.LeanCancel();
            LeanTween.scale(enemyProfile.gameObject, Vector3.one, 0.1f);
        });
    }
}
