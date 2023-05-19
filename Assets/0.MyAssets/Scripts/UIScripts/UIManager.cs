using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public TextMeshProUGUI[] probabilityText;
    
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
        countdownText.text = "WAVE : " + spawner.waveIndex;
        probabilityText[0].text = GameManagers.instance.probability10.ToString() + "%";
        probabilityText[1].text = GameManagers.instance.probability20.ToString() + "%";
        probabilityText[2].text = GameManagers.instance.probability30.ToString() + "%";
        probabilityText[3].text = GameManagers.instance.probability40.ToString() + "%";
        probabilityText[4].text = GameManagers.instance.probability50.ToString() + "%";

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
            //리셋버튼 클릭시 -10원으로 설정
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

    public void ColoringBox(int price, Image target)
    {
        Color skyblue;
        Color orange = new Color(1f, 0.5f, 0f, 1f);
        Color darkgreen;
        Color lightgray;

        ColorUtility.TryParseHtmlString("#0091FF", out skyblue);
        ColorUtility.TryParseHtmlString("#00BA17", out darkgreen);
        ColorUtility.TryParseHtmlString("#9D9D9D", out lightgray);

        switch(price){
            case 10:
                target.color = lightgray;
                break;
            case 20:
                target.color = skyblue;
                break;
            case 30:
                target.color = darkgreen;
                break;
            case 40:
                target.color = orange;
                break;
            case 50:
                target.color = Color.red;
                break;
        }
    }
}