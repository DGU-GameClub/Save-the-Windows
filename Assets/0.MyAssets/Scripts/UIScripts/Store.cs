using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Store : MonoBehaviour
{
    public TowerBuffer towerBuffer;
    public Transform slotRoot;
    private List<Slot> slots;
	//onSlotClick에 외부에서 연결해주면 호출
	public System.Action<TowerProperty> onSlotClick;
    public bool fullInvenSlot = false;
    void Start()
    {
        slots = new List<Slot>();
        int slotCnt = slotRoot.childCount;
        //오브젝트 slot의 Slot 컴포넌트 가져오기
        for(int i=0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();

            if(i < towerBuffer.towers.Count)
            {
                slot.SetTower(towerBuffer.towers[i]);
            }
            else
            {
                //아이템 없는 슬롯의 경우 버튼 비활
				slot.GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
            slots.Add(slot);
        }
    }

	public void OnClickSlot(Slot slot)
	{
		if(onSlotClick != null)
		{
            slot.image.enabled = false;
            onSlotClick(slot.tower);
            slot.GetComponent<UnityEngine.UI.Button>().interactable = false;
		}
	}

    public void SetText()
    {
        var emptySlot = slots.Find(t => t.image.enabled == true);
        if (emptySlot != null)
        {
            emptySlot.contents.enabled = true;
        }
    }

}
