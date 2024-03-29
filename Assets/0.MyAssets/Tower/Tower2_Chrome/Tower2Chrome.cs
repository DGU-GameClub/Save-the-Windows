using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower2Chrome : TowerUnit
{
    public GameObject[] TowerBullet;
    public GameObject TowerPrefeb;

    public float bulletScale_X = 0.5f;
    public float bulletScale_Y = 0.5f;
    public AudioSource TowerAudio;
    public AudioClip[] TowerAudioClip;
    private void Awake()
    {
        AttackTime = Cooldown;
    }
    private void FixedUpdate()
    {
        AttackTime += Time.deltaTime;
        if (CheckTheNullEnemy()) return;
        if (AttackTime > Cooldown)
        {
            FindTarget();
            if (AttackEnemy == null) return;
            AttackTime = 0.0f;
            Attack();

        }

    }
   new void Attack()
    {
        int RandomNum = Random.Range(0, 3);
        var Bullet = Instantiate(TowerBullet[RandomNum], transform.position, Quaternion.identity, TowerPrefeb.transform);
        TowerAudio.clip = TowerAudioClip[RandomNum];
        if (RandomNum < 2) TowerAudio.volume = .3f;
        else TowerAudio.volume = .45f;
        TowerAudio.Play();
        Bullet.GetComponent<TowerBullet>().AttackEnemy = AttackEnemy;
        Bullet.GetComponent<TowerBullet>().Tower = gameObject;
        Bullet.GetComponent<TowerBullet>().targetPosition = (AttackEnemy.transform.position - transform.position).normalized;
        Bullet.transform.localScale = new Vector3(bulletScale_X, bulletScale_Y);
    }
}
