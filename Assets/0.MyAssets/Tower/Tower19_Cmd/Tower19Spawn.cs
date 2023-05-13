using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower19Spawn : MonoBehaviour
{
    private bool isclicked = false;
    public bool isCreate = false;
    public GameObject alpha150 = null;
    private GameObject createalpha = null;
    public GameObject Range;
    Map Tilemap;
    TowerUnit Tu;
    public GameObject child;
    private void Start()
    {
        Tilemap = GameObject.Find("Grid").GetComponent<Map>();
        Tu = gameObject.GetComponentInChildren<TowerUnit>();
    }
    private void OnMouseDown()
    {
        isclicked = true;
        if (GameManagers.instance.SellMode)
        {
            if (isCreate)
            {
                int SellMoney = 0;
                if (Tu.TowerLevel == 1)
                    SellMoney = (int)(Tu.UnitPrice * 0.8);
                else if (Tu.TowerLevel == 2)
                    SellMoney = (int)(Tu.UnitPrice * 0.8 * 5);
                else if (Tu.TowerLevel == 3)
                    SellMoney = (int)(Tu.UnitPrice * 0.8 * 12);
                GameManagers.instance.AddMoney(SellMoney);
                GameManagers.instance.RemoveTowerNumber();
                Destroy(gameObject);
                return;
            }
            else if (!isCreate) { return; }
        }
        TowerInfoManager.instance.Setup(Tu.TowerImage, Tu.UnitPrice, Tu.UnitName,
            Tu.Synergy1, Tu.Synergy2, Tu.TowerLevel, Tu.Contents, Tu.Attack, Tu.Cooldown, Tu.ExpPercent());
        if (!isCreate)
        {
            createalpha = Instantiate(alpha150);
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 1.0f;
            createalpha.transform.position = mousepos;
            return;
        }

    }

    private void OnMouseDrag()
    {
        if (GameManagers.instance.SellMode) return;
        if (isclicked && !isCreate)
        {
            // 마우스따라 반투명 캐릭터가 움직임
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 1.0f;
            createalpha.transform.position = mousepos;
        }
        else if (isCreate)
        {
            Range.SetActive(true);
        }
    }

    private void OnMouseUp()
    {
        if (GameManagers.instance.SellMode) return;
        isclicked = false;
        if (!isCreate)
        {
            if (Vector3.Distance(Tilemap.GetCoordTileUnderMouse(), createalpha.transform.position) < 1.1f)
            {
                Collider2D objectCollider = Physics2D.OverlapCircle(createalpha.transform.position, 0.05f, 1 << 8);

                if (objectCollider != null && !objectCollider.Equals(gameObject))
                {
                    if (objectCollider.gameObject.GetComponentInChildren<TowerUnit>().AddExp(gameObject))
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    child.GetComponent<CircleCollider2D>().enabled = true;
                    gameObject.transform.position = Tilemap.GetCoordTileUnderMouse();
                    gameObject.GetComponentInChildren<Tower19Cmd>().SetupCMD();
                    Tu.EffectOn();
                    isCreate = true;
                    GameManagers.instance.AddTowerNumber();
                }
            }
            Destroy(createalpha);
        }
        Range.SetActive(false);
        TowerInfoManager.instance.CloseInfo();
    }
}
