using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    public static GameManagers instance;
    public int Money { set; get; }
    public int Life;
    public UIManager UIManager;
    public Spawner Spawner;
    public int TowerNumber = 0;
    public bool SellMode = false;
    public SellManager _sellManager;

    public int probability10 = 45; // 10원 타워의 확률
    public int probability20 = 30; // 20원 타워의 확률
    public int probability30 = 20; // 30원 타워의 확률
    public int probability40 = 5;  // 40원 타워의 확률
    public int probability50 = 0;  // 50원 타워의 확률

    bool isEnd;
    int Stageindex = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    void Start()
    {
        Money = 100;
        Life = 10;
        _sellManager = GameObject.Find("SellCanvas").GetComponent<SellManager>();
        _sellManager.TutorialStart();
        isEnd = false;
        SoundManager.instance.StopBGM();
        SoundManager.instance.PlayBGM("GameStart", false, 1f);
    }

    // Update is called once per frame
    public void AddMoney(int price) {
        Money += price;
    }

    public void DamageLife() {
        Life -= 1;
        if (Life <= 0 && !isEnd) {
            //게임 오버
            int tmp = GameManagers.instance.Life;
            StartCoroutine(UIManager.instance.GameOver(tmp));
            isEnd = true;
            SoundManager.instance.StopBGM();
            SoundManager.instance.PlayBGM("GameOver", false, 1f);
        }
    }
    public void RecoveryLife(int rate) {
        Life += rate;
        if (Life >= 20) Life = 20;
    }
    public void TowerV3Ability() {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("TowerV3");
        if (gameObjects.Length == 0) return;
        foreach (GameObject obj in gameObjects) {
            obj.GetComponentInChildren<Tower16V3>().LifeUp();
        }
    }
    public void TowerAvastAbility()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("TowerAvast");
        if (gameObjects.Length == 0) return;
        foreach (GameObject obj in gameObjects)
        {
            obj.GetComponentInChildren<Tower17Avast>().GetMoney();
        }
    }
    public void TowerNotepadAbility()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("TowerNotepad");
        if (gameObjects.Length == 0) return;
        foreach (GameObject obj in gameObjects)
        {
            if (obj.GetComponent<Tower23Spawn>().isCreate)
                obj.GetComponentInChildren<Tower23Notepad>().RandomTowerSpawn();
        }
    }
    public void TowerNotepadAbilityOff()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("TowerNotepad");
        if (gameObjects.Length == 0) return;
        foreach (GameObject obj in gameObjects)
        {
            obj.GetComponentInChildren<Tower23Notepad>().TowerDestory();
        }
    }
    public void InitTower() {
        GameObject[] Towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject obj in Towers) {
            obj.GetComponentInChildren<TowerUnit>().InitAttackTime();
        }
        GameObject[] Towerss = GameObject.FindGameObjectsWithTag("TowerRecycleBin");
        foreach (GameObject obj in Towerss)
        {
            obj.GetComponentInChildren<TowerUnit>().InitAttackTime();
        }
        GameObject[] Towersss = GameObject.FindGameObjectsWithTag("TowerMypc");
        foreach (GameObject obj in Towersss)
        {
            obj.GetComponentInChildren<TowerUnit>().InitAttackTime();
        }
    }
    public void AddTowerNumber() {
        TowerNumber++;
        _sellManager.UpdateHealthBarUI();
    }
    public void RemoveTowerNumber() {
        TowerNumber--;
        _sellManager.UpdateHealthBarUI();
    }
    public int GetTowerNumber() {
        return TowerNumber;
    }

    public void UpdateProbability(int waveindxt) {
        switch (waveindxt) {
            case 10:
                probability10 = 35;
                probability20 = 25;
                probability30 = 25;
                probability40 = 10;
                probability50 = 5;
                break;
            case 20:
                probability10 = 30;
                probability20 = 25;
                probability30 = 20;
                probability40 = 15;
                probability50 = 10;
                break;
            case 25:
                probability10 = 25;
                probability20 = 25;
                probability30 = 20;
                probability40 = 20;
                probability50 = 10;
                break;
        }
    }
    public GameObject GetMostKillTower() {
        GameObject[] Towers = GameObject.FindGameObjectsWithTag("Tower");
        if (Towers.Length == 0) return null;
        int MostKillNumber = 0;
        GameObject MostKillTower = null;
        foreach (GameObject obj in Towers)
        {
            int tempKillNumber = obj.GetComponentInChildren<TowerUnit>().KillNumber;
            if (tempKillNumber > MostKillNumber) {
                MostKillNumber = tempKillNumber;
                MostKillTower = obj;
            }
        }
        Towers = GameObject.FindGameObjectsWithTag("TowerRecycleBin");
        foreach (GameObject obj in Towers)
        {
            int tempKillNumber = obj.GetComponentInChildren<TowerUnit>().KillNumber;
            if (tempKillNumber > MostKillNumber)
            {
                MostKillNumber = tempKillNumber;
                MostKillTower = obj;
            }
        }
        return MostKillTower;
    }
    public void BossStageOn(int i) {
        switch (i) {
            case 5:
                _sellManager.BossStageStart(0);
                SoundManager.instance.StopBGM();
                SoundManager.instance.PlayBGM("Boss", true, 0.5f);
                break;
            case 10:
                _sellManager.BossStageStart(1);
                SoundManager.instance.StopBGM();
                SoundManager.instance.PlayBGM("Boss", true, 0.5f);
                break;
            case 15:
                _sellManager.BossStageStart(2);
                SoundManager.instance.StopBGM();
                SoundManager.instance.PlayBGM("Boss", true, 0.5f);
                break;
            case 20:
                _sellManager.BossStageStart(3);
                SoundManager.instance.StopBGM();
                SoundManager.instance.PlayBGM("Boss", true, 0.5f);
                break;
            case 25:
                _sellManager.BossStageStart(4);
                SoundManager.instance.StopBGM();
                SoundManager.instance.PlayBGM("Boss", true, 0.5f);
                break;
            case 30:
                _sellManager.BossStageStart(5);
                SoundManager.instance.StopBGM();
                SoundManager.instance.PlayBGM("Boss", true, 0.5f);
                break;
            default:
                if(_sellManager.isBossUI) _sellManager.BossSpawnUI();
                _sellManager.BossStageEnd();
                if (Stageindex % 5 == 0 && Stageindex > 0) {
                    SoundManager.instance.StopBGM();
                    SoundManager.instance.PlayBGM("NonBoss", true, 0.5f);
                }
                break;
        }
        Stageindex = i;
    }

    public void LastWave()
    {
        StartCoroutine(UIManager.instance.GameWin());
    }
}
