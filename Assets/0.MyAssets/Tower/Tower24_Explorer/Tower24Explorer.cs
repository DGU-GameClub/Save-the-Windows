using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower24Explorer : TowerUnit
{
    public GameObject TowerBullet;
    public GameObject TowerPrefeb;
    private float AttackTime = 0f;
    public float bulletScale_X = 0.5f;
    public float bulletScale_Y = 0.5f;
    public Transform[] firePoints;
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
        for (int i = 0; i < 8; i++)
        {
            Vector3 dir = firePoints[i].position - transform.position;
            //float angle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg;
            var Bullet = Instantiate(TowerBullet, transform.position, Quaternion.identity, TowerPrefeb.transform);
            Bullet.GetComponent<Tower24Bullet>().Tower = gameObject;
            Bullet.GetComponent<Tower24Bullet>().targetPosition = dir.normalized;
            Bullet.transform.localScale = new Vector3(bulletScale_X, bulletScale_Y);
        }
    }
}
