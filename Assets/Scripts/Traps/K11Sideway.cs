using UnityEngine;

public class K11Sideway : MonoBehaviour
{
    [SerializeField] private float movementDistance;  // 移動範圍
    [SerializeField] private float speed;  // 移動速度
    private bool movingLeft;  // 物體是否朝左移動
    private float leftEdge;  // 左邊界
    private float rightEdge;  // 右邊界

    private void Awake()
    {
        // 計算邊界
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        // 根據移動方向移動物體
        if (movingLeft)
        {
            // 確保物體不會越過左邊界
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;  // 到達左邊界後反轉方向
            }
        }
        else
        {
            // 確保物體不會越過右邊界
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;  // 到達右邊界後反轉方向
            }
        }
    }

    // 當進入觸發區域時，反轉移動方向
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 確保反轉移動方向的邏輯
        movingLeft = !movingLeft;
    }
}




