using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower21Bullet : TowerBulletParalysis
{

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,0.1f);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.gameObject == null) return;
            collision.gameObject.GetComponent<Enemy>().ApplySpecial("Paralysis", gameObject);
        }
    }
}
