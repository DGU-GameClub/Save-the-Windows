using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    enum SPAWNER_STATE
    {
        READY,
        START,
    }

    public Transform wayPoints;
    public Enemy enemyPrefabs;
    public Wave[] waves;

    int waveIndex;
    Wave currentWave;

    SPAWNER_STATE state;

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

        state = SPAWNER_STATE.READY;
        //NextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SPAWNER_STATE.READY)
            return;

        if (enemyRemainingToSpawn > 0 && Time.time > nextSpawnTime)
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
        enemy.Setup(currentWave.sprite, currentWave.moveSpeed, currentWave.heath, wayPoints, currentWave.Price);
        enemy.OnDeath += OnEnemyDeath;
        enemy.transform.parent = transform;
    }

    void OnEnemyDeath()
    {
        enemyRemainingAlive--;

        if (enemyRemainingAlive == 0)
        {
            state = SPAWNER_STATE.READY;
            GameManagers.instance.TowerV3Ability();
            GameManagers.instance.TowerAvastAbility();
            GameManagers.instance.TowerNotepadAbilityOff();
            //NextWave();
        }
    }

    public void NextWave()
    {
        if (state == SPAWNER_STATE.START)
        {
            Debug.Log("이미 시작했습니다");
            return;
        }

        if (waveIndex < waves.Length)
        {
            currentWave = waves[waveIndex];

            enemyRemainingToSpawn = currentWave.enemyCount;
            enemyRemainingAlive = enemyRemainingToSpawn;

            print("Wave: " + (waveIndex + 1));
            state = SPAWNER_STATE.START;
            GameManagers.instance.TowerNotepadAbility();
        }
        nextSpawnTime = Time.time;
        waveIndex++;
    }
    public int CurrentState() {
        return (int)state;
    }
    [System.Serializable]
    public class Wave
    {
        public Sprite sprite;
        public int enemyCount;
        public float heath;
        public float moveSpeed;
        public float spawnTime;
        public int Price;
    }
}
