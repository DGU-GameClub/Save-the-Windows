using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickNewGame()
    {
        //게임실행
        StartCoroutine(nnn());
       
        Debug.Log("Game Start");
    }

    public void OnClickSettings()
    {
        //게임설정(배경음악, 기타음향 조절)
        Debug.Log("Settings");
    }

    public void OnClickExit()
    {
        Debug.Log("Exit");

        //플레이 상태 중단
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public IEnumerator nnn(){
        //StartCoroutine 안의 함수가 끝나고 나서 다음 코드 실행
        yield return StartCoroutine(BlackPannel.instance.FadeIn());
        //
        BlackPannel.instance.NextScene("02. Main");
        

    }

}
