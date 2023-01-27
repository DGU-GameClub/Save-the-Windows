using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnit : MonoBehaviour
{
    [SerializeField]
    private string UnitName;        //Ÿ�� �̸�
    [SerializeField]
    private float Attak;            //���ݷ�
    [SerializeField]
    private float CoolTime;         //���� ��Ÿ��
    [SerializeField]
    private int UnitPrice;          //Ÿ�� ����
    [SerializeField]
    private string Contents;        //Ÿ�� ����
    
    private int TowerLevel = 1;     //���� Ÿ�� ����

    private List<GameObject> EnemyOfRange = new();  //�ݶ��̴� �ȿ� ���� Enemy ������Ʈ��(���ݴ��)
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Ÿ�� ���� ������ ��������, ���̶�� ���ݴ�� �߰�.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            EnemyOfRange.Add(other.gameObject);
        }
    }
    //Ÿ�� ���� ������ ����� ��, ���ݴ�� �ִ� ���̶�� ���ݴ�󿡼� ����.
    private void OnTriggerExit(Collider other)
    {
        foreach(GameObject obj in EnemyOfRange)
        {
            if (obj == other.gameObject) {
                EnemyOfRange.Remove(obj);
                break;
            }
        }
    }
}
