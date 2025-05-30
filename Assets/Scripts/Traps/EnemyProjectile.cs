// using UnityEngine;

// public class EnemyProjectile : EnemyDamage
// {
//     [SerializeField] private float speed;
//     [SerializeField] private float resetTime;
//     private float lifetime;
//     private Animator anim;
//     private BoxCollider2D coll;

//     private bool hit;

//     private void Awake()
//     {
//         anim = GetComponent<Animator>();
//         coll = GetComponent<BoxCollider2D>();
//     }

//     public void ActivateProjectile()
//     {
//         hit = false;
//         lifetime = 0;
//         gameObject.SetActive(true);
//         coll.enabled = true;
//     }
//     private void Update()
//     {
//         if (hit) return;
//         float movementSpeed = speed * Time.deltaTime;
//         transform.Translate(movementSpeed, 0, 0);

//         lifetime += Time.deltaTime;
//         if (lifetime > resetTime)
//             gameObject.SetActive(false);
//     }

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         hit = true;
//         base.OnTriggerEnter2D(collision); //Execute logic from parent script first
//         coll.enabled = false;

//         if (anim != null)
//             anim.SetTrigger("explode"); //When the object is a fireball explode it
//         else
//             gameObject.SetActive(false); //When this hits any object deactivate arrow
//     }
//     private void Deactivate()
//     {
//         gameObject.SetActive(false);
//     }
// }

using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float resetTime = 5f;

    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;
    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        // Lazy initialization in case Awake() was skipped
        if (anim == null) anim = GetComponent<Animator>();
        if (coll == null) coll = GetComponent<BoxCollider2D>();

        hit = false;
        lifetime = 0f;

        gameObject.SetActive(true);

        if (coll != null)
            coll.enabled = true;
        else
            Debug.LogWarning("EnemyProjectile: BoxCollider2D not found.");
    }

    private void Update()
    {
        if (hit) return;

        float moveX = speed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision);

        if (coll != null)
            coll.enabled = false;

        if (anim != null)
        {
            anim.SetTrigger("explode");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Animation event calls this
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
