using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveMenu : MonoBehaviour
{
    public Button clearButton;

    void Start()
    {
        if (clearButton == null)
        {
            Debug.LogError("clearButton 未綁定！");
            return;
        }

        clearButton.onClick.AddListener(ClearAllPlayerPrefs);
    }

    public void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("All PlayerPrefs have been cleared!");

        RestartGame();
    }

    private void RestartGame()
    {
        Time.timeScale = 1;  // 確保遊戲恢復正常速度
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Debug.Log("Scene Reloaded Successfully!");
    }
}




// using UnityEngine;
// using UnityEngine.UI;  // 引入 UI 命名空間
// using UnityEngine.SceneManagement;  // 引入場景管理命名空間

// public class SaveMenu : MonoBehaviour
// {
//     public Button clearButton;  // UI 按鈕

//     void Start()
//     {
//         // 確保按鈕有設定 OnClick 事件
//         if (clearButton != null)
//         {
//             clearButton.onClick.AddListener(ClearAllPlayerPrefs);  // 綁定按鈕點擊事件
//         }
//     }

//     // 清除所有 PlayerPrefs 的方法
//     public void ClearAllPlayerPrefs()
//     {
//         // 清除所有 PlayerPrefs
//         PlayerPrefs.DeleteAll();  
//         PlayerPrefs.Save();  // 保存更改

//         Debug.Log("All PlayerPrefs have been cleared!");  // 輸出提示

//         // 重啟遊戲
//         RestartGame();
//     }

//     // 重啟遊戲方法
//     private void RestartGame()
//     {
//         // 使用 SceneManager 重新加載當前場景
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//     }
// }






// using UnityEngine;
// using UnityEngine.UI;  // 引入 UI 命名空間

// public class SaveMenu : MonoBehaviour
// {
//     public Button clearButton;  // UI 按鈕

//     void Start()
//     {
//         // 確保按鈕有設定 OnClick 事件
//         if (clearButton != null)
//         {
//             clearButton.onClick.AddListener(ClearAllPlayerPrefs);  // 綁定按鈕點擊事件
//         }
//     }

//     // 清除所有 PlayerPrefs 的方法
//     public void ClearAllPlayerPrefs()
//     {
//         PlayerPrefs.DeleteAll();  // 清除所有 PlayerPrefs
//         PlayerPrefs.Save();  // 保存更改
//         Debug.Log("All PlayerPrefs have been cleared!");  // 輸出提示
//     }
// }

