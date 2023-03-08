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

        //move start
        StartCoroutine(MoveTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpecialDamage();
        }
    }

    void RemoveHealthBar()
    {
        if(hpBar != null)
        {
            Destroy(hpBar);
        }
    }

    public void Setup(Sprite sprite, float _speed, float _health, Transform _wayPoints)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        moveSpeed = _speed;
        originSpeed = moveSpeed;
        health = _health;
        originHealth = health;
        wayPoints = _wayPoints;
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


    public void SpecialDamage()
    {
        //중첩x일 경우
        //if(gameObject.tag != "Enemy")
        //{
        //    StopCoroutine("BurnDamage");
        //    StopCoroutine("ParalysisDamage");
        //    StopCoroutine("SlowDamage");
        //
        //    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //    spriteRenderer.color = new Color(1, 1, 1, 1);
        //    moveSpeed = originSpeed;
        //}
        
        switch (gameObject.tag)
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
/*
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag) {
            case "Burn":
                StartCoroutine(BurnDamage(10f, 3, 0.85f));
                break;
            case "Paralysis":
                StartCoroutine(ParalysisDamage(2f));
                break;
            case "Slow":
                StartCoroutine(SlowDamage(2f, 0.5f));
                break;
            default:
                Debug.Log("특수 공격 실패");
                break;
        }
    }*/

    IEnumerator BurnDamage()
    {
        int num = 0;
        int count = specialAttack.burn.count;
        float power = specialAttack.burn.power;
        float time = specialAttack.burn.time;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 0, 0, 1);
        while (num != count)
        {
            num++;
            TakeDamage(power);
            print(health);
            yield return new WaitForSeconds(time);
        }

        spriteRenderer.color = new Color(1, 1, 1, 1);
        gameObject.tag = "Enemy";
    }

    IEnumerator ParalysisDamage()
    {
        float time = specialAttack.paralysis.time;
        float originSpeed = moveSpeed;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        moveSpeed = 0f;
        spriteRenderer.color = new Color(1, 0.5f, 0, 1);

        yield return new WaitForSeconds(time);

        moveSpeed = originSpeed;
        gameObject.tag = "Enemy";
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    IEnumerator SlowDamage()
    {
        float time = specialAttack.slow.time;
        float percent = specialAttack.slow.percent;
        float originSpeed = moveSpeed;
        moveSpeed = percent * moveSpeed;

        yield return new WaitForSeconds(time);

        moveSpeed = originSpeed;
        gameObject.tag = "Enemy";
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