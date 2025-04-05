using UnityEngine;
using UnityEngine.UI;  // 引入UI命名空間來控制Text和Button

public class NPCInteraction : MonoBehaviour
{
    public GameObject questionPanel;      // 顯示問題和選項的UI面板
    public Text questionTextUI;           // 顯示問題的Text元件
    public Button[] answerButtons;        // 可能的選項按鈕

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowQuestion();  // 當玩家進入觸發區域時顯示問題
        }
    }

    private void ShowQuestion()
    {
        string question = "Hi, do you want my tool?";  // 設定問題文字
        string[] answers = { "Yes", "No" };  // 設定可能的答案

        // 顯示問題
        questionTextUI.text = question;

        // 顯示問題面板
        questionPanel.SetActive(true);

        // 設置選項按鈕的文本並綁定按鈕的事件
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < answers.Length)
            {
                answerButtons[i].gameObject.SetActive(true);  // 顯示按鈕
                answerButtons[i].GetComponentInChildren<Text>().text = answers[i];  // 設置按鈕文字
                int index = i;  // 防止閉包問題
                answerButtons[i].onClick.RemoveAllListeners();  // 清除舊的事件
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));  // 設置按鈕事件
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);  // 隱藏額外的按鈕
            }
        }
    }

    private void OnAnswerSelected(int index)
    {
        if (index == 0)
        {
            Debug.Log("Player chose Yes.");
            // 在這裡處理「Yes」選項的邏輯
        }
        else if (index == 1)
        {
            Debug.Log("Player chose No.");
            // 在這裡處理「No」選項的邏輯
        }

        HideQuestion();  // 隱藏問題面板
    }

    private void HideQuestion()
    {
        questionPanel.SetActive(false);  // 隱藏問題面板
    }
}


