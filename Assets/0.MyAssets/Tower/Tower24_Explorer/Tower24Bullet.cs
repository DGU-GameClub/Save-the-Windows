using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower24Bullet : TowerBulletSlow
{
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Speed * Time.deltaTime * targetPosition;

        // 나와 부모의 사이가 일정거리(1.5f) 도달하면 삭제
        float distance = Vector3.Distance(transform.position, transform.parent.position);
        if (distance > Pdistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            float dam = Tower.GetComponent<TowerUnit>().Attack;
            if (collision.gameObject == null) return;
            if (collision.CompareTag("Enemy")) dam += collision.gameObject.GetComponent<Enemy>().health * Tower.GetComponent<Tower24Explorer>().special;
            if (collision.gameObject == null) return;
            if ((collision.gameObject.GetComponent<Enemy>().health - dam) <= 0)
            {
                Tower.GetComponent<TowerUnit>().KillNumber++;
            }
            if (collision.gameObject == null) return;
            collision.gameObject.GetComponent<Enemy>().TakeDamage(dam);
            Destroy(gameObject);
        }
    }
}
