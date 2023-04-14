using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower23Notepad : TowerUnit
{
    public GameObject Notepad;
    public GameObject[] Towers = new GameObject[20];
    private GameObject SpawnTower;
    // Start is called before the first frame update
    private void NotepadOn() {
        Notepad.SetActive(true);
    }
    private void NotepadOff() {
        Notepad.SetActive(false);
    }
    public void RandomTowerSpawn() { 
        int RandomNum = Random.Range(0, Towers.Length);
        gameObject.GetComponent<Tower23Notepad>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        SpawnTower = Instantiate(Towers[RandomNum], gameObject.transform);
        SpawnTower.GetComponent<TowerSpawn>().isCreate = true;
        NotepadOff();
    }
    public void TowerDestory()
    {
        if (SpawnTower != null)
        { 
            Destroy(SpawnTower);
            gameObject.GetComponent<Tower23Notepad>().enabled = true;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            NotepadOn();
        }
    }
    protected override void StatusUp()
    {
        if (TowerLevel == 2)
        {
            return;
        }
        else if (TowerLevel == 3)
        {
            return;
        }
    }

}
