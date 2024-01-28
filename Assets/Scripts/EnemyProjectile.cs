using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject enemyProjectilePrefab; // Bullet to be shot by the enemy
    public Transform enemyShootPoint; // Where the enemy is shooting from
    public float enemyShootForce = 30f;
    public float enemyFireRate = 2f; // Time between the enemy shots

    private float nextEnemyFireTime = 2f;
    private Transform playerTransform;
    public float projectileLifetime = 2f; // Bullets destroyed after this amount of time

    void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }
    void Update()
    {
        if (Time.time >= nextEnemyFireTime)
        {
            ShootEnemyProjectile();
            nextEnemyFireTime = Time.time + enemyFireRate;
        }
    }

    private void ShootEnemyProjectile()
    {
        Vector3 shootDirection = (playerTransform.position - transform.position).normalized;
        Vector3 shootOrigin = transform.position + shootDirection; 


        GameObject newEnemyProjectile = Instantiate(enemyProjectilePrefab, shootOrigin, Quaternion.identity);
        Rigidbody projectileRigidbody = newEnemyProjectile.GetComponent<Rigidbody>();

        if (projectileRigidbody != null)
        {
            projectileRigidbody.AddForce(shootDirection * enemyShootForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Enemy projectile doesn't have a rigidbody component");
        }
        // Destroy(newEnemyProjectile, projectileLifetime )
    }
}
