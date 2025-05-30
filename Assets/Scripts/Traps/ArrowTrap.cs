
// using UnityEngine;

// public class ArrowTrap : MonoBehaviour
// {
//     [SerializeField] private float attackCooldown;
//     [SerializeField] private Transform firePoint;
//     [SerializeField] private GameObject[] arrows;
//     private float cooldownTimer;

//     [Header("SFX")]
//     [SerializeField] private AudioClip arrowSound;

//     private void Attack()
//     {
//         cooldownTimer = 0;

//         SoundManager.instance.PlaySound(arrowSound);
//         arrows[FindArrow()].transform.position = firePoint.position;
//         arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
//     }
//     private int FindArrow()
//     {
//         for (int i = 0; i < arrows.Length; i++)
//         {
//             if (!arrows[i].activeInHierarchy)
//                 return i;
//         }
//         return 0;
//     }
//     private void Update()
//     {
//         cooldownTimer += Time.deltaTime;

//         if (cooldownTimer >= attackCooldown)
//             Attack();
//     }
// }

using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        cooldownTimer = 0;

        int arrowIndex = FindArrow();
        if (arrowIndex == -1)
        {
            Debug.LogWarning("ArrowTrap: No inactive arrows available!");
            return;
        }

        GameObject arrow = arrows[arrowIndex];
        if (arrow == null)
        {
            Debug.LogError("ArrowTrap: Arrow reference is null.");
            return;
        }

        arrow.transform.position = firePoint.position;

        EnemyProjectile projectile = arrow.GetComponent<EnemyProjectile>();
        if (projectile == null)
        {
            Debug.LogError("ArrowTrap: Missing EnemyProjectile component on arrow.");
            return;
        }

        SoundManager.instance?.PlaySound(arrowSound);
        projectile.ActivateProjectile();
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return -1;
    }
}
