using UnityEngine;
using NativeWebSocket;
using System;
using Newtonsoft.Json;

public class Connection : MonoBehaviour
{
    public string ip;
    public int port;
    UGTcpSocket socket;

    private void Awake()
    {
        Application.runInBackground = true;
    }

    /// <summary>
    /// Server Bağlantı işlemleri gerçekleştiriliyor ve 
    /// </summary>
    void Start()
    {
        socket = new UGTcpSocket();

        socket.OnConnected(OnConnected);
        socket.OnDisconnect(OnDisconnected);
        socket.OnEventAdd("OnFoundMatch", OnFoundMatch);

        socket.Connect(new NetworkConfig(ip,port));
    }
    private void OnDisconnected()
    {
        UGPopup.Get("notification").SetText("Bağlantı komptu ! Lütfen internetinizi kontrol ediniz .").SetTitle("Bildirim !").Open();
    }
    private void OnConnected()
    {
        Debug.Log("Bağlantı Sağlandı ");
    }
    private void OnFoundMatch(string json)
    {
        Debug.Log("Data Geldi : "+ json);
    }
    void Update()
    {
        socket?.Listen();
    }
    void OnDestroy()
    {
        socket?.ShotDown();
    }
    /// <summary>
    /// Test
    /// </summary>
    void SendData()
    {
        var data = new SendTestData();

        data.message = "sunal";

        data.channelId = 15;

        data.question = "Nasılsın ?";
        data.reply = "iyiyim :D";

        var stringData = JsonConvert.SerializeObject(data);


        socket.Send("Test",stringData);
    }
}

public class SendTestData
{
    public int channelId;
    public string message;
    public string question;
    public string reply;
}