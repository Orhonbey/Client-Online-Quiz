using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///  OptionSelect Button içersinde text metni ve id si oalcak ve tıklanılınca servera gidecek .
/// </summary>
public class OptionSelectButton : MonoBehaviour
{
    [HideInInspector]
    public string id;
    public Text optionText;
    [HideInInspector]
    public bool isSelect;

    public Image enemyImage;
    public Image correctImage;


    Button _mButton;
    private void OnEnable()
    {
        _mButton = GetComponent<Button>();
    }
    public void SetPanel(string id,string optionText)
    {
        this.optionText.text = optionText;
        this.id = id;
    }
    public void SelectOption()
    {
        if (GameManager.instance.playPanel.IsCheckBeforSelectButton())
        {
            isSelect = true;
            QuestionReply reply = new QuestionReply();
            reply.optionId = id;
            reply.roomId = RoomManager.instance.currectRoomData.id;
            if (!RoomManager.instance.currectRoomData.isFirstReply)
            { 
                reply.firstReply = true;
                RoomManager.instance.currectRoomData.isFirstReply = true;
            }
            else
            {
                /// Rakibin Şıkını göstermeliyiz burda 
                GameManager.instance.playPanel.EnemyButtonSelect(RoomManager.instance.reply);
            }
            var jsonData = JsonUtility.ToJson(reply);
            Debug.Log(jsonData);
            GameManager.instance.connection.SendData("OnQuestionReply", jsonData);
            _mButton.interactable = false;
        }
    }
}
