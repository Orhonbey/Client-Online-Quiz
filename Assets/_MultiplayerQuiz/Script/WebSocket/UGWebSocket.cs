using NativeWebSocket;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UGTcpSocket Sunal orhon tarafından basit bir standart oluşturmak için Case için oluşturulmuştur .
/// </summary>
public class UGWebSocket
{
    WebSocket socket;
    string _url;
    public string GetUrl { get { return _url; } }

    Action onOpen;
    /// <summary>
    /// Server Bağlantı sağlandığında tetiklenecek callback'ğin atamasının yapıldığı yer
    /// </summary>
    public Action OnOpen
    {
        get { return onOpen; }
        set { onOpen += value; }
    }
    Action<string> onError;
    /// <summary>
    /// bağlantı üzerinde hataların Döndürülen callback methodları 
    /// </summary>
    public Action<string> OnError
    {
        get { return onError; }
        set { onError += value; }
    }
    Action<WebSocketCloseCode> onClose;
    /// <summary>
    /// bağlantı üzerinde hataların Döndürülen callback methodları 
    /// </summary>
    public Action<WebSocketCloseCode> OnClose
    {
        get { return onClose; }
        set { onClose += value; }
    }
    /// <summary>
    /// serverdan gelen eventler Oluyor .
    /// </summary>
    Dictionary<string, Action<string>> onMessageEvent = new Dictionary<string, Action<string>>();
    public void OnMessageEvent(string key, Action<string> jsonValue)
    {
        onMessageEvent.Add(key, jsonValue);
    }
    /// <summary>
    /// Server Bağlantının Denendiği Yer
    /// (Eventler atandıktan sonra tetiklenmelidir .)
    /// </summary>
    /// <param name="url">Bağlantı yapılacak olan Adress Url server adres</param>
    public void Connect(string url)
    {
        _url = url;
        ServerConnect(url);
    }
    /// <summary>
    /// Server Bağlantının yapıldığı yer .
    /// </summary>
    async void ServerConnect(string url)
    {
        socket = new WebSocket(url);
        socket.OnOpen += OnOpenCallback;
        socket.OnMessage += OnMessageCallback;
        socket.OnError += OnErrorCallback;
        socket.OnClose += OnCloseCallback;

        await socket.Connect();
    }
    private void OnCloseCallback(WebSocketCloseCode closeCode)
    {
        onClose?.Invoke(closeCode);
    }
    private void OnErrorCallback(string errorMsg)
    {
        onError?.Invoke(errorMsg);
    }
    /// <summary>
    /// Serverdan kullanıcıya gelen datanın işlendiği yer
    /// </summary>
    /// <param name="data"></param>
    private void OnMessageCallback(byte[] data)
    {
        var jsonData = System.Text.Encoding.UTF8.GetString(data);

        var comingData = JsonUtility.FromJson<EventKeyValue>(jsonData);

        if (onMessageEvent.TryGetValue(comingData.key, out var getAction))
        {
            getAction(comingData.value);
        }
        else
        {
            Debug.LogError("Event not found !");
        }
    }
    /// <summary>
    /// Kullanıcı bağlantı sağlandığında servera bağlandı bilgisini aldığı yer 
    /// </summary>
    private void OnOpenCallback()
    {
        onOpen?.Invoke();
    }
    /// <summary>
    ///  Update İçerisinde bulunması gerekiyor 
    ///  gelen dataların fark edilmesi için.
    /// </summary>
    public void SetMessageQueue()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        socket?.DispatchMessageQueue();
#endif
    }
    /// <summary>
    /// Socket bağlantısını sonladnırıyoruz .
    /// </summary>
    public void ShotDown()
    {
        Close();
    }
    async void Close()
    {
        await socket.Close();
    }
    /// <summary>
    /// Serverdaki eventi belirtilen kanala json veri yolluyoruz .
    /// </summary>
    /// <param name="eventKey">serverda açılan eventın keyi</param>
    /// <param name="jsonData">serverdaki evente gönderilen json data</param>
    public void Send(string eventKey, string jsonData)
    {
        EventKeyValue newSendData = new EventKeyValue(eventKey, jsonData);
        var json = JsonUtility.ToJson(newSendData);
        SendData(json);
    }
    async void SendData(string value)
    {
        await socket.SendText(value);
    }
  
}

/// <summary>
/// Alınacak ve Gönderilecek Olan Data tipi 
/// </summary>
[Serializable]
public class EventKeyValue
{
    public EventKeyValue(string key, string value)
    {
        this.key = key;
        this.value = value;
    }
    public string key;
    public string value;
}