using UnityEngine;

public class Pivot : MonoBehaviour
{
    public float speed = 2f; // Swing speed
    public float minAngle = -45f; // Minimum rotation angle
    public float maxAngle = 45f;  // Maximum rotation angle

    private void Update()
    {
        // Calculate oscillating angle
        float t = Mathf.Sin(Time.time * speed);
        float angle = Mathf.Lerp(minAngle, maxAngle, (t + 1) / 2);

        // Rotate this object (which should be the PIVOT, not the swinging object itself)
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
