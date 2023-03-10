using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower19Cmd : TowerUnit
{
    bool isStop = false;
    GameObject UpgradeTower;
    private void Start()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 1f);
        int index = 0;
        GameObject[] Towers = new GameObject[colls.Length];
        for (int i = 0; i < colls.Length; i++) {
            if (colls[i].CompareTag("Tower"))
            {
                Towers[index] = colls[i].gameObject;
                index++;
            }
        }
        if (index == 0) return;
        int RandomNumber = Random.Range(0, index);
        UpgradeTower = Towers[RandomNumber];
        UpgradeTower.GetComponentInChildren<TowerUnit>().AttackSpeedUp((Attak-1));
        UpgradeTower.GetComponentInChildren<TowerUnit>().AttackUp(Attak);

    }
    
    private void OnDestroy()
    {
        if (UpgradeTower != null) { 
            UpgradeTower.GetComponentInChildren<TowerUnit>().InitAttack();
            UpgradeTower.GetComponentInChildren<TowerUnit>().InitAttackSpeed();
        }
    }
}
