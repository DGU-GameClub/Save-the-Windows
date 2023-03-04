using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower15Bullet : MonoBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    public GameObject ExplosionParticle = null;
    public float Speed = 3.0f;
    public float Pdistance = 5f;
    public float Percent = 1.5f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += targetPosition * Time.deltaTime * Speed;

        // 나와 부모의 사이가 일정거리(1.5f) 도달하면 삭제
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
            if (collision.gameObject.GetComponent<Enemy>().health - gameObject.GetComponentInParent<TowerUnit>().Attak <= 0) {
                /*
                 collision.gameObject.GetComponent<Enemy>().UpPrice(Percent);
                 */
            }
            collision.gameObject.GetComponent<Enemy>().TakeDamage(gameObject.GetComponentInParent<TowerUnit>().Attak);
            Destroy(gameObject);
        }
    }
    
    /*public int Price = 0;
    public void UpPrice(float Percent) {
        Price = (int)(Price  * Percent);
    }*/
}
