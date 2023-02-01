using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnit : MonoBehaviour
{
    [SerializeField]
    private string UnitName;        //타워 이름
    [SerializeField]
    private float Attak;            //공격력
    [SerializeField]
    private float CoolTime;         //공격 쿨타임
    [SerializeField]
    private int UnitPrice;          //타워 가격
    [SerializeField]
    private string Contents;        //타워 설명
    
    private int TowerLevel = 1;     //현재 타워 레벨

    private GameObject AttackEnemy; //현재 공격할 대상

    private List<GameObject> EnemyOfRange;  //콜라이더 안에 들어온 Enemy 오브젝트들(공격대상)
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
        //현재 공격대상이 없거나 공격대상이 범위를 벗어난 경우 또는 사망한 경우 가장 가까운 적 타켓팅
        if(AttackEnemy == null|| !EnemyOfRange.Contains(AttackEnemy))
            AttackEnemy = FindDistanceObj();

    }
    //타워 공격 범위에 들어왔을때, 적이라면 공격대상에 추가.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            EnemyOfRange.Add(other.gameObject);
        }
    }
    //타워 공격 범위에 벗어났을 때, 공격대상에 있던 적이라면 공격대상에서 제거.
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
    //공격대상에 있는 적중 가장 가까운 적을 타겟팅.

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
