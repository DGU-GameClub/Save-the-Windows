using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower10_Ps : TowerUnit
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
        Vector3 dir = AttackEnemy.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg;
        var Bullet = Instantiate(TowerBullet, transform.position, Quaternion.AngleAxis(angle,Vector3.forward), TowerPrefeb.transform);
        Bullet.GetComponent<TowerBullet>().Tower = gameObject;
        Bullet.GetComponent<TowerBullet>().targetPosition = dir.normalized;
        Bullet.transform.localScale = new Vector3(bulletScale_X, bulletScale_Y);
    }
}
