using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class EnemySpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyPrefab;
    public Transform targetTranform;
    public Metronome time;
    private float startTime;
    private bool lv1 = false;
    private float actionInterval = 10f;
    private float timer = 0f;
    void Start(){
        Vector3 randomSpawnPos = transform.position;
        GameObject newEnemy = (GameObject)Instantiate(enemyPrefab, randomSpawnPos, transform.rotation);
        Enemy enemy = newEnemy.GetComponent<Enemy>();
        enemy.GetMetronome(time);
    }
    void Update(){
        timer += Time.deltaTime;
        if (timer >= actionInterval){
            Vector3 randomSpawnPos = transform.position;
            GameObject newEnemy = (GameObject)Instantiate(enemyPrefab, randomSpawnPos, transform.rotation);
            Enemy enemy = newEnemy.GetComponent<Enemy>();
            enemy.GetMetronome(time);
            newEnemy.GetComponent<ObjectShaderLight>().GetPlayer(player);
            timer = 0f;
        }

    }
}