using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower7Excel : TowerUnit
{
    public GameObject TowerBullet;
    public GameObject TowerPrefeb;
    private float AttackTime = 0f;
    public float bulletScale_X = 0f;
    public float bulletScale_Y = 0f;

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
        var Target = (AttackEnemy.transform.position - transform.position);
        float angle = Mathf.Atan2(Target.y, Target.x) * Mathf.Rad2Deg;
        var Bullet = Instantiate(TowerBullet, transform.position, Quaternion.AngleAxis(angle - 90, Vector3.forward), TowerPrefeb.transform);
        Bullet.GetComponent<Tower7Bullet>().targetPosition = Target.normalized;
        Bullet.transform.localScale = new Vector3(bulletScale_X, bulletScale_Y);
    }
}
