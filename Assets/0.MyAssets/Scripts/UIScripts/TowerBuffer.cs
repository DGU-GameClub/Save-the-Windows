using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuffer : MonoBehaviour
{
    //어떤 타워들이 리스트에 존재하는지 
    //TowerProperty 클래스를 들고 있으면 에디터에서 설정가능
    //TowerUnit 스크립트로 대체 가능한가?
    public List<TowerProperty> towers;
}
