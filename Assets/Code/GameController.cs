using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    // Outlets
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

    // Configuration
    public float maxEnemyDelay = 2f;
    public float minEnemyDelay = 2f;

    // State Tracking
    public float timeElapsed;
    public float enemyDelay;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine("EnemySpawnTimer");
    }

    void Update()
    {
        // Increment passage of time for each frame of the game
        timeElapsed += Time.deltaTime;

        // Compute Enemy Delay
        float decreaseDelayOverTime = maxEnemyDelay - ((maxEnemyDelay - minEnemyDelay) / 30f * timeElapsed);
        enemyDelay = Mathf.Clamp(decreaseDelayOverTime, minEnemyDelay, maxEnemyDelay);
    }

    void SpawnEnemy()
    {
        // Pick random spawn points and random enemy prefab
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawnPoint = spawnPoints[randomSpawnIndex];
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject randomEnemyPrefab = enemyPrefabs[randomEnemyIndex];

        // Spawn
        Instantiate(randomEnemyPrefab, randomSpawnPoint.position, Quaternion.identity);
    }

    IEnumerator EnemySpawnTimer()
    {
        // Wait
        yield return new WaitForSeconds(enemyDelay);

        // Spawn
        SpawnEnemy();

        // Repeat
        StartCoroutine("EnemySpawnTimer");
    }
}
