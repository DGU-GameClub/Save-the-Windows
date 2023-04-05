using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{   
    //마우스 이벤트
    private bool isclicked = false;
    private bool isCreate = false;
    public GameObject alpha150 = null;
    private GameObject createalpha = null;

    private void OnMouseDown()
    {
        isclicked = true;
        TowerUnit Tu = gameObject.GetComponentInChildren<TowerUnit>();
        TowerInfoManager.instance.Setup(Tu.TowerImage, Tu.UnitPrice, Tu.UnitName, 
            Tu.Synergy1, Tu.Synergy2, Tu.TowerLevel, Tu.Contents, Tu.Attack, Tu.Cooldown);
        if (isCreate) return;
        createalpha = Instantiate(alpha150, transform);
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 1.0f;
        createalpha.transform.position = mousepos;
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
    }

    private void OnMouseUp()
    {
        isclicked = false;
        if (!isCreate)
        {
            //타워 설치 위치 검사 후 설치
            //Instantiate(realTower, createalpha.transform.position, Quaternion.identity);
            gameObject.transform.position = createalpha.transform.position;
            isCreate = true;
            Destroy(createalpha);
        }
        TowerInfoManager.instance.CloseInfo();
    }
}
