using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    // Outlets
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public TMP_Text textScore;
    public TMP_Text textMoney;
    public TMP_Text textHealth;

    // Configuration
    public float maxEnemyDelay = 2f;
    public float minEnemyDelay = 2f;

    // State Tracking
    public float timeElapsed;
    public float enemyDelay;
    public float missileSpeed = 2f;
    public int score;
    public int money;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine("EnemySpawnTimer");

        score = 0;
    }

    void Update()
    {
        // Increment passage of time for each frame of the game
        timeElapsed += Time.deltaTime;

        // Compute Enemy Delay
        float decreaseDelayOverTime = maxEnemyDelay - ((maxEnemyDelay - minEnemyDelay) / 30f * timeElapsed);
        enemyDelay = Mathf.Clamp(decreaseDelayOverTime, minEnemyDelay, maxEnemyDelay);

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        textScore.text = score.ToString();
        textMoney.text = money.ToString();
        textHealth.text = "Health - " + PlayerMovement.instance.health;
    }

    public void EarnPoints(int pointAmount)
    {
        money += Mathf.RoundToInt(pointAmount + 10);
        score += Mathf.RoundToInt(pointAmount);
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
