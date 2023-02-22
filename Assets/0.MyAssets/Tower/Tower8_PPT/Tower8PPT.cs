using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower8PPT : MonoBehaviour
{
    [SerializeField]
    private string UnitName;        //타워 이름
    [SerializeField]
    public float Attak;            //공격력
    [SerializeField]
    public float Cooldown;         //공격 쿨타임
    [SerializeField]
    public int UnitPrice;          //타워 가격
    [SerializeField]
    public string Contents;        //타워 설명

    private int TowerLevel = 1;     //현재 타워 레벨
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
