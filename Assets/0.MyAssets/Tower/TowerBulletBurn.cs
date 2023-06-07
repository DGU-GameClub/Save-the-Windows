using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBulletBurn : TowerBullet
{
    public int count = 0;
    public float power = 0f;
    public float time = 0f;

    private void Start()
    {
        power = Tower.GetComponent<TowerUnit>().Attack / 4;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Enemy") || collision.CompareTag("Boss")) && ((AttackEnemy != null && AttackEnemy.Equals(collision.gameObject))))
        {
            if (collision.gameObject == null) return;
            if ((collision.gameObject.GetComponent<Enemy>().health - Tower.GetComponent<TowerUnit>().Attack) <= 0)
            {
                Tower.GetComponent<TowerUnit>().KillNumber++;
            }
            if (collision.gameObject == null) return;
            collision.gameObject.GetComponent<Enemy>().ApplySpecial("Burn", gameObject);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(Tower.GetComponent<TowerUnit>().Attack);
            Destroy(gameObject);
        }
    }
}
