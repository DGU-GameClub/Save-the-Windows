using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : TowerUnit
{
    public GameObject TowerBullet;
    public GameObject TowerPrefeb;
    private float AttackTime = 0f;
    public float bulletScale_X = 0.5f;
    public float bulletScale_Y = 0.5f;

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
        var Bullet = Instantiate(TowerBullet, transform.position, Quaternion.identity, TowerPrefeb.transform);
        Bullet.GetComponent<TowerBullet>().targetPosition = (AttackEnemy.transform.position - transform.position).normalized;
        Bullet.transform.localScale = new Vector3(bulletScale_X, bulletScale_Y);
    }
}