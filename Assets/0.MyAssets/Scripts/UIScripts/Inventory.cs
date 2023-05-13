using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.EventSystem;

public class Inventory : MonoBehaviour
{
    private GameObject[] invenSlots;//인벤토리 슬롯 배열
    public GameObject invenParent;//Inven 오브젝트
    private int currentSlot = 0; //인벤토리의 현재 슬롯 번호
    public Map map;

    public GameObject realTower;
    private List<GameObject> towerList;

    private void Start()
    {
        invenSlots = new GameObject[3];
        towerList = new List<GameObject>();
        //인벤토리 내 하위 슬롯들 초기화
        for (int i = 0; i < invenSlots.Length; i++)
        {
            invenSlots[i] = invenParent.transform.GetChild(i).gameObject;
        }
    }

    public void OnTowerClick(GameObject tower)
    {
        int currentSlot = 0;
        //현재 슬롯이 인벤토리 슬롯의 배열길이보다 작고, 타워가 있는지
        while (currentSlot < invenSlots.Length && invenSlots[currentSlot].transform.GetChild(0).GetComponent<Image>().sprite != null)
        {
            currentSlot++;
        }
        // 인벤토리 슬롯이 모두 찼으면 추가x
        if (currentSlot == invenSlots.Length)
        {
            return;
        }
        invenSlots[currentSlot].transform.GetChild(0).GetComponent<Image>().sprite = tower.GetComponentInChildren<SpriteRenderer>().sprite;
        invenSlots[currentSlot].gameObject.GetComponentInChildren<Button>().interactable = true;
        AddTower(tower, currentSlot);
    }

    //판매버튼 클릭시 이벤트
    public void OnSellClick(int slotIndex)
    {
        Sprite sprite = invenSlots[slotIndex].transform.GetChild(0).GetComponent<Image>().sprite;
        //돈 다시 리턴
        GameObject towerPrefab = FindTowerInList(sprite);
        GameManagers.instance.Money += towerPrefab.GetComponentInChildren<TowerUnit>().UnitPrice;
        invenSlots[slotIndex].transform.GetChild(0).GetComponent<Image>().sprite = null;
        invenSlots[slotIndex].gameObject.GetComponentInChildren<Button>().interactable = false;
        DestroyTower(towerPrefab);
    }

    public bool IsInvenFull()
    {
        Sprite sprite;
        for (int i = 0; i < invenSlots.Length; i++)
        {
            sprite = invenSlots[i].transform.GetChild(0).GetComponent<Image>().sprite;
            //인벤슬롯에 타워가 안 들어갔으면 -> 이미지가 없으면
            if (sprite == null)
            {
                return false;
            }
        }
        return true;
    }
    public void AddTower(GameObject tower, int currentSlot)
    {
        Debug.Log(currentSlot);
        GameObject towerInstance = Instantiate(tower, invenSlots[currentSlot].transform.position, Quaternion.identity);
        towerInstance.transform.SetParent(realTower.transform);
        towerList.Add(towerInstance);
    }

    public GameObject FindTowerInList(Sprite target)
    {
        foreach(GameObject tower in towerList)
        {
            SpriteRenderer spriteRenderer = tower.GetComponentInChildren<SpriteRenderer>();
          
            if (spriteRenderer != null && spriteRenderer.sprite == target)
            {
                return tower; // 일치하는 스프라이트가 있는 타워 오브젝트를 반환합니다.
            }
        }
        return null; // 일치하는 스프라이트가 없으면 null을 반환합니다.
    }

    public void DestroyTower(GameObject targetObject)
    {
        if (towerList.Contains(targetObject))
        {
            towerList.Remove(targetObject);
            Debug.Log("Destroy " + targetObject);
            Destroy(targetObject);  
        }
    }
}
