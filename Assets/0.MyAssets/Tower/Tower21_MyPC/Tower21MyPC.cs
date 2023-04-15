using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower21MyPC : TowerUnit
{
    public GameObject TowerBullet;
    public GameObject TowerPrefeb;
    public float bulletScale_X = 0.5f;
    public float bulletScale_Y = 0.5f;

    private void Awake()
    {
        AttackTime = 0;
    }
    private void FixedUpdate()
    {
        AttackTime += Time.deltaTime;
        if (CheckTheNullEnemy()) return;
        if (AttackTime > Cooldown)
        {
            FindTarget();
            AttackTime = 0.0f;
            Attack();

        }
    }
    new void Attack()
    {
        Instantiate(TowerBullet, transform.position, Quaternion.identity, TowerPrefeb.transform);
    }
}
