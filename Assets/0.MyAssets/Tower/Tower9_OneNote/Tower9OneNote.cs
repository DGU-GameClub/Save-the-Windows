using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower9OneNote : TowerUnit
{
    List<GameObject> Effectobjs = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower")) {
            Effectobjs.Add(collision.gameObject);
            collision.gameObject.GetComponentInChildren<TowerUnit>().AttackSpeedUp(Attack);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            Effectobjs.Remove(collision.gameObject);
            collision.gameObject.GetComponentInChildren<TowerUnit>().InitAttackSpeed(Attack);
        }
    }
    private void OnDestroy()
    {
        for (int i = 0; i < Effectobjs.Count; i++)
        {
            if(Effectobjs[i] != null)
                Effectobjs[i].GetComponentInChildren<TowerUnit>().InitAttackSpeed(Attack);
        }
    }
    protected override void StatusUp()
    {
        if (TowerLevel == 2)
        {
            StartCoroutine(OnOffColloder(0.2f));
        }
        else if (TowerLevel == 3)
        {
            StartCoroutine(OnOffColloder(0.3f));
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
