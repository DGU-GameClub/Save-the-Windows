using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower20RecycleBin : TowerUnit
{
    public GameObject TowerBullet;
    public GameObject TowerPrefeb;
    private float AttackTime = 0f;
    public float bulletScale_X = 0.5f;
    public float bulletScale_Y = 0.5f;
    public Sprite Trash;
    public int BossAttack = 10;
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
            FindTarget();
            AttackTime = 0.0f;
            Attack();

        }

    }
    void Attack()
    {
        var Bullet = Instantiate(TowerBullet, transform.position, Quaternion.identity, TowerPrefeb.transform);
        Bullet.GetComponent<Tower20Bullet>().Tower = gameObject;
        if (AttackEnemy.CompareTag("Boss")) {
            AttackEnemy.GetComponent<Enemy>().TakeDamage(BossAttack);
            return;
        }
        AttackEnemy.GetComponent<Enemy>().StopMove();
        AttackEnemy.tag = "Trash";
        AttackEnemy.GetComponent<SpriteRenderer>().sprite = Trash;
        StartCoroutine(PullObj());
        
    }
    IEnumerator PullObj() {
        float percent = 0f;
        Vector3 originPosition = AttackEnemy.transform.position;
        Vector3 targetPosition = transform.position;

        while (percent < 1f)
        {
            percent += Time.deltaTime * 0.5f;
            if (AttackEnemy == null) yield break;
            AttackEnemy.transform.position = Vector3.Lerp(originPosition, targetPosition, percent);
            yield return null;
        }
    }
}
