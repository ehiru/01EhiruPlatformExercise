using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private Transform pivot; // 擺盪的中心點
    public float rotationSpeed = 30f; // 旋轉速度（度/秒）

    private float currentAngle = 0f; // 當前擺盪角度
    private bool swingingRight = true; // 控制擺盪方向

    void Update()
    {
        if (pivot != null)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;

            if (swingingRight)
            {
                currentAngle += rotationStep;
                if (currentAngle >= 90f) // 轉到最大角度
                {
                    currentAngle = 90f;
                    swingingRight = false; // 換方向
                }
            }
            else
            {
                currentAngle -= rotationStep;
                if (currentAngle <= -90f) // 轉到最小角度
                {
                    currentAngle = -90f;
                    swingingRight = true; // 換方向
                }
            }

            // 讓物體圍繞 pivot 擺動
            transform.RotateAround(pivot.position, Vector3.forward, swingingRight ? rotationStep : -rotationStep);
        }
    }
}
