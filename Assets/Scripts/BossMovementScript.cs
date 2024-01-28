using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovementScript : MonoBehaviour
{

    public float minX = -25f;
    public float maxX = 25f;
    public float speed = 10f;

    public float minPauseTime = 2f;
    public float maxPauseTime = 8f;

    private float targetX;

    private bool isMoving = false;
    private int currentStage = 0;

    private SmokeAttack smokeAttackLeft;
    // private SmokeAttack smokeAttackRight;
    private ShockwaveScript shockwave;
    private BossDodge bossDodge;
    // public GameObject spawnerPrefab;
    public Vector2 roomMin, roomMax;
    private AudioSource audioSource;

    private void Start() {

        
        smokeAttackLeft = transform.Find("Desk/SmokeMachineLeft/SmokeOrigin/Smokepointleft").GetComponent<SmokeAttack>();
        // smokeAttackRight = transform.Find("Desk/SmokeMachineRight/SmokeOrigin/Smokepointright").GetComponent<SmokeAttack>();
        shockwave = transform.Find("Shockwave").GetComponent<ShockwaveScript>();
        bossDodge = transform.Find("Hitbox").GetComponent<BossDodge>();

        audioSource = GetComponent<AudioSource>();

        StartCoroutine(BossBehaviour());
    }

    private void Update() {
        if (isMoving) {

            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
            transform.position = newPosition;

            if (Mathf.Approximately(transform.position.x, targetX)) {
                isMoving = false;
            }
        }
    }

    public void newInterval() {
        currentStage++;
    }

    private void PlayDJYellingSound() {
        if (audioSource && audioSource.clip) {
            audioSource.Play();
        }
    }


    private void ChooseNewPosition() {
        targetX = UnityEngine.Random.Range(minX, maxX);
    }

    private IEnumerator BossBehaviour() {

        
        while (true) {
            
            float standDuration = UnityEngine.Random.Range(minPauseTime, maxPauseTime);

            yield return new WaitForSeconds(standDuration);

            CallRandomAttack();

            yield return new WaitForSeconds(standDuration);

            
            ChooseNewPosition();
            isMoving = true;
            while (isMoving) {
                yield return null;
            }
            

        }
    }
/*
    private void SpawnEnemySpawner() {
        for (int i = 0; i < 2; i++) { // 2 because we spawn two spawners.
            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(roomMin.x, roomMax.x), 0, UnityEngine.Random.Range(roomMin.y, roomMax.y));
            Instantiate(spawnerPrefab, spawnPosition, Quaternion.identity);
        }
    }
*/
    private void CallRandomAttack() {
        int randomAttack = UnityEngine.Random.Range(0, currentStage + 1);
        switch (randomAttack) {
            // Min 0-1
            case 0:
                Debug.Log("Doing nothing");
                break;
            // Min 1-2
            case 1:
                Debug.Log("Shockwave attack");
                shockwave.StartShockwave();
                break;
            // Min 2-3
            case 2:
                smokeAttackLeft.EmitSmoke();
                //smokeAttackRight.EmitSmoke();
                break;
            // Min 3-4
            case 3:
                // SpawnEnemySpawner();
                PlayDJYellingSound();
                break;
            // Min 4-5
            case 4:
                bossDodge.Dodge();
                break;


        }
    }

}

