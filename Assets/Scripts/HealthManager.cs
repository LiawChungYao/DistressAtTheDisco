using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] public int startingHealth = 100;

    [SerializeField] private UnityEvent onDeath;

    [SerializeField] private UnityEvent<float> onHealthChanged;
    public OnDeathManager onDeathManager;
    public MouseLook mouseLook;
    public Timer timerScript;
    public RecoilScript gun1Recoil;
    public RecoilScript gun2Recoil;
    public GameManager gameManager;

    public int _currentHealth;

    public int CurrentHealth
    {

        get => this._currentHealth;
        set
        {        
            this._currentHealth = value;
            var frac = this._currentHealth / (float)this.startingHealth;
            this.onHealthChanged.Invoke(frac);
            if (CurrentHealth <= 0 && !onDeathManager.winningPanel.activeInHierarchy )
            {

                onDeathManager.ShowDeathUI();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                this.onDeath.Invoke();
                this.GetComponent<PlayerMovement>().enabled = false;
                this.GetComponent<ProjectileShooter>().enabled = false;
                mouseLook.canLook = false;
                timerScript.enabled = false;
                gun1Recoil.enabled = false;
                gun2Recoil.enabled = false;
                gameManager.DisplayLosingScore();
           
                //Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        ResetHealthToStarting();
        
    }

    public void ResetHealthToStarting()
    {
        CurrentHealth = this.startingHealth;
    }

    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth >=startingHealth){
            CurrentHealth = startingHealth;
        }
    }
}
