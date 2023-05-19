using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private GameObject[] invenSlots;//인벤토리 슬롯 배열
    public GameObject invenParent;//Inven 오브젝트
    public GameObject realTower;
    GameObject[] towerArr;//인벤토리에 들어온 타워 배열
    Button[] sellBtns;
    Vector3 towerPos;

    private void Start()
    {
        invenSlots = new GameObject[3];
        towerArr = new GameObject[3];
        sellBtns = new Button[3];
        //인벤토리 내 하위 슬롯들 초기화
        for (int i = 0; i < invenSlots.Length; i++)
        {
            invenSlots[i] = invenParent.transform.GetChild(i).gameObject;
            towerArr[i] = null;
            sellBtns[i] = invenSlots[i].GetComponentInChildren<Button>();
            sellBtns[i].interactable = false;
        }
    }

    private void Update() {
        //타워 위치랑 인벤토리 위치랑 비교해보기
        //다르면 타워 배열에서 해당 타워 해제, 해당 sellBtn 비활성화
        for(int i=0; i<3; i++){
            towerPos = invenSlots[i].transform.position;
            if(towerArr[i] != null && towerArr[i].transform.position != towerPos){
                towerArr[i] = null;
                sellBtns[i].interactable = false;
            }
        }
    }

    public void OnTowerClick(GameObject tower)
    {
        int currentSlot = 0;
        //현재 슬롯이 인벤토리 슬롯의 배열길이보다 작고, 타워가 있는지
        while (currentSlot < invenSlots.Length && towerArr[currentSlot] != null){
            currentSlot++;
        }
        // 인벤토리 슬롯이 모두 찼으면 추가x
        if (currentSlot == invenSlots.Length){
            return;
        }
        sellBtns[currentSlot].interactable = true;
        AddTower(tower, currentSlot);
    }

    //판매버튼 클릭시 이벤트
    public void OnSellClick(int slotIndex)
    {
        //타워 있는지 검사
        if (towerArr[slotIndex] == null){
            Debug.Log("삭제할 타워가 없습니다!");
            return;
        }

        TowerUnit tu = towerArr[slotIndex].GetComponentInChildren<TowerUnit>();

        //태그가 trash면 50원 더해주기
        if (towerArr[slotIndex].tag == "TowerRecycleBin"){
            GameManagers.instance.Money += 50;
        } else{
            GameManagers.instance.Money += tu.UnitPrice;
        }

        DestroyTower(slotIndex);
    }

    public bool IsInvenFull()
    {
        foreach (GameObject tower in towerArr){
            if (tower == null){
                return false;
            }
        }
        Debug.Log("인벤토리가 가득 찼습니다.");
        return true;
    }

    public void AddTower(GameObject tower, int currentSlot)
    {
        GameObject towerInstance = Instantiate(tower, invenSlots[currentSlot].transform.position, Quaternion.identity);
        towerInstance.transform.SetParent(realTower.transform);
        towerArr[currentSlot] = towerInstance;
    }

    public void DestroyTower(int towerIndex)
    {
        GameObject target = towerArr[towerIndex];
        //Debug.Log("Sell " + target.GetComponentInChildren<TowerUnit>().UnitName);
        sellBtns[towerIndex].interactable = false;
        towerArr[towerIndex] = null;
        Destroy(target);
    }
}
