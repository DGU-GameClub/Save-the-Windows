using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform wayPoints;
    public Enemy enemyPrefabs;
    public Wave[] waves;

    int waveIndex;
    Wave currentWave;

    int enemyRemainingToSpawn;
    int enemyRemainingAlive;
    float nextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        waveIndex = 0;
        enemyRemainingToSpawn = 0;
        enemyRemainingAlive = 0;
        nextSpawnTime = Time.time;
        NextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyRemainingToSpawn > 0  && Time.time > nextSpawnTime)
        {
            enemyRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.spawnTime;

            StartCoroutine("SpawnEnemy");
        }
    }

    IEnumerator SpawnEnemy()
    {
        float spawnTimer = 0f;

        while (spawnTimer < currentWave.spawnTime)
        {
            spawnTimer += Time.deltaTime;
            yield return null;
        }

        Enemy enemy = Instantiate(enemyPrefabs, Vector3.zero, Quaternion.identity);
        enemy.Setup(currentWave.sprite, currentWave.moveSpeed, currentWave.heath, wayPoints);
        enemy.OnDeath += OnEnemyDeath;
    }

    void OnEnemyDeath()
    {
        enemyRemainingAlive--;

        if (enemyRemainingAlive == 0)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        if(waveIndex < waves.Length)
        {
            currentWave = waves[waveIndex];

            enemyRemainingToSpawn = currentWave.enemyCount;
            enemyRemainingAlive = enemyRemainingToSpawn;

            print("Wave: " + (waveIndex + 1));
        }

        waveIndex++;
    }

    [System.Serializable]
    public class Wave
    {
        public Sprite sprite;
        public int enemyCount;
        public float heath;
        public float moveSpeed;
        public float spawnTime;
    }
}
