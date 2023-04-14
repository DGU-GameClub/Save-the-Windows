using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{   
    //���콺 �̺�Ʈ
    private bool isclicked = false;
    public bool isCreate = false;
    public GameObject alpha150 = null;
    private GameObject createalpha = null;
    public GameObject Range;
    TowerUnit Tu;
    Map Tilemap;
    private void Start()
    {
        Tilemap = GameObject.Find("Grid").GetComponent<Map>();
        Tu = gameObject.GetComponentInChildren<TowerUnit>();
    }
    private void OnMouseDown()
    {
        isclicked = true;
        
        TowerInfoManager.instance.Setup(Tu.TowerImage, Tu.UnitPrice, Tu.UnitName,
            Tu.Synergy1, Tu.Synergy2, Tu.TowerLevel, Tu.Contents, Tu.Attack, Tu.Cooldown);
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
            // ���콺���� ������ ĳ���Ͱ� ������
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 1.0f;
            createalpha.transform.position = mousepos;
        }
        else if (isCreate) {
            Range.SetActive(true);
        }
    }

    private void OnMouseUp()
    {
        isclicked = false;
        if (!isCreate)
        {
            //Ÿ�� ��ġ ��ġ �˻� �� ��ġ
            if (Vector3.Distance(Tilemap.GetCoordTileUnderMouse(), createalpha.transform.position) < 1.16f)
            {
                Collider2D objectCollider = Physics2D.OverlapCircle(createalpha.transform.position, 0.1f, 1 << 8);
                
                if (objectCollider != null) {
                    if (objectCollider.gameObject.GetComponentInChildren<TowerUnit>().AddExp(gameObject)) {
                        Destroy(createalpha);
                        Destroy(gameObject); 
                    }
                }
                else {
                    gameObject.transform.position = Tilemap.GetCoordTileUnderMouse();
                    isCreate = true;
                }
            }
            Destroy(createalpha);
        }
        Range.SetActive(false);
        TowerInfoManager.instance.CloseInfo();
    }
}
