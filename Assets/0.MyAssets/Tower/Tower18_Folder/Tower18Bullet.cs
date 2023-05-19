using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower18Bullet : MonoBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    public GameObject ExplosionParticle = null;
    public float Speed = 3.0f;
    public float Pdistance = 5f;
    public float Percent = 1.5f;
    public GameObject Tower = null;
    public AudioSource TowerAudio;
    bool isStop = false;
    public float Area;
    public Sprite NextSprite;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isStop) return;
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
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            isStop = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = NextSprite;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack() { 
        ExplosionParticle.SetActive(true);
        TowerAudio.Play();
        yield return new WaitForSeconds(0.5f);
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, Area);
        for (int i = 0; i < colls.Length; i++) {
            if (colls[i].CompareTag("Enemy") || colls[i].CompareTag("Boss")) {
                if ((colls[i].gameObject.GetComponent<Enemy>().health - Tower.GetComponent<TowerUnit>().Attack) <= 0)
                {
                    Tower.GetComponent<TowerUnit>().KillNumber++;
                }
                colls[i].gameObject.GetComponent<Enemy>().TakeDamage(Tower.GetComponent<TowerUnit>().Attack);
            }
        }
        Destroy(gameObject);
    }
}
