using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Store : MonoBehaviour
{
    public GameObject[] towerPrefabs; //타워 프리팹 배열
    public GameObject slotsParent; // 상점 슬롯들의 부모 오브젝트. -> store 오브젝트.
    public Inventory inven;

    private GameObject[] slots; // 상점의 슬롯 배열.
    private ColorBlock colors; //버튼 비활시 지정된 색 변경
    private Transform texts;
    private int price;

    private void Start() {
        slots = new GameObject[3];
        inven = GameObject.Find("Inventory").GetComponent<Inventory>();

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotsParent.transform.GetChild(i).gameObject;
        }
        InitSlot();
    }

    void InitSlot()
    {
        //상점슬롯에 타워 랜덤하게 가져오고 이미지, context 띄우기
        int random;
        Sprite sprite;
        string contents;
        string name;

        for (int i = 0; i < slots.Length; i++)
        {
            random = Random.Range(0,towerPrefabs.Length);
            GameObject towerPrefab = towerPrefabs[random];
            GameObject tower = Instantiate(towerPrefab, slots[i].transform.position, Quaternion.identity);
            tower.transform.SetParent(slots[i].transform);

            sprite = tower.GetComponentInChildren<SpriteRenderer>().sprite;
            name = towerPrefab.name.Substring(towerPrefab.name.LastIndexOf('_')+1);
            price = tower.GetComponentInChildren<TowerUnit>().UnitPrice;
            contents = tower.GetComponentInChildren<TowerUnit>().Contents;
            texts = slots[i].transform.Find("Texts");

            slots[i].GetComponent<Image>().sprite = sprite;
            texts.GetChild(0).GetComponent<TMP_Text>().text = name + " : " + contents;
            texts.GetChild(1).GetComponent<TMP_Text>().text = "PRICE : " + price.ToString();
            slots[i].transform.GetChild(0).gameObject.SetActive(true);
        }
        
    }

    public void OnSlotClick(int slotIndex)
    {
        //해당 슬롯이 비어있거나, 인벤토리가 다 차 있거나, 돈이 없으면 클릭 불가능
        if (slots[slotIndex].transform.childCount == 1 || inven.IsInvenFull() || GameManagers.instance.Money == 0)
        {
            //색 안변하게 기존 설정 변경
            colors = slots[slotIndex].gameObject.GetComponent<Button>().colors;
            colors.disabledColor = colors.normalColor;
            slots[slotIndex].gameObject.GetComponent<Button>().colors = colors;
            return;
        }

        //해당 슬롯에 있는 타워 프리팹을 인벤토리에 추가.
        GameObject towerPrefab = slots[slotIndex].transform.GetChild(1).gameObject;
        price = towerPrefab.GetComponentInChildren<TowerUnit>().UnitPrice;
        inven.OnTowerClick(towerPrefab);

        SellTower(slotIndex, price);
    }

    //타워를 판매하는 함수
    void SellTower(int slotIndex, int price)
    {
        //타워 가격 빼기
        GameManagers.instance.Money -= price;
        //판매시 슬롯의 타워 오브젝트 삭제
        slots[slotIndex].gameObject.GetComponent<Image>().sprite = null;
        slots[slotIndex].transform.GetChild(0).gameObject.SetActive(false);
        Destroy(slots[slotIndex].transform.GetChild(1).gameObject);
    }

    //타워 전부 삭제하고 다시 초기화
    public void DestroyAllTower()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount == 2)
                Destroy(slots[i].transform.GetChild(1).gameObject);
        }
        InitSlot();
    }
}