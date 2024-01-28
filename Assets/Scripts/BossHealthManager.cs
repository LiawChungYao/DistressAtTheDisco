using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthManager : MonoBehaviour
{
    public int maxHealth = 200; 
    private int currentHealth; 
    public BossMovementScript bossMovementScript;
    public int currentStage;

    private void Start() {
        currentHealth = maxHealth;
    }


    public void Damage(int damageAmount)
    {
        // Take the bullets damage away from the current health
        currentHealth -= damageAmount;


        // Check if the enemy should be dead if their health goes below 0
        if (currentHealth <= 0)
        {

            DeathCheck();
        }
    }

    // This method is called to check if the enemy has died
    private void DeathCheck()
    {
        // THIS IS WHERE WE DO ANY DEATH ACTIONS
        
        // Destroy the enemy object
        
        Destroy(gameObject);
        
        // When boss is destroyed, win the game.
    }

    private void OnCollisionEnter(Collision collision) {

        // Need to make it so this only works in last minute, during boss fight
        if (collision.gameObject.CompareTag("Projectile") && currentStage == 5) {
            Damage(collision.gameObject.GetComponent<BulletDestroyedOnImpact>().damage);
        }

        
    }

    public void newInterval() {
        currentStage++;
    }
}
