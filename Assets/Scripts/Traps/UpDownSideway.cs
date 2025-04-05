using UnityEngine;

public class UpDownSideway : MonoBehaviour
{
    [SerializeField] private float movementDistance = 2f; // 移動範圍
    [SerializeField] private float speed = 2f; // 移動速度
    private bool movingUp;
    private float topEdge;
    private float bottomEdge;

    private void Awake()
    {
        topEdge = transform.position.y + movementDistance;
        bottomEdge = transform.position.y - movementDistance;
    }

    private void Update()
    {
        // 平台上下移動
        if (movingUp)
        {
            if (transform.position.y < topEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingUp = false;
            }
        }
        else
        {
            if (transform.position.y > bottomEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingUp = true;
            }
        }
    }

    // 當玩家站在平台上時，讓玩家成為平台的子物件，但保持世界座標不變
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform, true); // true 確保縮放不受影響
        }
    }

    // 當玩家離開平台時，取消父子關係
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}



