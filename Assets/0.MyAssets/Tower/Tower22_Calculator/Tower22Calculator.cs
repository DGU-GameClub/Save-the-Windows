using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower22Calculator : TowerUnit
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().UpPrice(Attack);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().InitPrice();
        }
    }
    protected override void StatusUp()
    {
        if (TowerLevel == 2)
        {
            StartCoroutine(OnOffColloder(1.2f));
        }
        else if (TowerLevel == 3)
        {
            StartCoroutine(OnOffColloder(1.3f));
        }
    }
    IEnumerator OnOffColloder(float Value)
    {
        CircleCollider2D circleCollider = gameObject.GetComponent<CircleCollider2D>();
        Vector3 originalPosition = transform.position;

        circleCollider.enabled = false;
        PrimitiveAttack = Value;
        Attack = Value;
        transform.position = new Vector3(10000f, 10000f, 0f);
        circleCollider.enabled = true;

        yield return new WaitForFixedUpdate();
        transform.position = originalPosition;
    }
}
