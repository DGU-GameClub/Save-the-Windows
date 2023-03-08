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
    public string Contents;        //Ÿ�� ����
    
    private int TowerLevel = 1;     //���� Ÿ�� ����
    private float PrimitiveAttack;
    private float PrimitiveCooldown;
    public GameObject AttackEnemy; //���� ������ ���

    private List<GameObject> EnemyOfRange;  //�ݶ��̴� �ȿ� ���� Enemy ������Ʈ��(���ݴ��)
    // Start is called before the first frame update
    void Start()
    {
        EnemyOfRange = new();
        AttackEnemy = null;
        PrimitiveAttack = Attak;
        PrimitiveCooldown = Cooldown;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyOfRange.Add(other.gameObject);
        }
    }
    //Ÿ�� ���� ������ ����� ��, ���ݴ�� �ִ� ���̶�� ���ݴ�󿡼� ����.
    private void OnTriggerExit2D(Collider2D other)
    {
        foreach(GameObject obj in EnemyOfRange)
        {
            if (obj == other.gameObject) {
                EnemyOfRange.Remove(obj);
                if (AttackEnemy != null && obj != null && AttackEnemy.Equals(obj))
                    AttackEnemy = null;
                break;
            }
        }
    }
    //���ݴ�� �ִ� ���� ���� ����� ���� Ÿ����.
    public GameObject FindDistanceObj() {
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

    public bool CheckTheNullEnemy() {
        if (EnemyOfRange.Count == 0) { AttackEnemy = null; return true; }
        return false;
    }
    //���� ���ݴ���� ���ų� ���ݴ���� ������ ��� ��� �Ǵ� ����� ��� ���� ����� �� Ÿ����
    public void FindTarget() {
        if (AttackEnemy == null || !EnemyOfRange.Contains(AttackEnemy))
        {
            AttackEnemy = FindDistanceObj();
        }
    }
    public void AttackUp(float Enhance) {
        Attak *= Enhance;
    }
    public void AttackSpeedUp(float Enhance)
    {
        Cooldown -= (Cooldown * Enhance);
    }
    public void InitAttack() {
        Attak = PrimitiveAttack;
    }
    public void InitAttackSpeed() {
        Cooldown = PrimitiveCooldown;
    }
}
