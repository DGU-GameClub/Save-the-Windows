using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower2 : TowerUnit
{
    public GameObject T2B;
    public GameObject Tower2prf;
    private float AttackTime = 0f;
    public float Correction;
    public float StopTime;
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
    IEnumerator Attack() {
        Vector3 EnemyRandomPosition = new Vector3(
            AttackEnemy.transform.position.x + Random.Range(-1 * Correction, Correction),
            AttackEnemy.transform.position.y + Random.Range(-1 * Correction, Correction),
            0);
        for (int i = 0; i < 5; i++)
        {
            var Bullet = Instantiate(T2B, transform.position, Quaternion.identity, Tower2prf.transform);
            Bullet.GetComponent<Tower2Bullet>().targetPosition = (EnemyRandomPosition - transform.position).normalized;
            Bullet.transform.localScale = new Vector3(0.2f, 0.2f);
            yield return new WaitForSeconds(StopTime); 
        }
    }
}
