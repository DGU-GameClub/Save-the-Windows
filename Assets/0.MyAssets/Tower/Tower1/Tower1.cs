using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1 : TowerUnit
{
    public GameObject T1B;
    private float AttackTime = 0f;

    private void Awake()
    {
        AttackTime = Cooldown;
    }
    private void FixedUpdate()
    {
        AttackTime += Time.deltaTime;
        if (CheckTheNullEnemy()) return;
        Debug.Log("적이들어옴");
        FindTarget();
        AttackTime += Time.deltaTime;
        if (AttackTime > Cooldown)
        {
            Debug.Log("쿨이 다 돌았음");
            AttackTime = 0.0f;
            Attack();
        }

    }
    void Attack()
    {
        Debug.Log("공격");
        var Bullet = Instantiate(T1B, transform.position, Quaternion.identity, transform);
        Bullet.GetComponent<Tower1Bullet>().targetPosition = (GetEnemy().transform.position - transform.position).normalized;
        Bullet.transform.localScale = new Vector3(0.5f, 0.5f);
    }
}