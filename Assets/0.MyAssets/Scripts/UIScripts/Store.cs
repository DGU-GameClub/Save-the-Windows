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
    private bool[] slotOn;  // 상점 슬롯의 활성화 여부를 저장하는 배열. 
    private ColorBlock colors; //버튼 비활시 지정된 색 변경

    private void Start() {
        //상점 슬롯들의 활성화 여부 true로 초기화
        slotOn = new bool[5];
        slots = new GameObject[5];
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
        List<int> ranList = new List<int>();
        int random;
        for(int i=0; i< slots.Length; i++)
        {
            do
            {
                random = Random.Range(0,towerPrefabs.Length);
            }
            while (ranList.Contains(random));
            ranList.Add(random);
        }

        for (int i = 0; i < slots.Length; i++)
        {
            random = ranList[i];
            GameObject towerPrefab = towerPrefabs[random];
            GameObject tower = Instantiate(towerPrefab, slots[i].transform.position, Quaternion.identity);
            tower.transform.SetParent(slots[i].transform);

            slots[i].GetComponent<Image>().sprite = tower.GetComponentInChildren<SpriteRenderer>().sprite;
            slots[i].transform.Find("Text").GetComponent<TMP_Text>().text= towerPrefab.GetComponentInChildren<TowerUnit>().Contents;
            slotOn[i] = true;
        }
    }

    public void OnSlotClick(int slotIndex)
    {
        //해당 슬롯이 비어있거나 인벤토리가 다 차 있으면 클릭 불가능
        if (!slotOn[slotIndex] || inven.IsInvenFull())
        {
            //색 안변하게 기존 설정 변경
            colors = slots[slotIndex].gameObject.GetComponent<Button>().colors;
            colors.disabledColor = colors.normalColor;
            slots[slotIndex].gameObject.GetComponent<Button>().colors = colors;
            //버튼 비활
            slots[slotIndex].gameObject.GetComponent<Button>().interactable = false;
            return;
        }

        //해당 슬롯에 있는 타워 프리팹을 인벤토리에 추가.
        GameObject towerPrefab = slots[slotIndex].transform.GetChild(1).gameObject;
        inven.OnTowerClick(towerPrefab);

        //해당 슬롯 비활성화
        slotOn[slotIndex] = false;
        SellTower(slotIndex);
    }

    //타워를 판매하는 함수
    void SellTower(int slotIndex)
    {
        //판매시 슬롯의 타워 오브젝트 삭제
        slots[slotIndex].gameObject.GetComponent<Image>().sprite = null;
        slots[slotIndex].transform.Find("Text").GetComponent<TMP_Text>().text = null;
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