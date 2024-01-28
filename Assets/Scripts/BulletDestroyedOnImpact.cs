using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyedOnImpact : MonoBehaviour
{

    public string enemyTag = "Enemy";
    public string playerTag = "Player";

    public int damage;
    public int enemyBulletDamage;

    // Removes the bullet once it hits something
    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.CompareTag(enemyTag))
        {
            PlaceholderEnemyHealthManager placeholderEnemyHealthManager = collision.gameObject.GetComponent<PlaceholderEnemyHealthManager>();

            if (placeholderEnemyHealthManager != null)
            {
                placeholderEnemyHealthManager.Damage(damage);
            }
            else
            {
                Debug.LogWarning("No EnemyHealthManager script found on the enemy GameObject.");
            }

        }

        else if (collision.gameObject.CompareTag(playerTag))
        {
            HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();

            if (playerHealth != null)
            {
                playerHealth.ApplyDamage(enemyBulletDamage);
            }
            else
            {
                Debug.LogWarning("No HealthManager script found on the player GameObject.");
            }
        }
        Destroy(this.gameObject);
    }
}
