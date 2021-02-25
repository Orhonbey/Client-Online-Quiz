using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Telepathy;
using UnityEngine;
using EventType = Telepathy.EventType;
/// <summary>
/// UGTcpSocket Sunal orhon tarafından basit bir standart oluşturmak için Case için oluşturulmuştur .
/// </summary>
public class UGTcpSocket
{
    private Client _socketClient;

    NetworkConfig _networkConfig;
    public int GetPort { get { return _networkConfig.port; } }
    public string GetIp { get { return _networkConfig.ip; } }

    private Action onConnected;
    private Action onDisconnected;
    Dictionary<string, Action<string>> onEventAdd = new Dictionary<string, Action<string>>();
    /// <summary>
    /// İlk bağlantı sağlandığında Tetiklenen method
    /// </summary>
    public void OnConnected(Action onConnected) => this.onConnected = onConnected;
    /// <summary>
    /// server ile olan bağlantı koptuğu zaman tetiklenene method
    /// </summary>
    public virtual void OnDisconnect(Action onDisconnected) => this.onDisconnected = onDisconnected;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="eventAction"></param>
    public virtual void OnEventAdd(string eventName, Action<string> eventAction) => onEventAdd.Add(eventName, eventAction);

    /// <summary>
    /// Server Bağlantının Denendiği Yer
    /// (Eventler atandıktan sonra tetiklenmelidir .)
    /// </summary>
    public void Connect(NetworkConfig networkConfig)
    {
        _networkConfig = networkConfig;
        _socketClient = new Client();
        _socketClient.Connect(networkConfig.ip, networkConfig.port);
    }

    /// <summary>
    /// update içersine konulması gerekir 
    /// ve serverdan gelen datayı dinler .
    /// </summary>
    public void Listen()
    {
        if (_socketClient == null)
            return;

        while (_socketClient.GetNextMessage(out var message))
        {
            switch (message.eventType)
            {
                case EventType.Connected:
                    onConnected?.Invoke();
                    break;
                case EventType.Data:
                    OnMessageCallback(message.data);
                    break;
                case EventType.Disconnected:
                    onDisconnected?.Invoke();
                    break;
            }
        }
    }

    /// <summary>
    /// Serverdan kullanıcıya gelen datanın işlendiği yer
    /// </summary>
    /// <param name="data"></param>
    private void OnMessageCallback(byte[] data)
    {
        var jsonData = Encoding.UTF8.GetString(data);

        var comingData = JsonUtility.FromJson<EventKeyValue>(jsonData);

        if (onEventAdd.TryGetValue(comingData.key, out var getAction))
        {
            getAction(comingData.value);
        }
        else
        {
            Debug.LogError("Event not found !");
        }
    }

    /// <summary>
    /// Socket bağlantısını sonladnırıyoruz .
    /// </summary>
    public void ShotDown()
    {
        _socketClient.Disconnect();
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

    void SendData(string value)
    {
        byte[] sendData = Encoding.UTF8.GetBytes(value);
        _socketClient.Send(sendData);
    }
}
[Serializable]
public class NetworkConfig
{
    public NetworkConfig(string ip, int port)
    {
        this.ip = ip;
        this.port = port;
    }
    public string ip;
    public int port;
}