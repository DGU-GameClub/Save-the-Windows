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

        // ���� �θ��� ���̰� �����Ÿ�(1.5f) �����ϸ� ����
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
            if (collision.CompareTag("Enemy")) dam += collision.gameObject.GetComponent<Enemy>().health * Tower.GetComponent<Tower24Explorer>().special;
            collision.gameObject.GetComponent<Enemy>().TakeDamage(dam);
            Destroy(gameObject);
        }
    }
}
