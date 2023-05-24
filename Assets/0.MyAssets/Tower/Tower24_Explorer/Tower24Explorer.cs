using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower24Explorer : TowerUnit
{
    public GameObject TowerBullet;
    public GameObject TowerPrefeb;
    public float bulletScale_X = 0.5f;
    public float bulletScale_Y = 0.5f;
    public Transform[] firePoints;
    public AudioSource TowerAudio;
    public float special;
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
    new void Attack()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            Vector3 dir = firePoints[i].position - transform.position;
            //float angle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg;
            var Bullet = Instantiate(TowerBullet, transform.position, Quaternion.identity, TowerPrefeb.transform);
            TowerAudio.Play();
            Bullet.GetComponent<TowerBullet>().Tower = gameObject;
            Bullet.GetComponent<TowerBullet>().targetPosition = dir.normalized;
            Bullet.transform.localScale = new Vector3(bulletScale_X, bulletScale_Y);
        }
    }
    protected override void StatusUp()
    {
        base.StatusUp();
        if (TowerLevel == 2) special = 0.07f;
        else if (TowerLevel == 3) special = 0.15f;
    }
}
