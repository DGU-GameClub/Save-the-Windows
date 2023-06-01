using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBulletSlow : TowerBullet
{
    public float time = 0f;
    public float percent = 0f;

    private void Start()
    {
        if (Tower.GetComponent<TowerUnit>().TowerLevel == 2)
            percent = 0.6f;
        else if (Tower.GetComponent<TowerUnit>().TowerLevel == 3)
            percent = 0.5f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Enemy") || collision.CompareTag("Boss")) && ((AttackEnemy != null && AttackEnemy.Equals(collision.gameObject)) || Tower.GetComponent<TowerUnit>().UnitName.Equals("¿þÀÏ")))
        {
            if (collision.gameObject == null) return;
            if ((collision.gameObject.GetComponent<Enemy>().health - Tower.GetComponent<TowerUnit>().Attack) <= 0)
            {
                Tower.GetComponent<TowerUnit>().KillNumber++;
            }
            if (collision.gameObject == null) return;
            collision.gameObject.GetComponent<Enemy>().ApplySpecial("Slow", gameObject);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(Tower.GetComponent<TowerUnit>().Attack);
            Destroy(gameObject);
        }
    }
}
