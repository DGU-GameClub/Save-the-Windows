using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private GameObject[] invenSlots;//인벤토리 슬롯 배열
    public GameObject invenParent;//Inven 오브젝트
    private int currentSlot = 0; //인벤토리의 현재 슬롯 번호

    private void Start() {
        invenSlots = new GameObject[3];
        //인벤토리 내 하위 슬롯들 초기화
        for (int i = 0; i < invenSlots.Length; i++)
        {
            invenSlots[i] = invenParent.transform.GetChild(i).gameObject;
        }
    }

    public void OnTowerClick(GameObject towerPrefab)
    {
        int currentSlot = 0;
        //현재 슬롯이 인벤토리 슬롯의 배열길이보다 작고, 배열의 자식 슬롯들이 1이 아닌지(타워가 들어가 있는지) 확인
        while (currentSlot < invenSlots.Length && invenSlots[currentSlot].transform.childCount != 1)
        {
            currentSlot++;
        }

        // 인벤토리 슬롯이 모두 찼으면 추가x
        if (currentSlot == invenSlots.Length)
        {
            return;
        }

        //인벤토리에 타워 추가
        GameObject tower = Instantiate(towerPrefab, invenSlots[currentSlot].transform.position, Quaternion.identity);
        tower.transform.SetParent(invenSlots[currentSlot].transform);
        invenSlots[currentSlot].GetComponent<Image>().sprite = tower.GetComponentInChildren<SpriteRenderer>().sprite;
        invenSlots[currentSlot].gameObject.GetComponentInChildren<Button>().interactable = true;
    }

    //판매버튼 클릭시 이벤트
    public void OnSellClick(int slotIndex)
    {
        Destroy(invenSlots[slotIndex].transform.GetChild(1).gameObject);
        invenSlots[slotIndex].GetComponent<Image>().sprite = null;
        invenSlots[slotIndex].gameObject.GetComponentInChildren<Button>().interactable = false;
    }

    public bool IsInvenFull()
    {
        for (int i = 0; i < invenSlots.Length; i++)
        {
            //인벤슬롯에 타워가 들어갔으면
            if(invenSlots[i].transform.childCount != 2)
            {
                return false;
            }
        }
        Debug.Log("invenfull");
        return true;
    }
}
