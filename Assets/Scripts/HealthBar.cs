using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HealthManager playerHealth; 
    public PlaceholderEnemyHealthManager enemyHealth;
    public Image playerHealthBar; 
    [SerializeField] private Slider slider;

    private void Update()
    {
        if(playerHealthBar!= null && playerHealth != null){
        float playerHealthPercentage = (float)playerHealth.CurrentHealth / playerHealth.startingHealth;
        playerHealthBar.fillAmount = playerHealthPercentage;
        }
 
        if(slider!= null && enemyHealth != null){
            float enemyHealthPercentage = (float)enemyHealth.CurrentHealth/ enemyHealth.startingHealth;
            slider.value = (float)enemyHealthPercentage;
        }
    }


}
