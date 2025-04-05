using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private bool isCollected = false;

    private void Start()
    {
        // 檢查是否有儲存該物品的收集狀態
        if (PlayerPrefs.HasKey(gameObject.tag + "Coin"))
        {
            isCollected = PlayerPrefs.GetInt(gameObject.tag + "Coin") == 1;
        }

        // 只有在物品已經被收集過的情況下，才隱藏物品
        if (isCollected)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 當玩家接觸到物品，並且物品尚未被收集過
        if (collision.gameObject.CompareTag("Player") && !isCollected)
        {
            // 設定物品為已收集
            isCollected = true;
            gameObject.SetActive(false); // 隱藏物品，假設它被收集

            // 儲存物品狀態（是否收集）到 PlayerPrefs
            PlayerPrefs.SetInt(gameObject.tag + "Coin", 1); // 物品是否收集
            PlayerPrefs.SetFloat(gameObject.tag + "X", transform.position.x); // 儲存位置
            PlayerPrefs.SetFloat(gameObject.tag + "Y", transform.position.y);
            PlayerPrefs.SetFloat(gameObject.tag + "Z", transform.position.z);
            PlayerPrefs.Save();

            // 在這裡可以播放道具拾取的音效或觸發其他事件
        }
    }
}
