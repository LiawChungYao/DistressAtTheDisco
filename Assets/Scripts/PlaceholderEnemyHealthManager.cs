using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderEnemyHealthManager : MonoBehaviour
{
    public int startingHealth = 50; 
    public int CurrentHealth;
    
    public GameObject healthPrefab;

    public void Damage(int damageAmount)
    {
        // Take the bullets damage away from the current health
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0 )
            {
                Destroy(gameObject);
                
            }

    }

    // This method is called to check if the enemy has died
    private void DeathCheck()
    {
        // THIS IS WHERE WE DO ANY DEATH ACTIONS
        
        // Chance of spawning healthpack
        if (Random.Range(0, 10) <= 10){
            // Spawn healthpack
            Instantiate(healthPrefab, transform.position, transform.rotation);
        }

        // Add score
        GameManager.instance.AddScore(100);

        // Destroy the enemy object
        Destroy(gameObject);
    }

    void Awake()
    {
        // Initialize the current health to the maximum health when the enemy spawns
       // _currentHealth = startingHealth;
        CurrentHealth = startingHealth;
        Debug.Log("current health" + CurrentHealth);
    }
}
