using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnit : MonoBehaviour
{
    [SerializeField]
    public string UnitName;        //타워 이름
    [SerializeField]
    public float Attack;            //공격력
    public float[] NextAttack;
    [SerializeField]
    public float Cooldown;         //공격 쿨타임
    [SerializeField]
    public int UnitPrice;          //타워 가격
    [SerializeField]
    public string Contents;        //타워 설명
    protected float AttackTime = 0f;
    public string Synergy1;        //타워 시너지1
    public string Synergy2;        //타워 시너지2
    public SpriteRenderer TowerImage;
    public GameObject Effectobj;

    public int TowerLevel = 1;     //현재 타워 레벨
    private int curExp;
    private int[] MaxExp; 
    protected float PrimitiveAttack;
    protected float PrimitiveCooldown;
    public GameObject AttackEnemy; //현재 공격할 대상
    public int KillNumber = 0;
    private List<GameObject> EnemyOfRange;  //콜라이더 안에 들어온 Enemy 오브젝트들(공격대상)

    public AudioSource LevelUpSound;
    public Animator LevelUpAnim;

    GameObject[] LevelStar = new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {
        EnemyOfRange = new();
        AttackEnemy = null;
        PrimitiveAttack = Attack;
        PrimitiveCooldown = Cooldown;
        TowerLevel = 1;
        MaxExp = new int[3];
        curExp = 0;
        MaxExp[0] = 10;  //4개 먹이면 1렙업
        MaxExp[1] = 23; //7개 먹이면 2렙업
        MaxExp[2] = 10000;
        LevelStar[0] = transform.GetChild(0).gameObject;
        LevelStar[1] = transform.GetChild(1).gameObject;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("TowerRecycleBin")) {
            if (other.gameObject.CompareTag("Enemy"))
                EnemyOfRange.Add(other.gameObject);
            return;
        }
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            EnemyOfRange.Add(other.gameObject);
        }
    }
    //타워 공격 범위에 벗어났을 때, 공격대상에 있던 적이라면 공격대상에서 제거.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (EnemyOfRange.Count <= 0) return;
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
    //공격대상에 있는 적중 가장 가까운 적을 타겟팅.
    public GameObject FindDistanceObj()
    {
        if (EnemyOfRange.Count == 0) { return null; }

        GameObject closestEnemy = null;
        float minDistance = float.MaxValue;

        // Use a for loop to allow safe removal of objects from the list during iteration
        for (int i = EnemyOfRange.Count - 1; i >= 0; i--)
        {
            GameObject enemy = EnemyOfRange[i];

            // If the enemy is null, remove it from the list and continue to the next enemy
            if (enemy == null)
            {
                EnemyOfRange.RemoveAt(i);
                continue;
            }

            // Calculate the distance from the tower to the current enemy
            float distance = (transform.position - enemy.transform.position).sqrMagnitude;

            // If the current enemy is closer than the currently closest enemy, update the closest enemy and minimum distance
            if (distance < minDistance)
            {
                closestEnemy = enemy;
                minDistance = distance;
            }
        }

        return closestEnemy;
    }

    public bool CheckTheNullEnemy() {
        if (EnemyOfRange.Count == 0) { AttackEnemy = null; return true; }
        return false;
    }
    //현재 공격대상이 없거나 공격대상이 범위를 벗어난 경우 또는 사망한 경우 가장 가까운 적 타켓팅
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
        if (Cooldown <= 0.5f) Cooldown = 0.5f;
    }
    public void InitAttack(float Enhance) {
        Attack /= Enhance;
    }
    public void InitAttackSpeed(float Enhance) {
        Cooldown /= (1 - Enhance);
    }
    public bool AddExp(GameObject obj) {
        if (!obj.GetComponentInChildren<TowerUnit>().UnitName.Equals(UnitName)) 
            return false;
        if (TowerLevel >= 3)
            return false;
        curExp += 3;
        LevelUpSound.Play();
        CheckLevelUp();
        Destroy(obj);
        return true;
    }
    private void CheckLevelUp()
    {
        if (curExp >= MaxExp[TowerLevel - 1]) {
            int tempLevel = TowerLevel++;
            if (tempLevel != TowerLevel) LevelUpAnim.SetTrigger("LevelUp");

            if (TowerLevel >= 3)
            {
                curExp = 0;
                LevelStar[1].SetActive(true);
                LevelStar[0].SetActive(false);
            }
            else
            {
                curExp -= MaxExp[TowerLevel - 2];
                LevelStar[0].SetActive(true);
                LevelStar[1].SetActive(false);
            }
            StatusUp();     
        }
    }
    virtual protected void StatusUp()
    {
        if (TowerLevel == 2)
        {
            float tempPercent = 0f;
            if (Attack != PrimitiveAttack) tempPercent = Attack / PrimitiveAttack;
            PrimitiveAttack = NextAttack[0];
            Attack = PrimitiveAttack;
            if (tempPercent > 0f) AttackUp(tempPercent);
        }
        else if (TowerLevel == 3) {
            float tempPercent = 0f;
            if (Attack != PrimitiveAttack) tempPercent = Attack / PrimitiveAttack;
            PrimitiveAttack = NextAttack[1];
            Attack = PrimitiveAttack;
            if (tempPercent > 0f) AttackUp(tempPercent);
        }
    }
    public float ExpPercent() {
        return (float)curExp / (float)MaxExp[TowerLevel - 1];
    }
    public void InitAttackTime() {
        AttackTime = 0f;
    }
    public virtual void EffectOn() {
        if (Effectobj != null)
            Effectobj.SetActive(true);
    }
}
