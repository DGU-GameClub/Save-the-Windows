using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower8PPT : TowerUnit
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower")) {
            collision.gameObject.GetComponentInChildren<TowerUnit>().AttackUp(Attak);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            collision.gameObject.GetComponentInChildren<TowerUnit>().InitAttack();
        }
    }
}
