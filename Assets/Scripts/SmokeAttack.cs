using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAttack : MonoBehaviour
{
    

    public ParticleSystem smokeParticleSystem;
    public float attackDuration = 3f;
    public Transform target;
    public int smokeDamage = 25;
    private AudioSource audioSource;

    public void EmitSmoke() {

        Debug.Log("Emit smoke entered");
        Vector3 directionToPlayer = (target.position - transform.position).normalized;
        transform.forward = directionToPlayer;

        smokeParticleSystem.Play();
        PlaySmokeSound();
        Invoke("StopEmitting", attackDuration);
    }

    public void Start() {
        smokeParticleSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void StopEmitting() {
        smokeParticleSystem.Stop();
    }

    private void PlaySmokeSound() {
        if (audioSource && audioSource.clip) {
            audioSource.Play();
        }
    }

    void Update() {
        if (Input.GetKey(KeyCode.N)) {
            Debug.Log("N key pressed");
            EmitSmoke();
        }
    }

    public void OnParticleCollision(GameObject other) {
        Debug.Log("Particles collide with player");
        if (other.CompareTag("Player")) {
            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null) {
                healthManager.ApplyDamage(smokeDamage);
            }
        }
    }
}
