
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireblasts;
    [SerializeField] private AudioClip fireblastSound;


    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireblastSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireblasts[FindFireblast()].transform.position = firePoint.position;
        fireblasts[FindFireblast()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindFireblast()
    {
        for (int i = 0; i < fireblasts.Length; i++)
        {
            if (!fireblasts[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
