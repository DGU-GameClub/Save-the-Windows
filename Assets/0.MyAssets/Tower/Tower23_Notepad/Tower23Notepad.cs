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

        SpawnTower = Instantiate(Towers[RandomNum], gameObject.transform);
        NotepadOff();
    }
    public void TowerDestory()
    {
        if (SpawnTower != null)
        { 
            Destroy(SpawnTower);
            NotepadOn();
        }
    }
    
}
