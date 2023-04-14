using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower17Avast : TowerUnit
{
    public void GetMoney()
    {
        GameManagers.instance.AddMoney((int)Attack);
    }
    protected override void StatusUp()
    {
        if (TowerLevel == 2)
        {
            PrimitiveAttack = 30f;
            Attack = 30f;
        }
        else if (TowerLevel == 3)
        {
            PrimitiveAttack = 50f;
            Attack = 50f;
        }
    }
}
