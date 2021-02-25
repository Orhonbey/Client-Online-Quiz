using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishPanel : MonoBehaviour
{
    public Text myScore;
    public Text enemyScore;
    public Text myName;
    public Text enemyName;

    public Text finishMessage;

    public void SetPanel(FinishData finishData)
    {
        myName.text = GameManager.instance.playerName;
        bool myFirstClient = false;

        if (GameManager.instance.playerName.Equals(finishData.clientFirstName))
        {
            myScore.text = finishData.clientFirstScore.ToString();
            enemyScore.text = finishData.clientSecoundScore.ToString();
            enemyName.text = finishData.clientSecoundName;
            myFirstClient = true;
        }
        else
        {
            myScore.text = finishData.clientSecoundScore.ToString();
            enemyScore.text = finishData.clientFirstScore.ToString();
            enemyName.text = finishData.clientFirstName;
        }
        if (myFirstClient)
        {
            if (finishData.clientFirstScore > finishData.clientSecoundScore)
            {
                finishMessage.text = "Kazandınız !";
            }
            else
            {
                finishMessage.text = "Kaybettiniz !";
            }
        }
        else
        {
            if (finishData.clientFirstScore > finishData.clientSecoundScore)
            {
                finishMessage.text = "Kaybettiniz !";
            }
            else
            {
                finishMessage.text = "Kazandınız !";
            }
        }
    }


    public void FindGame()
    {
        GameManager.instance.findUserPanel.SetPanel(GameManager.instance.playerName);
        UGPanel.Open("FindUser");
        /// Servera connection İsteği Yollanacak .
        GameManager.instance.connection.SendData("OnFindMatch", GameManager.instance.playerName);
    }
}
