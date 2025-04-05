using UnityEngine;

public class NewMenuManager : MonoBehaviour
{
   public GameObject PausePanel; // 在 Inspector 內設定對應 UI Panel

   // 暫停遊戲並顯示選單
   public void Pause()
   {
       if (PausePanel != null)
       {
           PausePanel.SetActive(true);
           Time.timeScale = 0f; // 暫停遊戲
       }
   }

   // 繼續遊戲並關閉選單
   public void Continue()
   {
       if (PausePanel != null)
       {
           PausePanel.SetActive(false);
           Time.timeScale = 1f; // 恢復遊戲
       }
   }
}




