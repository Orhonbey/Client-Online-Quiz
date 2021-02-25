using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-90)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ConnectionWeb connection;
    /// <summary>
    /// Find User Panel Datası
    /// </summary>
    [HideInInspector]
    public FindUserPanel findUserPanel;
    [HideInInspector]
    public PlayPanel playPanel;
    [HideInInspector]
    public FinishPanel finishPanel;
    /// <summary>
    /// Ramde tutulur oyuncu oyundan çıkmadığı sürece kullanılır .
    /// </summary>
    [HideInInspector]
    public string playerName;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        /// UGUI Paneli Üzerinden atamsaı yapılıyor .
        findUserPanel = UGPanel.Get("FindUser").GetComponent<FindUserPanel>();
        playPanel = UGPanel.Get("Play").GetComponent<PlayPanel>();
        finishPanel = UGPanel.Get("Finish").GetComponent<FinishPanel>();
        connection.Connect();
        RoomManager.instance.SetManager();
    }
}
