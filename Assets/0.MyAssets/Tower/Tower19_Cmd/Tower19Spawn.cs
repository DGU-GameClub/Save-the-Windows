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
    private void Start()
    {
        Tilemap = GameObject.Find("Grid").GetComponent<Map>();
        Tu = gameObject.GetComponentInChildren<TowerUnit>();
    }
    private void OnMouseDown()
    {
        isclicked = true;
        
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
                    gameObject.transform.position = Tilemap.GetCoordTileUnderMouse();
                    gameObject.GetComponentInChildren<Tower19Cmd>().SetupCMD();
                    Tu.EffectOn();
                    isCreate = true;
                }
            }
            Destroy(createalpha);
        }
        Range.SetActive(false);
        TowerInfoManager.instance.CloseInfo();
    }
}
