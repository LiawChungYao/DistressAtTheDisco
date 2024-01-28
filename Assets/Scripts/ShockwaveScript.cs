using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public int pointsCount;
    public float maxRadius;
    public float speed;
    public float force;
    public int shockwaveDamage = 25;
    public float startWidth;
    public PlayerMovement playerMovementScript;
    public HealthManager playerHealthManager;
    private HashSet<GameObject> affectedObjects = new HashSet<GameObject>();
    private AudioSource audioSource;




    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = pointsCount + 1;

        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator Blast() {
        PlayShockwaveSound();
        float currentRadius = 0f;
        while (currentRadius < maxRadius) {
            currentRadius += Time.deltaTime * speed;
            Draw(currentRadius);
            Damage(currentRadius);
            yield return null;
        }
        affectedObjects.Clear();
    }
    private void Damage(float currentRadius) {

        // All objects that are hit by the shockwave. Loop through them to apply effects
        Collider[] hittingObjects = Physics.OverlapSphere(transform.position, currentRadius);

        for (int i = 0; i < hittingObjects.Length; i++) {

            // If the object has already been effected by the shockwave, don't apply any more effects
            if (affectedObjects.Contains(hittingObjects[i].gameObject)) {
                continue;
            }

            // Direction of shockwave push
            Vector3 direction = (hittingObjects[i].transform.position - transform.position).normalized;
            direction.y += 0.5f;

            // Logic for shockwave htting the player. Only applies if player is on ground. So player can jump over it
            if (hittingObjects[i].CompareTag("Player")) {
                if (playerMovementScript.isGrounded) {

                    playerMovementScript.ApplyKnockback(direction * force);
                    playerMovementScript.isKnockbackImmume = true;
                    playerHealthManager.ApplyDamage(shockwaveDamage);
                    StartCoroutine(ResetPlayerKnockbackImmunity(playerMovementScript));
                }   
                affectedObjects.Add(hittingObjects[i].gameObject);
            }
            
            // Logic for enemy being hit by shockwave. Shouldn't take damage, just be pushed back.
            if (hittingObjects[i].CompareTag("Enemy")) {
                Debug.Log("Enemy hit by shockwave");
                Rigidbody rb = hittingObjects[i].GetComponent<Rigidbody>();
                if (rb) {
                    rb.AddForce(direction * force, ForceMode.Impulse);
                }
               affectedObjects.Add(hittingObjects[i].gameObject); 
            }
        }
    } 

    // Function to deal with knockback immunity
    private IEnumerator ResetPlayerKnockbackImmunity(PlayerMovement playerMovement) {
        yield return new WaitForSeconds(1f);
        playerMovement.ResetKnockbackImmunity();
    }
    
    // Function to visually create the shockwave
    private void Draw(float currentRadius) {
        float angleBetweenPoints = 360f / pointsCount;
        for (int i = 0; i <= pointsCount; i++) {
            float angle = i * angleBetweenPoints * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
            Vector3 position = direction * currentRadius;
            lineRenderer.SetPosition(i, position);
        }
        lineRenderer.widthMultiplier = Mathf.Lerp(0f, startWidth, 1f - currentRadius / maxRadius);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            StartCoroutine(Blast());
        }
    }

    private void PlayShockwaveSound() {
        if (audioSource && audioSource.clip) {
            audioSource.Play();
        }
    }

    public void StartShockwave() {
        StartCoroutine(MultipleShockwaves());
    }

    private IEnumerator MultipleShockwaves() {
        for (int i = 0; i < 3; i++) {
            StartCoroutine(Blast());
            yield return new WaitForSeconds(1.5f);
        }
    }
    
}
