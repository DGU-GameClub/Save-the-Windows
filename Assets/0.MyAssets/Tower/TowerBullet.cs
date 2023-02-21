using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    public GameObject ExplosionParticle = null;
    public float Speed = 3.0f;
    public float Pdistance = 5f;
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
        if (collision.CompareTag("Enemy"))
        {
            //������� ���ο� ó��
            Destroy(gameObject);
        }
    }
}