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
    public Sprite heart;
    public Sprite dollar;

    private GameObject[] slots; // 상점의 슬롯 배열.
    GameObject to;
    Image[] towerImages;
    private Transform texts;
    private Transform images;
    private int price;
    int []p;

    private void Start() {
        slots = new GameObject[3];
        p = new int[3];
        inven = GameObject.Find("Inventory").GetComponent<Inventory>();
        towerImages = new Image[3];

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotsParent.transform.GetChild(i).gameObject;
            towerImages[i] = slots[i].transform.Find("Images").transform.Find("TowerImage").GetComponent<Image>();
        }
        InitSlot();
    }

    void InitSlot()
    {
        //상점슬롯에 타워 랜덤하게 가져오고 이미지, context 띄우기
        string contents;
        string name;
        Transform slot;


        for (int i = 0; i < slots.Length; i++)
        {
            to = GetRandomTower();
            TowerUnit tu = to.GetComponentInChildren<TowerUnit>();
            slot = slots[i].transform;
            images = slot.GetChild(0).transform;
            texts = slot.GetChild(1).transform;

            name = tu.UnitName.ToString();
            price = tu.UnitPrice;
            contents = tu.Contents;

            //타워가 휴지통일 경우 돈 이미지 -> 하트로 바꿔주기, 돈 색 바꾸기, ui 2로 표시
            if(to.transform.tag == "TowerRecycleBin"){
                texts.transform.Find("MoneyImg").GetComponent<Image>().sprite = heart;
                texts.transform.Find("Money").GetComponent<TMP_Text>().color = Color.red;
                texts.transform.Find("Money").GetComponent<TMP_Text>().text = "2";
            } else {
                texts.transform.Find("MoneyImg").GetComponent<Image>().sprite = dollar;
                texts.transform.Find("Money").GetComponent<TMP_Text>().color = new Color(14/255f, 183/255f, 61/255f);
                texts.transform.Find("Money").GetComponent<TMP_Text>().text = price.ToString();
            }

            //타워 이름, 설명
            texts.GetChild(0).GetComponent<TMP_Text>().text = name;
            texts.GetChild(1).GetComponent<TMP_Text>().text = contents;

            //타워 이미지, 테두리 색
            towerImages[i].sprite = tu.TowerImage.sprite;
            UIManager.instance.ColoringBox(price, images.Find("Border").GetComponent<Image>());

            slot.gameObject.SetActive(true);
        }
    }

    public void OnSlotClick(int slotIndex)
    {
        Sprite target = towerImages[slotIndex].sprite;
        Sprite moneyImg = slots[slotIndex].transform.Find("Texts").transform.Find("MoneyImg").GetComponent<Image>().sprite;
        //해당 슬롯이 비어있거나, 인벤토리가 다 차 있으면 클릭 불가능
        if (target == null || inven.IsInvenFull()){
            return;
        }
        
        GameObject towerPrefab = FindTowerWithSprite(towerPrefabs, target);
        price = towerPrefab.GetComponentInChildren<TowerUnit>().UnitPrice;

        //사려는 타워가 휴지통이 아니고, 가진 돈보다 비싼 타워인지 검사
        if (towerPrefab.transform.tag != "TowerRecycleBin" && GameManagers.instance.Money - price < 0){
            UIManager.instance.ShowErrorMessage();
            return;
        }
         //해당 슬롯에 있는 타워 프리팹을 인벤토리에 추가.
        inven.OnTowerClick(towerPrefab);

        SellTower(slotIndex, towerPrefab);
    }

    //타워를 판매하는 함수
    void SellTower(int slotIndex, GameObject tower)
    {
        TowerUnit tu = tower.GetComponentInChildren<TowerUnit>();

        //태그가 trash면 생명 빼기
        if (tower.tag == "TowerRecycleBin"){
            //체력 2 까기
            GameManagers.instance.DamageLife();
            GameManagers.instance.DamageLife();
        } else {
            GameManagers.instance.Money -= tu.UnitPrice;
        }

        //판매시 슬롯의 타워 이미지 삭제
        towerImages[slotIndex].sprite = null;
        slots[slotIndex].SetActive(false);
    }

    //타워 전부 삭제하고 다시 초기화
    public void DestroyAllTower()
    {
        foreach (Image img in towerImages)
        {
            if (img.sprite != null)
                img.sprite = null;
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