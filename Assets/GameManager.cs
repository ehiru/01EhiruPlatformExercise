using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int currentLevel;
    private float playerHealth;

    // 確保 GameManager 唯一且不會被場景切換刪除
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持 GameManager 在場景間不被銷毀
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 設定關卡
    public void SetLevel(int level)
    {
        currentLevel = level;
    }

    // 設定玩家健康
    public void SetPlayerHealth(float health)
    {
        playerHealth = health;
    }

    // 獲取關卡
    public int GetLevel()
    {
        return currentLevel;
    }

    // 獲取玩家健康
    public float GetPlayerHealth()
    {
        return playerHealth;
    }
}
