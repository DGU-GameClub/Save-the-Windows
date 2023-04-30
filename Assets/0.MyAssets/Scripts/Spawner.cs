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
    public int bossParttern = 3;
    public Wave[] waves;
    public Wave[] bossWaves;

    int waveIndex;
    int bossWaveIndex;
    Wave currentWave;

    SPAWNER_STATE state;

    int enemyRemainingToSpawn;
    int enemyRemainingAlive;
    float nextSpawnTime;
    bool isBoss;

    // Start is called before the first frame update
    void Start()
    {
        bossWaveIndex = 0;
        waveIndex = 0;
        enemyRemainingToSpawn = 0;
        enemyRemainingAlive = 0;
        nextSpawnTime = Time.time;
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
        if (isBoss)
        {
            enemy.Setup(currentWave.sprite, currentWave.moveSpeed, currentWave.heath, wayPoints, currentWave.Price, "Boss");
            enemy.OnDeath += OnBossDeath;
        }
        else
        {
            enemy.Setup(currentWave.sprite, currentWave.moveSpeed, currentWave.heath, wayPoints, currentWave.Price);
            enemy.OnDeath += OnEnemyDeath;
            enemy.transform.parent = transform;
        }

    }

    void OnBossDeath()
    {
        enemyRemainingAlive--;

        if (enemyRemainingAlive == 0)
        {
            state = SPAWNER_STATE.READY;
            isBoss = false;

            //GameManagers.instance.TowerV3Ability();
            //GameManagers.instance.TowerAvastAbility();
            //GameManagers.instance.TowerNotepadAbilityOff();
        }
    }

    void OnEnemyDeath()
    {
        enemyRemainingAlive--;

        if (enemyRemainingAlive == 0)
        {
            state = SPAWNER_STATE.READY;

            if(bossParttern <= 0)
            {
                bossParttern = 1;
            }

            if (isBoss == false && waveIndex % bossParttern == 0)
            {
                isBoss = true;
                BossWave();
            }

            //GameManagers.instance.TowerV3Ability();
            //GameManagers.instance.TowerAvastAbility();
            //GameManagers.instance.TowerNotepadAbilityOff();
            //NextWave();
        }
    }
    public void BossWave()
    {
        if (state == SPAWNER_STATE.START)
        {
            Debug.Log("이미 시작했습니다");
            return;
        }

        if (bossWaveIndex < bossWaves.Length)
        {
            currentWave = bossWaves[bossWaveIndex++];

            enemyRemainingToSpawn = currentWave.enemyCount;
            enemyRemainingAlive = enemyRemainingToSpawn;

            print( "Boss Wave: "  +  bossWaveIndex);

            state = SPAWNER_STATE.START;
            //GameManagers.instance.TowerNotepadAbility();
        }
        nextSpawnTime = Time.time;
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
            currentWave = waves[waveIndex++];

            enemyRemainingToSpawn = currentWave.enemyCount;
            enemyRemainingAlive = enemyRemainingToSpawn;

            print( "Wave: " + waveIndex);

            state = SPAWNER_STATE.START;
            //GameManagers.instance.TowerNotepadAbility();
        }
        nextSpawnTime = Time.time;
        //waveIndex++;
    }

    public int CuurentState() {
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
