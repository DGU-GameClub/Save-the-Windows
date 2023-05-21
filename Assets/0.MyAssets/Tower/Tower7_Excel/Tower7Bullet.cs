using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower7Bullet : MonoBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    public GameObject ExplosionParticle = null;
    public float Speed = 0.1f;
    bool isArrival = false;
    public float Pdistance = 5f;
    public GameObject Tower = null;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(360f * Time.deltaTime * Vector3.forward);
        if (!isArrival)
        {
            
            transform.position += Speed * Time.deltaTime * targetPosition;
            
            float distance = Vector3.Distance(transform.position, transform.parent.position);
            if (distance >= Pdistance-0.3f)
            {
                isArrival = true;
            }
            
        }
        else if (isArrival) {
            targetPosition = (transform.parent.position - transform.position).normalized;
            transform.position += Speed * Time.deltaTime * targetPosition;
            float distance = Vector3.Distance(transform.position, transform.parent.position);
            if (distance <= 0.3f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            if (collision.gameObject == null) return;
            if ((collision.gameObject.GetComponent<Enemy>().health - Tower.GetComponent<TowerUnit>().Attack) <= 0)
            {
                Tower.GetComponent<TowerUnit>().KillNumber++;
            }
            if (collision.gameObject == null) return;
            collision.gameObject.GetComponent<Enemy>().TakeDamage(Tower.GetComponent<TowerUnit>().Attack);
        }
    }
}
