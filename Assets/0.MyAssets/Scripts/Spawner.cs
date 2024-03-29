using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    enum SPAWNER_STATE
    {
        READY,
        START,
    }

    public Transform wayPoints;
    public Enemy enemyPrefabs;
    public int bossParttern = 3;
    public Wave[] waves;
    public Wave[] bossWaves;

    float nextSpawnTime;
    float nextBossSpawnTime;
    public int waveIndex;
    int bossWaveIndex;
    Wave curWave;
    Wave curBossWave;

    SPAWNER_STATE state;

    int enemyRemainingToSpawn;
    int bossRemainingToSpawn;
    int enemyRemainingAlive;
    bool isBoss;

    // Start is called before the first frame update
    void Start()
    {
        bossWaveIndex = 0;
        waveIndex = 0;
        nextSpawnTime = Time.time;
        nextBossSpawnTime = Time.time;
        enemyRemainingToSpawn = 0;
        enemyRemainingAlive = 0;
        isBoss = false;

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
            nextSpawnTime = Time.time + curWave.spawnTime;
            StartCoroutine("SpawnEnemy");
        }

        if (isBoss && bossRemainingToSpawn > 0 && Time.time > nextBossSpawnTime)
        {
            bossRemainingToSpawn--;
            nextBossSpawnTime = Time.time + curBossWave.spawnTime;
            StartCoroutine("SpawnBoss");
        }
    }

    IEnumerator SpawnBoss()
    {
        float spawnTimer = 0f;

        while (spawnTimer < curBossWave.spawnTime)
        {
            spawnTimer += Time.deltaTime;
            yield return null;
        }


        Enemy enemy = Instantiate(enemyPrefabs, Vector3.zero, Quaternion.identity);
        enemy.Setup(curBossWave.sprite, curBossWave.moveSpeed, curBossWave.heath, wayPoints, curBossWave.Price, "Boss");
        enemy.OnDeath += OnEnemyDeath;
        enemy.transform.parent = transform;
    }

    IEnumerator SpawnEnemy()
    {
        float spawnTimer = 0f;

        while (spawnTimer < curWave.spawnTime)
        {
            spawnTimer += Time.deltaTime;
            yield return null;
        }


        Enemy enemy = Instantiate(enemyPrefabs, Vector3.zero, Quaternion.identity);
        enemy.Setup(curWave.sprite, curWave.moveSpeed, curWave.heath + HeathUp(), wayPoints, curWave.Price);
        //enemy.Setup(curWave.sprite, curWave.moveSpeed, curWave.heath , wayPoints, curWave.Price);
        enemy.OnDeath += OnEnemyDeath;
        enemy.transform.parent = transform;
    }

    void OnEnemyDeath()
    {
        enemyRemainingAlive--;

        if (enemyRemainingAlive == 0)
        {
            state = SPAWNER_STATE.READY;

            if (isBoss == true)
            {
                isBoss = false;
            }

            GameManagers.instance.TowerV3Ability();
            GameManagers.instance.TowerAvastAbility();
            GameManagers.instance.TowerNotepadAbilityOff();
            GameManagers.instance.UpdateProbability(waveIndex);

            if (waveIndex == waves.Length) {
                GameManagers.instance.LastWave();
            }
        }
    }

    public void NextWave()
    {
        if (Time.timeScale == 0f) return;
        if (state == SPAWNER_STATE.START)
        {
            return;
        }
        if (waveIndex >= waves.Length)
        {
            return;
        }

        if (bossParttern <= 0)
            bossParttern = 1;

        if (isBoss == false && (waveIndex + 1) % bossParttern == 0)
            isBoss = true;

        curWave = waves[waveIndex++];
        enemyRemainingToSpawn = curWave.enemyCount;
        enemyRemainingAlive = enemyRemainingToSpawn;

        if (isBoss && bossWaveIndex < bossWaves.Length)
        {
            curBossWave = bossWaves[bossWaveIndex++];
            bossRemainingToSpawn = curBossWave.enemyCount;
            enemyRemainingAlive += bossRemainingToSpawn;
        }

        state = SPAWNER_STATE.START;
        nextSpawnTime = Time.time;
        GameManagers.instance.TowerNotepadAbility();
        GameManagers.instance.InitTower();
        GameManagers.instance.BossStageOn(waveIndex);   
    }

    public int CurentState()
    {
        return (int)state;
    }

    public float HeathUp()
    {
        float plus = 0f;
        int TempNumber = GameManagers.instance.GetTowerNumber();

        if (TempNumber >= 5 && TempNumber < 10) plus = 10f;
        else if (TempNumber >= 10 && TempNumber < 15) plus = 30f;
        else if (TempNumber >= 15 && TempNumber < 20) plus = 60f;
        else if (TempNumber >= 20 && TempNumber < 25) plus = 120f;
        else if (TempNumber >= 25) plus = 240f;

        return plus;
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
