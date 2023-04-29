using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower4Whale : TowerUnit
{
    public GameObject TowerBullet;
    public GameObject TowerPrefeb;
    public float bulletScale_X = 0f;
    public float bulletScale_Y = 0f;
    public float Correction;
    public float StopTime;
    public int Special = 5;
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
            AttackEnemy = FindDistanceObj();
            StartCoroutine(Attack());
            AttackTime = 0.0f;
        }

    }
     new IEnumerator Attack() {
        Vector3 EnemyRandomPosition = new Vector3(
            AttackEnemy.transform.position.x + Random.Range(-1 * Correction, Correction),
            AttackEnemy.transform.position.y + Random.Range(-1 * Correction, Correction),
            0);
        for (int i = 0; i < Special; i++)
        {
            var Bullet = Instantiate(TowerBullet, transform.position, Quaternion.identity, TowerPrefeb.transform);
            Bullet.GetComponent<TowerBullet>().Tower = gameObject;
            Bullet.GetComponent<TowerBullet>().targetPosition = (EnemyRandomPosition - transform.position).normalized;
            Bullet.transform.localScale = new Vector3(bulletScale_X, bulletScale_Y);
            yield return new WaitForSeconds(StopTime); 
        }
     }
    protected override void StatusUp() {
        if (TowerLevel == 2) { Special = 7; }
        else if (TowerLevel == 3) { Special = 10; }
    }
}
