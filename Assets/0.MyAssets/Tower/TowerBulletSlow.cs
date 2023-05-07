using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBulletSlow : TowerBullet
{
    public float time = 0f;
    public float percent = 0f;

    private void Start()
    {
        if (Tower.GetComponent<TowerUnit>().TowerLevel == 2)
            percent = 0.6f;
        else if (Tower.GetComponent<TowerUnit>().TowerLevel == 3)
            percent = 0.5f;
    }
}
