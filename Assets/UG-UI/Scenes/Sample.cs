using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sample : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            UGPanel.Open("test");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            UGPanel.Open("test2");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            UGPopup.Get("message").OnOpen.SetText("Bildirim Deniyrouz ").SetTitle("Bildirim !");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }
    }
}
