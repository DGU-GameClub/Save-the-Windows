using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower19Cmd : TowerUnit
{
    GameObject UpgradeTower;
    public void SetupCMD()
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
        UpgradeTower.GetComponentInChildren<TowerUnit>().AttackSpeedUp((Attack-1));
        UpgradeTower.GetComponentInChildren<TowerUnit>().AttackUp(Attack);

    }
    
    private void OnDestroy()
    {
        if (UpgradeTower != null) { 
            UpgradeTower.GetComponentInChildren<TowerUnit>().InitAttack(Attack);
            UpgradeTower.GetComponentInChildren<TowerUnit>().InitAttackSpeed(Attack-1);
        }
    }
    protected override void StatusUp()
    {
        if (TowerLevel == 2)
        {
            if (UpgradeTower != null)
            {
                UpgradeTower.GetComponentInChildren<TowerUnit>().InitAttack(Attack);
                UpgradeTower.GetComponentInChildren<TowerUnit>().InitAttackSpeed(Attack-1);
                PrimitiveAttack = 1.4f;
                Attack = 1.4f;
                UpgradeTower.GetComponentInChildren<TowerUnit>().AttackSpeedUp((Attack - 1));
                UpgradeTower.GetComponentInChildren<TowerUnit>().AttackUp(Attack);
            }
            else {
                PrimitiveAttack = 1.4f;
                Attack = 1.4f;
                SetupCMD();
            }
        }
        else if (TowerLevel == 3)
        {
            if (UpgradeTower != null)
            {
                UpgradeTower.GetComponentInChildren<TowerUnit>().InitAttack(Attack);
                UpgradeTower.GetComponentInChildren<TowerUnit>().InitAttackSpeed(Attack - 1);
                PrimitiveAttack = 1.5f;
                Attack = 1.5f;
                UpgradeTower.GetComponentInChildren<TowerUnit>().AttackSpeedUp((Attack - 1));
                UpgradeTower.GetComponentInChildren<TowerUnit>().AttackUp(Attack);
            }
            else
            {
                PrimitiveAttack = 1.5f;
                Attack = 1.5f;
                SetupCMD();
            }
        }
    }
    public override void EffectOn() {
        if (UpgradeTower == null) return;
        Instantiate(Effectobj, UpgradeTower.transform);
    }
}
