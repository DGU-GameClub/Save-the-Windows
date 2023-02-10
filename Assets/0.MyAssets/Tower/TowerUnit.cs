using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnit : MonoBehaviour
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

    //Ÿ�� ���� ������ ��������, ���̶�� ���ݴ�� �߰�.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�浹");
        if (other.tag =="Enemy")
        {

            EnemyOfRange.Add(other.gameObject);
        }
    }
    //Ÿ�� ���� ������ ����� ��, ���ݴ�� �ִ� ���̶�� ���ݴ�󿡼� ����.
    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject obj in EnemyOfRange)
        {
            if (obj == other.gameObject)
            {
                EnemyOfRange.Remove(obj);
                if (AttackEnemy.Equals(obj))
                    AttackEnemy = null;
                break;
            }
        }
    }
    //���ݴ�� �ִ� ���� ���� ����� ���� Ÿ����.

    private GameObject FindDistanceObj()
    {
        if (EnemyOfRange.Count == 0) { return null; }
        GameObject Enemy = EnemyOfRange[0];
        float Mindis = 100f;
        foreach (GameObject obj in EnemyOfRange)
        {
            float dis = (gameObject.transform.position - obj.transform.position).sqrMagnitude;
            if (Mindis > dis)
            {
                Enemy = obj;
                Mindis = dis;
            }
        }
        return Enemy;
    }

    public bool CheckTheNullEnemy()
    {
        if (EnemyOfRange.Count == 0) { AttackEnemy = null; return true; }
        return false;
    }
    //���� ���ݴ���� ������ ���� ����� �� Ÿ����
    public void FindTarget()
    {
        if (AttackEnemy == null)
            AttackEnemy = FindDistanceObj();
    }
    public GameObject GetEnemy()
    {
        return AttackEnemy;
    }
}

