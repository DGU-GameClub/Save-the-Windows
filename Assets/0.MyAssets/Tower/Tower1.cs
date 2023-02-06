using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1 : TowerUnit
{
    private void FixedUpdate()
    {
        if (CheckTheNullEnemy()) return;
        FindTarget();
    }
}
