using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [HideInInspector]
    public TowerProperty tower;
    public UnityEngine.UI.Image image;
    public UnityEngine.UI.Button sellBtn;
    GameObject prefeb;
    public TextMeshProUGUI contents;
    int price;


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
            gameObject.name = "";
        }
        else
        {
            image.enabled = true;
            gameObject.name = tower.name;
            image.sprite= tower.prefeb.GetComponentInChildren<SpriteRenderer>().sprite;
            price = tower.prefeb.GetComponentInChildren<TowerUnit>().UnitPrice;
            prefeb = tower.prefeb;
            if (contents.text != "") contents.text = tower.prefeb.GetComponentInChildren<TowerUnit>().Contents;
            SetSellBtnOn(true);
        }
    }

    public void OnClickSellBtn()
    {
        SetTower(null);
    }

}
