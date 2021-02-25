using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanel : MonoBehaviour
{
    [HideInInspector]
    public List<OptionSelectButton> optionSelectButtons = new List<OptionSelectButton>();
    public OptionSelectButton prefabOptionButton;
    public Transform parent;

    public Text time;

    public Text myProfileName;
    public Text enemyProfileName;

    public Text myScoreText;
    public Text enemyScoreText;

    public Image enemyProfileImage;

    public Text questionText;

    public int questionTime = 30;

    public int timerId;

    /// <summary>
    /// Soru Paneli Updateleniyor .
    /// </summary>
    /// <param name="roomData"></param>
    public void SetPanel(RoomData roomData, bool isEnemyImageChange = true)
    {
        var enemyName = "";
        if (GameManager.instance.playerName.Equals(roomData.clientFirstName))
        {
            enemyName = roomData.clientSecondName;
            myScoreText.text = roomData.clientFirstScore.ToString();
            enemyScoreText.text = roomData.clientSecondScore.ToString();
        }
        else
        {
            myScoreText.text = roomData.clientSecondScore.ToString();
            enemyScoreText.text = roomData.clientFirstScore.ToString();
            enemyName = roomData.clientFirstName;
        }

        myProfileName.text = GameManager.instance.playerName;
        enemyProfileName.text = enemyName;

        if (isEnemyImageChange)
            enemyProfileImage.sprite = SpriteManager.instance.GetRandomProfileSprite();

        questionText.text = roomData.questionClient.questionText;

        for (int i = 0; i < optionSelectButtons.Count; i++)
        {
            Destroy(optionSelectButtons[i].gameObject);
        }
        optionSelectButtons.Clear();
        var options = roomData.questionClient.options;
        prefabOptionButton.gameObject.SetActive(true);
        for (int i = 0; i < options.Length; i++)
        {
            var cloneOptionButton = Instantiate(prefabOptionButton, parent);
            cloneOptionButton.SetPanel(options[i].optionId, options[i].option);
            optionSelectButtons.Add(cloneOptionButton);
        }
        prefabOptionButton.gameObject.SetActive(false);

        ///Timer Başaltılıyor .
        timerId =  LeanTween.value(questionTime, 0, questionTime).setOnUpdate((float x) =>
          {
              time.text = ((int)x).ToString();
          }).id;
    }
    /// <summary>
    /// Daha önce Oyuncu Butona Tıklamışmı diye baklıyor .
    /// </summary>
    /// <returns></returns>
    public bool IsCheckBeforSelectButton()
    {
        var selectButton = optionSelectButtons.Find(x => x.isSelect);
        if (selectButton != null)
        {
            return false;
        }

        return true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reply"></param>
    public void EnemyButtonSelect(QuestionReply reply)
    {
        var enemySelect = optionSelectButtons.Find(x => x.id == reply.optionId);

        if (enemySelect != null)
        {
            enemySelect.enemyImage.sprite = enemyProfileImage.sprite;
            enemySelect.enemyImage.gameObject.SetActive(true);
        }
    }

    public void QuestionCorrectOptions(QuestionFinishData correctFinishData)
    {
        var correctOption = optionSelectButtons.Find(x => x.id == correctFinishData.reply);

        if (correctOption != null)
        {
            correctOption.correctImage.gameObject.SetActive(true);
        }

        if (GameManager.instance.playerName.Equals(correctFinishData.clientFirstName))
        {
            myScoreText.text = correctFinishData.clientFirstScore.ToString();
            enemyScoreText.text = correctFinishData.clientSecondScore.ToString();
        }
        else
        {
            myScoreText.text = correctFinishData.clientSecondScore.ToString();
            enemyScoreText.text = correctFinishData.clientFirstScore.ToString();
        }
    }

}
