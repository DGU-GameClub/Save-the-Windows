using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1Bullet : MonoBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    public GameObject ExplosionParticle = null;
    public float Speed = 3.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(targetPosition * Time.deltaTime * Speed);

        // ���� �θ��� ���̰� �����Ÿ�(1.5f) �����ϸ� ����
        float distance = Vector3.Distance(transform.position, transform.parent.position);
        if (distance > 5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}