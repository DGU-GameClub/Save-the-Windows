using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject gameOverCanvas;
    public GameObject gameWinCanvas;
    public GameObject storeInvenCanvas;
    public GameObject enemyHpBarCanvas;

    public Spawner spawner;
    
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float setTime = 10.0f;
    // int min;
    // float sec;
    Store store;

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] TextMeshProUGUI moneyText_GO;
    [SerializeField] TextMeshProUGUI lifeText_GO;
    [SerializeField] TextMeshProUGUI moneyText_GW;
    [SerializeField] TextMeshProUGUI lifeText_GW;
    int money;
    int life;

    public static UIManager instance;
    public TextMeshProUGUI[] probabilityText;
    public TextMeshProUGUI lastWave;
    float Starttime;
    float playTime;
    public TextMeshProUGUI playTimeText_GO;
    public TextMeshProUGUI playTimeText_GW;
    public TextMeshProUGUI mvtNameText;
    public GameObject errorUI;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    void Start() 
    {
        StartCoroutine(BlackPannel.instance.FadeOut());
        countdownText.text = setTime.ToString();
        store = GameObject.Find("Store").GetComponent<Store>();
        gameOverCanvas.SetActive(false);
        gameWinCanvas.SetActive(false);
        errorUI.SetActive(false);
        Starttime = Time.realtimeSinceStartup;
    }

    void Update()
    {
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
        if (GameManagers.instance.Money < 10)
        {
            ShowErrorMessage();
            return;
        }
        
        if (store != null)
        {
            store.DestroyAllTower();
            //리셋버튼 클릭시 -10원으로 설정
            GameManagers.instance.Money -= 10;
        }
    }
    public void OnClickResetButton()
    {
        //게임종료하기 전에 확인 메세지 띄우기
        StartCoroutine(GameOver(GameManagers.instance.Life));
    }

    public void OnClickBack()
    {
        StartCoroutine(BackToMain());
    }
    public IEnumerator BackToMain()
    {
        yield return StartCoroutine(BlackPannel.instance.FadeIn()); 
        BlackPannel.instance.NextScene("00.Start");
    }

    public void OnClickReStart(){
        StartCoroutine(ReStart());
    }

    public IEnumerator ReStart(){
        yield return StartCoroutine(BlackPannel.instance.FadeIn());
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public IEnumerator GameOver(int life)
    {
        yield return StartCoroutine(BlackPannel.instance.FadeIn());
        playTime = Time.realtimeSinceStartup - Starttime;
        mainCanvas.SetActive(false);
        storeInvenCanvas.SetActive(false);
        
        lifeText_GO.text = "남은 생명: " + life.ToString();
        moneyText_GO.text = "남은 돈: " + GameManagers.instance.Money.ToString();
        lastWave.text = "마지막 단계: " + spawner.waveIndex.ToString();
        playTimeText_GO.text = "소요 시간: " + getTimeText(playTime);

        gameOverCanvas.SetActive(true);
        yield return StartCoroutine(BlackPannel.instance.FadeOut());
    }

    public IEnumerator GameWin()
    {
        yield return StartCoroutine(BlackPannel.instance.FadeIn());
        playTime = Time.realtimeSinceStartup - Starttime;

        GameObject mvt = GameManagers.instance.GetMostKillTower();
        mainCanvas.SetActive(false);
        storeInvenCanvas.SetActive(false);

        lifeText_GW.text = "남은 생명: " + GameManagers.instance.Life.ToString();
        moneyText_GW.text = "남은 돈: " + GameManagers.instance.Money.ToString();
        playTimeText_GW.text = "소요 시간: " + getTimeText(playTime);
        
        if (mvt == null) {
            mvtNameText.text = "MVT: 없습니다"; 
        } else {
            mvtNameText.text = "MVT: " + mvt.GetComponentInChildren<TowerUnit>().UnitName.ToString();
        }
        
        gameWinCanvas.SetActive(true);
        yield return StartCoroutine(BlackPannel.instance.FadeOut());
    }

    //테두리 색 정하기
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

    private String getTimeText(float time)
    {
        String minS;
        String secS;
        float min = Mathf.Floor(time / 60);
        float sec = Mathf.RoundToInt(time % 60);

        minS = min.ToString();
        secS = Mathf.RoundToInt(sec).ToString();

        return(string.Format("{0}분 {1}초", minS, secS));
    }

    public void ShowErrorMessage()
    {
        TextMeshProUGUI errorMessage = errorUI.GetComponentInChildren<TextMeshProUGUI>();
        errorUI.SetActive(true);
        errorMessage.text = "돈이 부족하여\n구매할 수 없습니다!";
        //0.5초 후에 함수 실행. 비동기.
        Invoke("tryInvoke", 0.65f);
    }

    void tryInvoke()
    {
        errorUI.SetActive(false);
    }
}