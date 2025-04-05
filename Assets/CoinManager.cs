// using UnityEngine;
// using UnityEngine.UI;

// public class CoinManager : MonoBehaviour
// {
//     public int coinCount;
//     public Text coinText;

//     void Start()
//     {

//     }

//     void Update()
//     {
//         coinText.text = "Coin Count: " + coinCount.ToString();
//     }
// }

using UnityEngine.UI;
using UnityEngine;


public class CoinManager : MonoBehaviour
{
    public int coinCount;  // 讓 coinCount 保持 public
    public Text coinText;  // 引入 TMP_Text 來顯示金幣數量


    void Update()
    {
        coinText.text = "Coin Count: " + coinCount.ToString();
    }


    private void Start()
    {
        // 載入已儲存的金幣數量，並顯示在UI中
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);
        Debug.Log("Loaded Coin Count: " + coinCount);  // 打印加載的金幣數量

        // 確保 UI 正確顯示金幣數量
        UpdateCoinText();
    }

    private void OnApplicationQuit()
    {
        // 確保退出遊戲時保存 PlayerPrefs
        PlayerPrefs.SetInt("CoinCount", coinCount);
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs saved with CoinCount: " + coinCount);  // 打印保存的金幣數量
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;  // 增加金幣數量
        PlayerPrefs.SetInt("CoinCount", coinCount);  // 儲存到 PlayerPrefs
        PlayerPrefs.Save();  // 儲存數據

        Debug.Log("Added " + amount + " coins, new CoinCount: " + coinCount);  // 打印新增後的金幣數量

        // 更新 UI 顯示
        UpdateCoinText();
    }

    // 更新 UI 顯示金幣數量
    public void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coin Count: " + coinCount.ToString();  // 更新金幣數量到 UI
        }
    }
}
