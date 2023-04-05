using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower16V3 : TowerUnit
{
    public void LifeUp() {
        GameManagers.instance.RecoveryLife((int)Attack);
    }
}
