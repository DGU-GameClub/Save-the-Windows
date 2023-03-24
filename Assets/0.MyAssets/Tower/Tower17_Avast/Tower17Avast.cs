using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower17Avast : TowerUnit
{
    public void GetMoney()
    {
        GameManagers.instance.AddMoney((int)Attak);
    }
}
