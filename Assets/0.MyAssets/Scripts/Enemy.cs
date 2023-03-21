using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : LivingEntity
{
    //speed
    [System.NonSerialized]
    public float moveSpeed;
    [System.NonSerialized]
    public float originSpeed;
    //tile object
    [System.NonSerialized]  
    public Transform wayPoints;

    public SpecialAttack specialAttack;

    //HpBarUI 추가한 변수
    public GameObject hpBarPrefab; //Instantiate 메서드로 복제할 프리펩을 담을 변수
    public Vector3 hpBarOffset = Vector3.zero;

    GameObject hpBar; //Slider의 초기 세팅, Hp 갱신에 사용할 Slider를 담을 변수
    Image enemyHpBarImage; //Slider의 초기 세팅, Hp 갱신에 사용할 Slider를 담을 변수

    //target transform
    Transform[] targetArr;
    int targetIndex = 0;

    public int Price;
    int OriginPrice;
    bool[] isSpecial = new bool[3];
    // Start is called before the first frame update
    void Start()
    {
        //get taget transform
        List<Transform> targetList = new List<Transform>();
        for (int i = 0; i < wayPoints.childCount; i++)
            targetList.Add(wayPoints.GetChild(i).transform);
        targetArr = targetList.ToArray();

        //set position
        transform.position = targetArr[0].position;

        SetHpBar();
        OnDeath += RemoveHealthBar;

        OriginPrice = Price;
        for(int i =0; i< isSpecial.Length; i++)
            isSpecial[i] = false;

        //move start
        StartCoroutine(MoveTarget());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RemoveHealthBar()
    {
        if(hpBar != null)
        {
            Destroy(hpBar);
        }
    }

    public void Setup(Sprite sprite, float _speed, float _health, Transform _wayPoints, int price)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        moveSpeed = _speed;
        originSpeed = moveSpeed;
        health = _health;
        originHealth = health;
        wayPoints = _wayPoints;
        Price = price;
        OriginPrice = Price;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if(enemyHpBarImage != null)
            enemyHpBarImage.fillAmount = health / originHealth;
    }

    //적 위치 + offset에 HpBarPrefab 생성하기
    void SetHpBar()
    {
        Canvas enemyHpBarCanvas = GameObject.Find("Enemy HpBar Canvas").GetComponent<Canvas>();
        hpBar = Instantiate<GameObject>(hpBarPrefab, enemyHpBarCanvas.transform);

        enemyHpBarImage = hpBar.GetComponent<Image>();
        var _hpbar = hpBar.GetComponent<EnemyHpBar>();
        _hpbar.enemyTr = transform;
        _hpbar.offset = hpBarOffset;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Burn":
                StartCoroutine(BurnDamage());
                break;
            case "Paralysis":
                StartCoroutine(ParalysisDamage());
                break;
            case "Slow":
                StartCoroutine(SlowDamage());
                break;
            default:
                Debug.Log("특수 공격 실패");
                break;
        }
    }

    IEnumerator BurnDamage()
    {
        if (isSpecial[0]) yield break;
        int num = 0;
        int count = specialAttack.burn.count;
        float power = specialAttack.burn.power;
        float time = specialAttack.burn.time;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 0, 0, 1);
        isSpecial[0] = true;
        while (num != count)
        {
            num++;
            TakeDamage(power);
            print(health);
            yield return new WaitForSeconds(time);
        }

        spriteRenderer.color = new Color(1, 1, 1, 1);
        gameObject.tag = "Enemy";
        isSpecial[0] = false;
    }

    IEnumerator ParalysisDamage()
    {
        if (isSpecial[1]) yield break;
        float time = specialAttack.paralysis.time;
        float originSpeed = moveSpeed;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        isSpecial[1] = true;
        moveSpeed = 0f;
        spriteRenderer.color = new Color(1, 0.5f, 0, 1);

        yield return new WaitForSeconds(time);
        
        moveSpeed = originSpeed;
        gameObject.tag = "Enemy";
        isSpecial[1] = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    IEnumerator SlowDamage()
    {
        if (isSpecial[2]) yield break;
        float time = specialAttack.slow.time;
        float percent = specialAttack.slow.percent;
        float originSpeed = moveSpeed;
        isSpecial[2] = true;
        moveSpeed = percent * moveSpeed;

        yield return new WaitForSeconds(time);

        moveSpeed = originSpeed;
        gameObject.tag = "Enemy";
        isSpecial[2] = false;
    }

    IEnumerator MoveTarget()
    {
        float percent = 0f;
        Vector3 originPosition = transform.position;
        Vector3 targetPosition = targetArr[targetIndex++].position;

        while (percent < 1f)
        {
            percent += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(originPosition, targetPosition, percent);
            yield return null;
        }

        if (targetIndex >= targetArr.Length)
        {
            Die();
            GameManagers.instance.DamageLife();
            GameManagers.instance.GetMoney(Price);
        }
        else
        {
            StartCoroutine(MoveTarget());
        }
    }
    public void UpPrice(float Percent)
    {
        Price = (int)(Price * Percent);
    }
    public void InitPrice() {
        Price = OriginPrice;
    }
    public void StopMove() {
        StopAllCoroutines();
    }
    [System.Serializable]
    public class SpecialAttack
    {
        public Burn burn;
        public Paralysis paralysis;
        public Slow slow;

        [System.Serializable]
        public class Burn
        {
            public int count;
            public float power;
            public float time;
        }

        [System.Serializable]
        public class Paralysis
        {
            public float time;
        }


        [System.Serializable]
        public class Slow
        {
            public float time;
            public float percent;
        }
    }
}