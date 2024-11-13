using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject FirePoint;
    public GameObject projectile;
    [SerializeField] private float shootCooldown;
    private float CurrentshootCooldown;
    public EnemyAi enemyai;
    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (CurrentshootCooldown < 0)
        {
            GameObject SpawnedProjectile = Instantiate(projectile, FirePoint.transform.position, Quaternion.identity);
            CurrentshootCooldown = shootCooldown;
        }
        else
        {
            CurrentshootCooldown -= Time.deltaTime;
        }
    }
}
