using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnit : MonoBehaviour
{
    [SerializeField]
    public string UnitName;        //Ÿ�� �̸�
    [SerializeField]
    public float Attack;            //���ݷ�
    [SerializeField]
    public float Cooldown;         //���� ��Ÿ��
    [SerializeField]
    public int UnitPrice;          //Ÿ�� ����
    [SerializeField]
    public string Contents;        //Ÿ�� ����

    public string Synergy1;        //Ÿ�� �ó���1
    public string Synergy2;        //Ÿ�� �ó���2
    public SpriteRenderer TowerImage;

    public int TowerLevel;     //���� Ÿ�� ����
    private int curExp;
    private int[] MaxExp; 
    private float PrimitiveAttack;
    private float PrimitiveCooldown;
    public GameObject AttackEnemy; //���� ������ ���

    private List<GameObject> EnemyOfRange;  //�ݶ��̴� �ȿ� ���� Enemy ������Ʈ��(���ݴ��)
    
    // Start is called before the first frame update
    void Start()
    {
        EnemyOfRange = new();
        AttackEnemy = null;
        PrimitiveAttack = Attack;
        PrimitiveCooldown = Cooldown;
        TowerLevel = 1;
        MaxExp = new int[2];
        curExp = 0;
        MaxExp[0] = 8;  //3�� ���̸� 1����
        MaxExp[1] = 19; //6�� ���̸� 2����
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
        Attack *= Enhance;
    }
    public void AttackSpeedUp(float Enhance)
    {
        Cooldown -= (Cooldown * Enhance);
        if (Cooldown <= 1f) Cooldown = 1f;
    }
    public void InitAttack() {
        Attack = PrimitiveAttack;
    }
    public void InitAttackSpeed() {
        Cooldown = PrimitiveCooldown;
    }
    public void AddExp(GameObject obj) {
        if (!obj.GetComponentInChildren<Tower>().UnitName.Equals(UnitName)) 
            return;
        if (TowerLevel >= 3)
            return;
        curExp += 3;
        CheckLevelUp();
        Destroy(obj);
    }
    private void CheckLevelUp()
    {
        if (curExp >= MaxExp[TowerLevel - 1]) {
            TowerLevel++;
            curExp -= MaxExp[TowerLevel - 1];
            StatusUp();
        }
    }
    private void StatusUp()
    {
        if (TowerLevel == 2)
        {
            PrimitiveAttack *= 1.2f;
            Attack *= 1.2f;
        }
        else if (TowerLevel == 3) {
            PrimitiveAttack *= 1.4f;
            Attack *= 1.4f;
            Cooldown -= (Cooldown * 0.2f);
            PrimitiveCooldown -= (PrimitiveCooldown * 0.2f);
        }
    }
    
}
