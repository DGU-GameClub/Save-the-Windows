using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower3 : TowerUnit
{
    public GameObject T3B;
    public GameObject Tower3prf;
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
        var Target = (AttackEnemy.transform.position - transform.position);
        float angle = Mathf.Atan2(Target.y, Target.x) * Mathf.Rad2Deg;
        var Bullet = Instantiate(T3B, transform.position, Quaternion.AngleAxis(angle - 90, Vector3.forward), Tower3prf.transform);
        Bullet.GetComponent<Tower3Bullet>().targetPosition = Target.normalized;
        Bullet.transform.localScale = new Vector3(3f, 0.5f);
    }
}
