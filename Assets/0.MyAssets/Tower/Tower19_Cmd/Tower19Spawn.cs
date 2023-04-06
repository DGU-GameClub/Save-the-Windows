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
    private void OnMouseDown()
    {
        isclicked = true;
        TowerUnit Tu = gameObject.GetComponentInChildren<TowerUnit>();
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
            //Ÿ�� ��ġ ��ġ �˻� �� ��ġ
            //Instantiate(realTower, createalpha.transform.position, Quaternion.identity);
            gameObject.transform.position = createalpha.transform.position;
            gameObject.GetComponentInChildren<Tower19Cmd>().SetupCMD();
            isCreate = true;
            Destroy(createalpha);
        }
        Range.SetActive(false);
        TowerInfoManager.instance.CloseInfo();
    }
}
