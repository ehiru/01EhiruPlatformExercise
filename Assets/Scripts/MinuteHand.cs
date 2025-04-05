using UnityEngine;

public class MinuteHand : MonoBehaviour
{
    [SerializeField] private Transform pivot; // 讓 Unity Inspector 顯示此變數
    public float rotationSpeed = 30f; // 旋轉速度（度/秒）

    void Update()
    {
        if (pivot != null)
        {
            transform.RotateAround(pivot.position, Vector3.forward, -rotationSpeed * Time.deltaTime);
        }
    }
}
