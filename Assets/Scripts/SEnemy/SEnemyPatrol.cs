using UnityEngine;

public class SEnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    
    [Header("Enemy")]
    [SerializeField] private Transform senemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = senemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (senemy.position.x >= leftEdge.position.x) // 使用 senemy 替代 enemy
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (senemy.position.x <= rightEdge.position.x) // 使用 senemy 替代 enemy
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        
        // Make enemy face direction
        senemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
                                         initScale.y, initScale.z);
        
        // Move in that direction
        senemy.position = new Vector3(senemy.position.x + Time.deltaTime * _direction * speed,
                                      senemy.position.y, senemy.position.z);
    }
}
