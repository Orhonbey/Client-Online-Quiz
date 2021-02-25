using NativeWebSocket;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionWeb : MonoBehaviour
{
    public string url;

    UGWebSocket socket;

    private void Awake()
    {
        Application.runInBackground = true;
    }

    /// <summary>
    /// Server Bağlantı işlemleri gerçekleştiriliyor ve 
    /// </summary>
    public void Connect()
    {
        socket = new UGWebSocket();
        socket.OnOpen = OnConnected;
        socket.OnError = OnError;
        socket.OnClose = OnClose;

        socket.Connect(url);
    }
    public void OnMessageEvent(string eventName, Action<string> callback)
    {
        socket.OnMessageEvent(eventName, callback);
    }
    private void OnRoomJoin(string data)
    {
        Debug.Log("Data Geldi Serverdan  !!" + data);
    }

    private void OnClose(WebSocketCloseCode obj)
    {
        UGPopup.Get("notification").SetText("Bağlantı komptu ! Lütfen internetinizi kontrol ediniz .").SetTitle("Bildirim !").Open();
    }

    private void OnError(string obj)
    {
        Debug.LogError(obj);
    }

    private void OnConnected()
    {
        Debug.Log("Bağlantı Sağlandı ");
    }

    void Update()
    {
        if (socket != null)
        {
            socket.SetMessageQueue();
        }
    }

    void OnDestroy()
    {
        /// Oyuncuyu burda Çıkarıyoruz 
        /// Odadanda çıkarıyoruz .
        socket.ShotDown();
    }
    public void SendData(string key, string jsonData)
    {
        socket.Send(key, jsonData);
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
        var stringData = JsonUtility.ToJson(data);

        socket.Send("Test", stringData);
    }
}
