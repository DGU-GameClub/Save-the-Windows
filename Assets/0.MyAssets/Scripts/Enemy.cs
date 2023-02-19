using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Setup(Sprite sprite, float _speed, float _health, Transform _wayPoints)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        moveSpeed = _speed;
        originSpeed = moveSpeed;
        health = _health;
        originHealth = health;
        wayPoints = _wayPoints;
    }

    public void SpecialDamage()
    {
        if(gameObject.tag != "Enemy")
        {
            StopCoroutine("BurnDamage");
            StopCoroutine("ParalysisDamage");
            StopCoroutine("SlowDamage");

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1, 1, 1, 1);
            moveSpeed = originSpeed;
        }

        switch (gameObject.tag)
        {
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
    }

    IEnumerator BurnDamage(float _power, int _count, float _time)
    {
        int num = 0;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 0, 0, 1);
        while (num != _count)
        {
            num++;
            TakeDamage(_power);
            print(health);
            yield return new WaitForSeconds(_time);
        }

        spriteRenderer.color = new Color(1, 1, 1, 1);
        gameObject.tag = "Enemy";
    }

    IEnumerator ParalysisDamage(float _time)
    {
        float originSpeed = moveSpeed;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        moveSpeed = 0f;
        spriteRenderer.color = new Color(1, 0.5f, 0, 1);

        yield return new WaitForSeconds(_time);

        moveSpeed = originSpeed;
        gameObject.tag = "Enemy";
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    IEnumerator SlowDamage(float _time, float _percent)
    {
        float originSpeed = moveSpeed;
        moveSpeed = _percent * moveSpeed;

        yield return new WaitForSeconds(_time);

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
}
