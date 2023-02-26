using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [HideInInspector]
    public TowerProperty tower;
    public UnityEngine.UI.Image image;
    public UnityEngine.UI.Button sellBtn;

    private void Start() 
    {
        SetSellBtnOn(false);
    }

    void SetSellBtnOn(bool b)
    {
        if(sellBtn != null)
        {
            sellBtn.interactable = b;
        }
    }
    public void SetTower(TowerProperty tower)
    {
        this.tower = tower;
        
        if(tower == null)
        {
            image.enabled = false;
            SetSellBtnOn(false);
            gameObject.name = "Empty";
        }
        else
        {
            image.enabled = true;

            gameObject.name = tower.name;
            image.sprite = tower.sprite;
            SetSellBtnOn(true);
        }
    }

    public void OnClickSellBtn()
    {
        SetTower(null);
    }
}
