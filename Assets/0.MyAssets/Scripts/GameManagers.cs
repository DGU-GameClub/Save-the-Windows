using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    public static GameManagers instance;
    public int Money { set; get; }
    private int Life;
    public UIManager UIManager;
    public Spawner Spawner;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
        }
        instance = this;
    }
    void Start()
    {
        Money = 100;
        Life = 10;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void GetMoney(int price) {
        Money += price;
    }
    public void DamageLife() {
        Life -= 1;
        if (Life <= 0)
        {
            //게임 오버
            UIManager.GameOverOn();
        }
    }
    public void RecoveryLife(int rate) {
        Life += rate;
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
            obj.GetComponentInChildren<Tower23Notepad>().RandomTowerSpawn();
        }
    }
}
