using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Oda Hakkında bütün işlemlerin oluşturulduğu yer oluyor .
/// </summary>
public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    [HideInInspector]
    public ConnectionWeb connection;
    public RoomData currectRoomData;
    public QuestionReply reply;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    public void SetManager()
    {
        connection = GameManager.instance.connection;
        connection.OnMessageEvent("OnRoomJoin", OnRoomJoin);
        connection.OnMessageEvent("OnRoomEnemyReply", OnRoomEnemyReply);
        connection.OnMessageEvent("OnQuestionFinish", OnQuestionFinish);
        connection.OnMessageEvent("OnNextQuestion", OnNextQuestion);
        connection.OnMessageEvent("OnFinish", OnFinish);
        connection.OnMessageEvent("OnEnemyExit", OnEnemyExit);
    }

    private void OnEnemyExit(string data)
    {
        UGPopup.Get("notification").SetText("Rakibiniz Kaçtı :( Üzgünüz ! Yeni oyun araya bilirsiniz.")
            .SetTitle("Bildirim").Open();
    }

    /// <summary>
    /// Oyun bitince çıkacak ekran veya Oyuncu çıkınca gösterilir .
    /// </summary>
    /// <param name="obj"></param>
    private void OnFinish(string data)
    {
        var finishData = JsonUtility.FromJson<FinishData>(data);
        currectRoomData = null;
        GameManager.instance.finishPanel.SetPanel(finishData);
        UGPanel.Open("Finish");
    }

    /// <summary>
    /// Next Level 
    /// </summary>
    /// <param name="data"></param>
    private void OnNextQuestion(string data)
    {
        currectRoomData = JsonUtility.FromJson<RoomData>(data);
        Debug.Log("Data ! Geldi" + currectRoomData);
        /// Play panele set yapıcağız 
        GameManager.instance.playPanel.SetPanel(currectRoomData);
        UGPanel.Open("Play");
    }

    /// <summary>
    /// Cevaplama bittikten sonra Doğru şık gösterme ve Puan atama 
    /// </summary>
    /// <param name="data"></param>
    private void OnQuestionFinish(string data)
    {
        GameManager.instance.playPanel.EnemyButtonSelect(reply);
        /// Doğru şık gösterilecek ve Puanalr yazıacak 
        var questionFinishData = JsonUtility.FromJson<QuestionFinishData>(data);
        LeanTween.cancel(GameManager.instance.playPanel.timerId);
        GameManager.instance.playPanel.QuestionCorrectOptions(questionFinishData);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    private void OnRoomEnemyReply(string data)
    {
        reply = JsonUtility.FromJson<QuestionReply>(data);
        currectRoomData.isFirstReply = true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    private void OnRoomJoin(string data)
    {
        Debug.Log("data : " + data);
        currectRoomData = JsonUtility.FromJson<RoomData>(data);
        Debug.Log(currectRoomData.id + " : " + currectRoomData.questionClient.questionText);
        /// Play panele set yapıcağız 
        GameManager.instance.playPanel.SetPanel(currectRoomData);
        UGPanel.Open("Play");
    }
}
[Serializable]
public class RoomData
{
    public int id;
    public Question questionClient;
    public string clientFirstName;
    public string clientSecondName;
    /// <summary>
    /// False Olursa daha önce cevap verilmemiş oluyor .
    /// </summary>
    public bool isFirstReply;
    public int clientFirstScore;
    public int clientSecondScore;
}
[Serializable]
public class Question
{
    public string _id;
    public int id;
    public string questionText;
    public Option[] options;
}
[Serializable]
public class Option
{
    public string id;
    public string questionId;
    public string option;
    public string optionId;
}
[Serializable]
public class QuestionReply
{
    public string optionId;
    public int roomId;
    public bool firstReply;
}
[Serializable]
public class QuestionFinishData
{
    public string reply;
    public int clientFirstScore;
    public int clientSecondScore;
    public string clientFirstName;
}

public class FinishData
{
    public string clientFirstName;
    public int clientFirstScore;
    public string clientSecoundName;
    public int clientSecoundScore;
}