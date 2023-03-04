using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    private Camera uiCamera; //UI 카메라를 담을 변수
    private Canvas canvas; //캔버스를 담을 변수
    private RectTransform rectParent; //부모의 rectTransform 변수를 저장할 변수
    private RectTransform rectHp; //자신의 rectTransform 저장할 변수

    //HideInInspector는 해당 변수 숨기기, 굳이 보여줄 필요가 없을 때 
    public Vector3 offset = Vector3.zero; //HpBar 위치 조절용, offset은 어디에 HpBar를 위치 출력할지
    [System.NonSerialized]
    public Transform enemyTr; //적 캐릭터의 위치


    void Start()
    {
        canvas = GetComponentInParent<Canvas>(); //부모가 가지고있는 canvas 가져오기, Enemy HpBar canvas임
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = this.gameObject.GetComponent<RectTransform>();
    }

    //LateUpdate는 update 이후 실행함, 적의 움직임은 Update에서 실행되니 움직임 이후에 HpBar를 출력함
    private void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(enemyTr.position + offset); //월드좌표(3D)를 스크린좌표(2D)로 변경, offset은 오브젝트 머리 위치
        /*
        if(screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
            //x, y, (z) 메인카메라에서 XY평면까지의 거리
        }
        백뷰시점에서는 뒤돌 경우 HpBar가 보이는 문제가 있어서 위의 코드로 안보이게 했지만, 나는 쿼터뷰 시점이라서 필요없음
        */

        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos); //스크린좌표에서 캔버스에서 사용할 수 있는 좌표로 변경?

        rectHp.localPosition = localPos; //그 좌표를 localPos에 저장, 거기에 hpbar를 출력
    }

}
