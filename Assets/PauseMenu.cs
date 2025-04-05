using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PauseMenu : MonoBehaviour
{
   public GameObject PausePanel; // 在 Inspector 內設定對應 UI Panel

   // 暫停遊戲並顯示選單
   public void Pause()
   {
       PausePanel.SetActive(true);
       Time.timeScale = 0;
   }

   // 繼續遊戲並關閉選單
   public void Continue()
   {
      PausePanel.SetActive(false);
      Time.timeScale =1;
   }
}
