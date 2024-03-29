using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower15GoClean : TowerUnit
{
    public GameObject TowerBullet;
    public GameObject TowerPrefeb;
    public float bulletScale_X = 0.5f;
    public float bulletScale_Y = 0.5f;
    public AudioSource TowerAudio;
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
            if (AttackEnemy == null) return;
            AttackTime = 0.0f;
            Attack();

        }

    }
    new void Attack()
    {
        Vector3 dir = AttackEnemy.transform.position - transform.position;
        //float angle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg;
        var Bullet = Instantiate(TowerBullet, transform.position, Quaternion.identity, TowerPrefeb.transform);
        TowerAudio.Play();
        Bullet.GetComponent<Tower15Bullet>().AttackEnemy = AttackEnemy;
        Bullet.GetComponent<Tower15Bullet>().Tower = gameObject;
        Bullet.GetComponent<Tower15Bullet>().targetPosition = dir.normalized;
        Bullet.transform.localScale = new Vector3(bulletScale_X, bulletScale_Y);
    }
}
