using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Store : MonoBehaviour
{
    public GameObject[] towerPrefabs;
    public GameObject[] towers10; //10원 타워 프리팹 배열
    public GameObject[] towers20; //20원 타워 프리팹 배열
    public GameObject[] towers30; //30원 타워 프리팹 배열
    public GameObject[] towers40; //40원 타워 프리팹 배열
    public GameObject[] towers50; //50원 타워 프리팹 배열

    public GameObject slotsParent; // 상점 슬롯들의 부모 오브젝트. -> store 오브젝트.
    public Inventory inven;

    private GameObject[] slots; // 상점의 슬롯 배열.
    private ColorBlock colors; //버튼 비활시 지정된 색 변경
    TowerUnit tu;
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
        Transform slot;

        for (int i = 0; i < slots.Length; i++)
        {
            random = Random.Range(0,towerPrefabs.Length);
            tu = towerPrefabs[random].GetComponentInChildren<TowerUnit>();
            slot = slots[i].transform;

            sprite = tu.TowerImage.sprite;
            name = tu.UnitName.ToString();
            price = tu.UnitPrice;
            contents = tu.Contents;
            texts = slot.Find("Texts");

            slot.GetComponent<Image>().sprite = sprite;
            texts.GetChild(0).GetComponent<TMP_Text>().text = name;
            texts.GetChild(1).GetComponent<TMP_Text>().text = contents;
            texts.GetChild(2).GetComponent<TMP_Text>().text = price.ToString();
            slot.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OnSlotClick(int slotIndex)
    {
        Sprite target = slots[slotIndex].GetComponent<Image>().sprite;
        //해당 슬롯이 비어있거나, 인벤토리가 다 차 있거나, 돈이 없으면 클릭 불가능
        if (target == null || inven.IsInvenFull() || GameManagers.instance.Money < 10)
        {
            //색 안변하게 기존 설정 변경
            colors = slots[slotIndex].gameObject.GetComponent<Button>().colors;
            colors.disabledColor = colors.normalColor;
            slots[slotIndex].gameObject.GetComponent<Button>().colors = colors;
            return;
        }

         //해당 슬롯에 있는 타워 프리팹을 인벤토리에 추가.
        GameObject towerPrefab = FindTowerWithSprite(towerPrefabs, target);
        price = towerPrefab.GetComponentInChildren<TowerUnit>().UnitPrice;
        inven.OnTowerClick(towerPrefab);

        SellTower(slotIndex, towerPrefab);
    }

    //타워를 판매하는 함수
    void SellTower(int slotIndex, GameObject tower)
    {
        TowerUnit tu = tower.GetComponentInChildren<TowerUnit>();

        //태그가 trash면 생명 빼기 -> 생명이 1 남았을 때는 못 사게 해야하나?
        //아니면 타워 가격 빼기
        if (tower.tag == "TowerRecycleBin"){
            // if (GameManagers.instance.Life == 2){
            //     Debug.Log("생명이 1 남았습니다.");
            //     return;
            // }
            GameManagers.instance.Life -= 2;
        } else {
            GameManagers.instance.Money -= tu.UnitPrice;
        }

        //판매시 슬롯의 타워 이미지 삭제
        slots[slotIndex].gameObject.GetComponent<Image>().sprite = null;
        slots[slotIndex].transform.GetChild(0).gameObject.SetActive(false);
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

    public GameObject FindTowerWithSprite(GameObject[] towerArray, Sprite target)
    {
        foreach(GameObject tower in towerArray)
        {
            SpriteRenderer spriteRenderer = tower.GetComponentInChildren<SpriteRenderer>();

            if(spriteRenderer != null && spriteRenderer.sprite == target)
            {
                return tower;
            }
        }
        return null;
    }
    public GameObject GetRandomTower()
    {
        int probability10 = GameManagers.instance.probability10; // 10원 타워의 확률
        int probability20 = GameManagers.instance.probability20; // 20원 타워의 확률
        int probability30 = GameManagers.instance.probability30; // 30원 타워의 확률
        int probability40 = GameManagers.instance.probability40;  // 40원 타워의 확률
        int probability50 = GameManagers.instance.probability50;  // 50원 타워의 확률

        int totalProbability = probability10 + probability20 + probability30 + probability40 + probability50;
        int randomValue = Random.Range(0, totalProbability);

        if (randomValue < probability10) // chance for 10원 tower
        {
            int index = Random.Range(0, towers10.Length);
            return towers10[index];
        }
        else if (randomValue < probability10 + probability20) // chance for 20원 tower
        {
            int index = Random.Range(0, towers20.Length);
            return towers20[index];
        }
        else if (randomValue < probability10 + probability20 + probability30 ) // chance for 30원 tower
        {
            int index = Random.Range(0, towers30.Length);
            return towers30[index];
        }
        else if (randomValue < probability10 + probability20 + probability30 + probability40) // chance for 40원 tower
        {
            int index = Random.Range(0, towers40.Length);
            return towers40[index];
        }
        else
        {
            int index = Random.Range(0, towers50.Length);
            return towers50[index];
        }
    }

}