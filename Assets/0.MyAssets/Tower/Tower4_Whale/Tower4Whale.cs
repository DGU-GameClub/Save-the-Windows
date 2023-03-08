using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower4Whale : TowerUnit
{
    public GameObject TowerBullet;
    public GameObject TowerPrefeb;
    private float AttackTime = 0f;
    public float bulletScale_X = 0f;
    public float bulletScale_Y = 0f;
    public float Correction;
    public float StopTime;
    private void Awake()
    {
        AttackTime = 0;
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
    IEnumerator Attack() {
        Vector3 EnemyRandomPosition = new Vector3(
            AttackEnemy.transform.position.x + Random.Range(-1 * Correction, Correction),
            AttackEnemy.transform.position.y + Random.Range(-1 * Correction, Correction),
            0);
        for (int i = 0; i < 5; i++)
        {
            var Bullet = Instantiate(TowerBullet, transform.position, Quaternion.identity, TowerPrefeb.transform);
            Bullet.GetComponent<TowerBullet>().Tower = gameObject;
            Bullet.GetComponent<TowerBullet>().targetPosition = (EnemyRandomPosition - transform.position).normalized;
            Bullet.transform.localScale = new Vector3(bulletScale_X, bulletScale_Y);
            yield return new WaitForSeconds(StopTime); 
        }
    }
}
