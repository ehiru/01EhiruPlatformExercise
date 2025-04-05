using UnityEngine;
using UnityEngine.UI;

public class QuestionUI : MonoBehaviour
{
    public Text questionText;   // 顯示問題的 Text 元件
    public Button[] answerButtons;  // 用來顯示答案的按鈕

    private int score;  // 玩家得分

    public void SetupQuestion(string question, string[] options, int[] points)
    {
        questionText.text = question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].GetComponentInChildren<Text>().text = options[i];
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(points[index]));
        }
    }

    private void OnAnswerSelected(int points)
    {
        score += points;
        // 在這裡，你可以關閉問題面板，並進行下一步的遊戲邏輯
        Debug.Log("Current Score: " + score);
    }
}
