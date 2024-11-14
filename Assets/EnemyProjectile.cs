using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject FirePoint;
    public GameObject projectile;
    [SerializeField] private float shootCooldown;
    private float CurrentshootCooldown;
    public EnemyAi enemyai;
    [SerializeField] private float projectileSpeed;
    private bool s = false;
    private bool isAttacking = false;
    private Animator animator;
    public ParticleSystem[] shootingPFX;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (enemyai.canfire)
        {

            if (CurrentshootCooldown < 0)
            {
                foreach (ParticleSystem item in shootingPFX)
                {
                    item.Play();
                }

                StartCoroutine(Attackanim());
                GameObject SpawnedProjectile = Instantiate(projectile, FirePoint.transform.position, Quaternion.identity);
                SpawnedProjectile.GetComponent<Projectile>().enemyAi = enemyai;

                Vector2 dir = enemyai.Target.transform.position - SpawnedProjectile.transform.position;
                SpawnedProjectile.GetComponent<Rigidbody2D>().AddForce(dir * projectileSpeed, ForceMode2D.Impulse);

                CurrentshootCooldown = shootCooldown;
            }
            else
            {
                CurrentshootCooldown -= Time.deltaTime;
            }
        }
    }

    private void Update()
    {
        animator.SetBool("isAttacking", isAttacking);
    }

    IEnumerator Attackanim()
    {
            isAttacking = true;
        yield return new WaitForSeconds(0.5f);
            isAttacking = false;
    }
}
