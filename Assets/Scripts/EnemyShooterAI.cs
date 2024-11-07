using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterAI : MonoBehaviour
{
    [Header("Movement")]
    public GameObject Target;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float orbitDistance = 2f;
    [SerializeField] private float oscillationSpeed = 2f;
    [SerializeField] private float oscillationWidth = 2f;

    private bool isOscillating = false;
    private float oscillationDirection = 1f; // 1 for right, -1 for left
    private Vector2 oscillationCenter;

    private EnemyHp enemyHp;

    [Header("Attack")]
    [SerializeField] private float Damage = 1f;
    [SerializeField] private float shootRange = 3f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float shootCooldown = 1f;
    private float lastShootTime = 0f;

    private void Start()
    {
        enemyHp = GetComponent<EnemyHp>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!enemyHp.isDead && Target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, Target.transform.position);

            if (!isOscillating)
            {
                // Move directly towards the target until within orbit distance
                if (distanceToTarget > orbitDistance)
                {
                    Vector2 direction = (Target.transform.position - transform.position).normalized;
                    rb.velocity = direction * speed;
                    Debug.Log("rör sig mot target");
                }
                else
                {
                    // Start oscillating once the enemy is close enough
                    isOscillating = true;
                    oscillationCenter = Target.transform.position;
                    rb.velocity = Vector2.zero;
                    Debug.Log("orbit");
                }
            }
            else
            {
                OscillateAroundTarget();

                // Check if enemy is close enough to shoot
                if (distanceToTarget <= shootRange && Time.time > lastShootTime + shootCooldown)
                {
                    ShootAtTarget();
                    lastShootTime = Time.time;
                    Debug.Log("Tillräkligt nära för att skjuta");
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OscillateAroundTarget()
    {
        // Move left and right around the oscillation center (target position)
        float xPosition = Mathf.PingPong(Time.time * oscillationSpeed, oscillationWidth) - (oscillationWidth / 2f);
        Vector2 newPos = oscillationCenter + new Vector2(xPosition * oscillationDirection, 0);

        transform.position = newPos;

        // Flip direction when reaching the edges
        if (Mathf.Abs(xPosition) >= (oscillationWidth / 2f))
        {
            oscillationDirection *= -1;
        }
    }

    private void ShootAtTarget()
    {
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector2 direction = (Target.transform.position - transform.position).normalized;
            projectile.GetComponent<Rigidbody2D>().velocity = direction * 10f; // Set your desired projectile speed here
        }
    }
}
