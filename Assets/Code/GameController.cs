using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    // Outlets
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public TMP_Text textScore;
    public TMP_Text textMoney;
    public TMP_Text textHealth;
    public TMP_Text levelNumText;

    // Configuration
    public float maxEnemyDelay;
    public float minEnemyDelay;

    // State Tracking
    public float timeElapsed;
    public float enemyDelay;
    public float missileSpeed = 2f;
    public int score;
    public float levelCap = 100f;
    public int money;
    public int levelNum;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine("EnemySpawnTimer");

        score = 0;
        levelNum = 1;
    }

    void Update()
    {
        // Set enemy delay based on level
        if (levelNum == 1)
        {
            maxEnemyDelay = 2f;
            minEnemyDelay = 2f;
        }
        else if (levelNum == 2)
        {
            maxEnemyDelay = 1f;
            minEnemyDelay = 1f;
        }
        else if (levelNum == 3)
        {
            maxEnemyDelay = 0.5f;
            minEnemyDelay = 0.5f;
        }
        else if (levelNum == 4)
        {
            maxEnemyDelay = 0.25f;
            minEnemyDelay = 0.25f;
        }

        // Increment passage of time for each frame of the game
        timeElapsed += Time.deltaTime;

        // Compute Enemy Delay
        float decreaseDelayOverTime = maxEnemyDelay - ((maxEnemyDelay - minEnemyDelay) / 30f * timeElapsed);
        enemyDelay = Mathf.Clamp(decreaseDelayOverTime, minEnemyDelay, maxEnemyDelay);

        // Double the level cap each level
        if(score >= levelCap)
        {
            levelNum += 1;
            levelCap *= 2;
        }

        //Debug.Log(levelNum + ", " + levelCap);

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        textScore.text = score.ToString();
        textMoney.text = money.ToString();
        textHealth.text = "Health - " + PlayerMovement.instance.health;
        levelNumText.text = "Level : " + levelNum.ToString();
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
