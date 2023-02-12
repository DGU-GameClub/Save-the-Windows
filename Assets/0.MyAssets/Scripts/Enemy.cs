using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : LivingEntity
{
    //speed
    [System.NonSerialized]
    public float moveSpeed;
    //tile object
    [System.NonSerialized]
    public Transform wayPoints;

    //target transform
    Transform[] targetArr;
    List<Transform> targetList;
    int targetIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //get taget transform
        targetList = new List<Transform>();
        for (int i = 0; i < wayPoints.childCount; i++)
        {
            targetList.Add(wayPoints.GetChild(i).transform);
        }
        targetArr = targetList.ToArray();

        //set position
        transform.position = targetArr[0].position;

        //move start
        StartCoroutine(MoveTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Sprite sprite, float _speed, float _health, Transform _wayPoints)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        moveSpeed = _speed;
        health = _health;
        wayPoints = _wayPoints;
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
