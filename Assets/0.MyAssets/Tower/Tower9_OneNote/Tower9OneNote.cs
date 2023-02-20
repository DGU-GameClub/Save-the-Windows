using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower9OneNote : MonoBehaviour
{
    [SerializeField]
    private string UnitName;        //Ÿ�� �̸�
    [SerializeField]
    public float Attak;            //���ݷ�
    [SerializeField]
    public float Cooldown;         //���� ��Ÿ��
    [SerializeField]
    public int UnitPrice;          //Ÿ�� ����
    [SerializeField]
    public string Contents;        //Ÿ�� ����

    private int TowerLevel = 1;     //���� Ÿ�� ����
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
            collision.gameObject.GetComponentInChildren<TowerUnit>().AttackSpeedUp(Attak);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            collision.gameObject.GetComponentInChildren<TowerUnit>().InitAttackSpeed();
        }
    }
}
