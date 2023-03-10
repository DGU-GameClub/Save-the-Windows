using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //인벤토리에서 타워 꺼내야하므로 인벤토리 슬롯에는 버튼컴포넌트 그대로 두기
    public Transform rootSlot;
    public Store store;
    private Slot[] slots;
    public UnityEngine.UI.Button sellBtn;
    // Start is called before the first frame update
    void Start()
    {
        slots = new Slot[3];
        int slotCnt = rootSlot.childCount;

        for(int i=0; i < slotCnt; i++)
        {
            var slot = rootSlot.GetChild(i).GetComponent<Slot>();
            slots[i] = slot;
        }
        store.onSlotClick += BuyTower;
    }

    //어떤 타워가 들어오는지 알아야 함
    void BuyTower(TowerProperty tower) 
    {
        Slot emptySlot = null;

        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].tower.name == "" || slots[i].tower == null)
            {
                emptySlot = slots[i];
                break;
            }
        }
        Debug.Log(emptySlot);
        //빈 슬롯이 있다면
        if(emptySlot != null)
        {
            emptySlot.SetTower(tower);
        }
    }
}
