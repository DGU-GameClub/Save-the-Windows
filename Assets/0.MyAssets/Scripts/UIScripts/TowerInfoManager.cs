using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerInfoManager : MonoBehaviour
{
    public static TowerInfoManager instance;
    public GameObject Towerinfo;
    public Image TowerImage;
    public TextMeshProUGUI Money;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Synergy1;
    public TextMeshProUGUI Synergy2;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Contents;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Cooldown;
    private void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void Setup(SpriteRenderer sp, int tm, string tn, string ts1, string ts2, int tl, string tc, float ta, float tcd) {
        TowerImage.sprite = sp.sprite;
        Money.text = tm.ToString();
        Name.text = tn;
        Synergy1.text = ts1;
        Synergy2.text = ts2;
        Level.text = "레벨 : " + tl.ToString();
        Contents.text = tc;
        Attack.text = "공격력 : " + ta.ToString();
        Cooldown.text = "쿨타임 : " + tcd.ToString();
        Towerinfo.SetActive(true);
    }
    public void CloseInfo() {
        Towerinfo.SetActive(false);
    }
}
