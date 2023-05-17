using System.Net.Http.Headers;
using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject gameOverCanvas;
    public GameObject gameWinCanvas;
    public Spawner spawner;
    
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float setTime = 10.0f;
    int min;
    float sec;
    Store store;

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI lifeText;
    int money;
    int life;
    float initTime;

    public static UIManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        
    }
    void Start() 
    {
        StartCoroutine(BlackPannel.instance.FadeOut());
        countdownText.text = setTime.ToString();
        store = GameObject.Find("Store").GetComponent<Store>();
        initTime = setTime;
    }

    void Update()
    {
        //타이머 -> 정비시간때만 돌아가게. 실제 실행때는 리셋.

        //Spawner 스크립트의 Spawner_state를 사용해야 함 -> 질문
        if (setTime > 0 && spawner.CurentState() == 0)
        {
            setTime -= Time.deltaTime;
            if (setTime >= 60f)
            {
                min = (int)setTime / 60;
                sec = setTime % 60;
                countdownText.text = "남은 시간 : " + min + "분 " + (int)sec + "초";
            }
            else if (setTime < 60f)
                countdownText.text = "남은 시간 : " + "0분 " + (int)setTime + "초";
        }

        if (setTime <= 0)
        {
            countdownText.text = "남은 시간 : 0분 0초";
            //spawner.NextWave();
            setTime = initTime;
        }
        
        //돈
        if (money != GameManagers.instance.Money)
        {
            money = GameManagers.instance.Money;
            moneyText.text = ": " + money.ToString();
        }

        //생명
        if (life != GameManagers.instance.Life)
        {
            life = GameManagers.instance.Life;
            lifeText.text = ": " + life.ToString();
        }
    }

    public void OnClickSettings()
    {
        //설정메뉴키기
        SettingCanvas.instance.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void ResetButtonOn()
    {
        if (store != null && GameManagers.instance.Money >= 10)
        {
            store.DestroyAllTower();
            //리셋버튼 클릭시 -20원으로 설정
            GameManagers.instance.Money -= 10;
        }
    }
    public void OnClickResetButton()
    {
        //게임종료하기 전에 확인 메세지 띄우기
        StartCoroutine(GameOver());
    }

    public void OnClickRestart()
    {
        StartCoroutine(BackToStart());
    }
    public IEnumerator BackToStart()
    {
        yield return StartCoroutine(BlackPannel.instance.FadeIn()); 
        BlackPannel.instance.NextScene("00.Start");
    }
    public IEnumerator GameOver()
    {
        yield return StartCoroutine(BlackPannel.instance.FadeIn());
        mainCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        yield return StartCoroutine(BlackPannel.instance.FadeOut());
    }

    public IEnumerator GameWin()
    {
        yield return StartCoroutine(BlackPannel.instance.FadeOut());
        gameWinCanvas.SetActive(true);
        StartCoroutine(BackToStart());
    }
}