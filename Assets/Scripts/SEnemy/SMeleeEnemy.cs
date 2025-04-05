using UnityEngine;

public class SMeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown; // 冷却时间
    [SerializeField] private int damage; // 伤害值

    [Header("Collider Settings")]
    [SerializeField] private BoxCollider2D boxCollider; // 检测范围的碰撞器
    [SerializeField] private LayerMask playerLayer; // 玩家所在的层

    private float cooldownTimer = Mathf.Infinity; // 冷却计时器

    private void Awake()
    {
        // 自动获取 BoxCollider2D 如果没有手动赋值
        if (boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider2D>();
            if (boxCollider == null)
            {
                Debug.LogError("BoxCollider2D is missing! Please add it to the GameObject or assign it in the Inspector.");
            }
        }
    }

    private void Update()
    {
        // 更新冷却计时器
        cooldownTimer += Time.deltaTime;

        // 如果玩家在视野中且冷却结束，则执行攻击逻辑
        if (PlayerInSight() && cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }

    private bool PlayerInSight()
    {
        // 使用 BoxCast 检测玩家是否在检测范围内
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,           // 检测范围的中心点
            boxCollider.bounds.size,             // 检测范围的大小
            0,                                   // 旋转角度
            Vector2.left,                        // 检测方向（这里是左侧）
            0,                                   // 检测距离（0表示只检测重叠）
            playerLayer                          // 检测的层
        );

        return hit.collider != null; // 如果检测到玩家，返回 true
    }

    private void Attack()
    {
        // 攻击逻辑
        cooldownTimer = 0; // 重置冷却计时器
        Debug.Log("Attacking player with " + damage + " damage!");
        // 在这里添加实际攻击逻辑，例如减少玩家生命值
    }

    private void OnDrawGizmos()
    {
        // 可视化检测范围，用于调试
        if (boxCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
        }
    }
}
