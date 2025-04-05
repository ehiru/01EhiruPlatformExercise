using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] protected float damage; // 可在 Inspector 設定傷害值
    private BoxCollider2D boxCollider;
    private Animator animator;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>(); // 獲取敵人的 BoxCollider2D
        animator = GetComponent<Animator>(); // 獲取 Animator
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 取得敵人的中心 Y 座標（更準確）
            float enemyCenterY = boxCollider.bounds.center.y;
            // 取得玩家的 Y 座標（用 transform.position，避免 bounds 受動畫影響）
            float playerY = collision.transform.position.y;

            // 玩家 **低於** 敵人，才會受傷
            if (playerY <= enemyCenterY)
            {
                Debug.Log($"玩家從敵人底部碰撞，受到 {damage} 傷害！");
                collision.GetComponent<Health>()?.TakeDamage(damage);

                // 觸發動畫
                if (animator != null)
                {
                    animator.SetTrigger("Hit"); // 確保 Animator 有 "Hit" 觸發器
                }
            }
            else
            {
                Debug.Log("玩家從側面或上方碰到敵人，不造成傷害");
            }
        }
    }
}
