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

    //HpBarUI 추가한 변수
    public GameObject hpBarPrefab; //Instantiate 메서드로 복제할 프리펩을 담을 변수
    public Vector3 hpBarOffset = Vector3.zero;

    GameObject hpBar; //Slider의 초기 세팅, Hp 갱신에 사용할 Slider를 담을 변수
    Image enemyHpBarImage; //Slider의 초기 세팅, Hp 갱신에 사용할 Slider를 담을 변수

    //target transform
    Transform[] targetArr;
    int targetIndex = 0;

    private Coroutine slowDamageCoroutine = null;
    private Coroutine burnDamageCoroutine = null;
    private Coroutine paralysisDamageCoroutine = null;
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

    public void Setup(Sprite sprite, float _speed, float _health, Transform _wayPoints, int price, string tag = "Enemy")
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        moveSpeed = _speed;
        originSpeed = moveSpeed;
        health = _health;
        originHealth = health;
        wayPoints = _wayPoints;
        Price = price;
        OriginPrice = Price;
        gameObject.tag = tag;
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
        hpBar = Instantiate(hpBarPrefab, enemyHpBarCanvas.transform);

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
                ApplyBurn(collision.gameObject.GetComponent<TowerBulletBurn>());
                break;
            case "Paralysis":
                if (gameObject.CompareTag("Boss")) return;
                ApplyParalysis(collision.gameObject.GetComponent<TowerBulletParalysis>());
                break;
            case "Slow":
                if (gameObject.CompareTag("Boss")) return;
                ApplySlow(collision.gameObject.GetComponent<TowerBulletSlow>());
                break;
            default:
                break;
        }
    }
    public void ApplyBurn(TowerBulletBurn burn)
    {
        if (burnDamageCoroutine != null)
        {
            return;
        }
        burnDamageCoroutine = StartCoroutine(BurnDamage(burn));
    }
    IEnumerator BurnDamage(TowerBulletBurn burn)
    {
   
        int num = 0;
        int count = burn.count;
        float power = burn.power;
        float time = burn.time;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 0, 0, 1);

        while (num != count)
        {
            num++;
            TakeDamage(power);
            yield return new WaitForSeconds(time);
        }

        spriteRenderer.color = new Color(1, 1, 1, 1);
        
        burnDamageCoroutine = null;
    }
    public void ApplyParalysis(TowerBulletParalysis paralysis)
    {
        if (paralysisDamageCoroutine != null)
        {
            return;
        }
        paralysisDamageCoroutine = StartCoroutine(ParalysisDamage(paralysis));
    }
    IEnumerator ParalysisDamage(TowerBulletParalysis paralysis)
    {
        float time = paralysis.time;
        float originSpeed = moveSpeed;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
 
        moveSpeed = 0f;
        spriteRenderer.color = new Color(1, 0.5f, 0, 1);

        yield return new WaitForSeconds(time);
        
        moveSpeed = originSpeed;

        paralysisDamageCoroutine = null;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void ApplySlow(TowerBulletSlow slow)
    {
        if (slowDamageCoroutine != null)
        {
            return;
        }
        slowDamageCoroutine = StartCoroutine(SlowDamage(slow));
    }
    IEnumerator SlowDamage(TowerBulletSlow slow)
    {
        float time = slow.time;
        float percent = slow.percent;
        float originSpeed = moveSpeed;
        moveSpeed = percent * moveSpeed;

        yield return new WaitForSeconds(time);

        moveSpeed = originSpeed;

        slowDamageCoroutine = null;
    }

    IEnumerator MoveTarget()
    {
        Vector3 targetPosition = targetArr[targetIndex++].position;

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (targetIndex >= targetArr.Length)
        {
            Die();
            GameManagers.instance.DamageLife();
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
    
}