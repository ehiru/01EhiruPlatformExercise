using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameManager : MonoBehaviour
{
    public void RestartGame()
    {
        // 清除 Checkpoint 存檔，讓遊戲從頭開始
        PlayerPrefs.DeleteKey("CheckpointX");
        PlayerPrefs.DeleteKey("CheckpointY");
        PlayerPrefs.DeleteKey("CheckpointZ");
        PlayerPrefs.Save();

        // 重新載入場景，玩家將從起始點開始
        SceneManager.LoadScene("Level1");
    }
}
