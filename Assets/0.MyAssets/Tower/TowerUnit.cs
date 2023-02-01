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

    private GameObject AttackEnemy; //���� ������ ���

    private List<GameObject> EnemyOfRange;  //�ݶ��̴� �ȿ� ���� Enemy ������Ʈ��(���ݴ��)
    // Start is called before the first frame update
    void Start()
    {
        EnemyOfRange = new();
        AttackEnemy = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (EnemyOfRange.Count == 0) { return; }
        //���� ���ݴ���� ���ų� ���ݴ���� ������ ��� ��� �Ǵ� ����� ��� ���� ����� �� Ÿ����
        if(AttackEnemy == null|| !EnemyOfRange.Contains(AttackEnemy))
            AttackEnemy = FindDistanceObj();

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
    //���ݴ�� �ִ� ���� ���� ����� ���� Ÿ����.

    private GameObject FindDistanceObj() {
        if (EnemyOfRange.Count == 0) { return null; }
        GameObject Enemy = EnemyOfRange[0];
        float Mindis = 100f;
        foreach (GameObject obj in EnemyOfRange) {
            float dis = (gameObject.transform.position - obj.transform.position).sqrMagnitude;
            if (Mindis > dis) {
                Enemy = obj;
                Mindis = dis;
            }
        }
        return Enemy;
    }
}
