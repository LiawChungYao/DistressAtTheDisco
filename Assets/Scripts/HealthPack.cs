using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private int health = -50;
    
    public string playerTag = "Player";
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();

            if (playerHealth != null)
            {
                playerHealth.ApplyDamage(health);
                Destroy(this.gameObject);
            }
        }
    }
}
