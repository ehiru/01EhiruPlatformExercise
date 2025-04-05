using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinID;  // 每個硬幣的唯一 ID
    [SerializeField] private AudioClip collectSound;  // 收集音效

    private void Start()
    {
        // 檢查這個硬幣是否已經被收集過
        if (PlayerPrefs.GetInt("Coin_" + coinID, 0) == 1)
        {
            // 如果已經收集過，則銷毀這個硬幣
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 當玩家碰到硬幣時，收集並讓硬幣消失
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        // 播放收集音效
        if (collectSound != null)
        {
            SoundManager.instance.PlaySound(collectSound);
        }

        // 記錄這個硬幣已經被收集
        PlayerPrefs.SetInt("Coin_" + coinID, 1);
        PlayerPrefs.Save();  // 立刻保存資料

        // 銷毀硬幣物件，使其永久消失
        Destroy(gameObject);
    }
}

