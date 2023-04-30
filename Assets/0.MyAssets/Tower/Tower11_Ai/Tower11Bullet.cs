using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower11Bullet : TowerBullet
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += targetPosition * Time.deltaTime * Speed;

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
            if ((collision.gameObject.GetComponent<Enemy>().health - Tower.GetComponent<TowerUnit>().Attack) <= 0)
            {
                int random = Random.Range(3, 5);
                GameManagers.instance.AddMoney(random);
            }
            collision.gameObject.GetComponent<Enemy>().TakeDamage(Tower.GetComponent<TowerUnit>().Attack);
            Destroy(gameObject);
        }
    }
}