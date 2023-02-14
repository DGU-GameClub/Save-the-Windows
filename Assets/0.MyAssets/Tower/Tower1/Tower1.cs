using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1 : TowerUnit
{
    public GameObject T1B;
    public GameObject Tower1prf;
    private float AttackTime = 0f;

    private void Awake()
    {
        AttackTime = Cooldown;
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
    void Attack()
    {
        var Bullet = Instantiate(T1B, transform.position, Quaternion.identity, Tower1prf.transform);
        Bullet.GetComponent<Tower1Bullet>().targetPosition = (AttackEnemy.transform.position - transform.position).normalized;
        Bullet.transform.localScale = new Vector3(0.5f, 0.5f);
    }
}