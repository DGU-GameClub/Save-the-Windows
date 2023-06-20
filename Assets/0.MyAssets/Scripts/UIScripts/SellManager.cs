using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class SellManager : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public bool ClickOn = false;
    public GameObject BackGround;
    public GameObject Healthbar;
    public TextMeshProUGUI CurrentTowerNumberText;
    public GameObject[] HealthUI;
    int Fastindex = 0;
    float TempTimeSpeed = 0f;
    bool ClickPause = false;

    public int Tutoindex = 0;
    public GameObject TutoBackgorund;
    public GameObject[] TutoImageList;

    public GameObject BossUI;
    public Image CurrentBossImage;
    public Sprite[] BossImages;
    public GameObject MoveUI;
    int currentStage;
    public bool isBossUI = false;
    public Transform[] bosstransforms; 

    public Button FastButton;
    public Sprite play1;
    public Sprite play2;
    public Sprite play3;
    public void SettingSellMode() {
        if (!ClickOn)
        {
            Healthbar.SetActive(false);
            BackGround.SetActive(true);
            GameManagers.instance.SellMode = true;
            ClickOn = true;
            StartCoroutine(StartAlpha());
            
        }
        else if (ClickOn)
        {
            Healthbar.SetActive(true);
            BackGround.SetActive(false);
            GameManagers.instance.SellMode = false;
            ClickOn = false;
            StopCoroutine(StartAlpha());
        }
    }
    IEnumerator StartAlpha() {
        while (true) {
            Text.CrossFadeAlpha(0.6f, 1.2f, false);
            yield return new WaitForSeconds(1.2f);
            Text.CrossFadeAlpha(1f,1.2f,false);
            yield return new WaitForSeconds(1.2f);
        }
    }
    public void UpdateHealthBarUI() {
        int TempNumber = GameManagers.instance.GetTowerNumber();
        if (TempNumber < 5) {
            for (int i = 0; i < 5; i++)
                HealthUI[i].SetActive(false);
        }
        else if (TempNumber >= 5 && TempNumber < 10) {
            HealthUI[0].SetActive(true);
            for (int i = 1; i < 5; i++)
                HealthUI[i].SetActive(false);
        }
        else if (TempNumber >= 10 && TempNumber < 15) {
            for (int i = 0; i < 2; i++)
                HealthUI[i].SetActive(true);
            for (int i = 2; i < 5; i++)
                HealthUI[i].SetActive(false);
        }
        else if (TempNumber >= 15 && TempNumber < 20) {
            for (int i = 0; i < 3; i++)
                HealthUI[i].SetActive(true);
            for (int i = 3; i < 5; i++)
                HealthUI[i].SetActive(false);
        }
        else if (TempNumber >= 20 && TempNumber < 25) {
            for (int i = 0; i < 4; i++)
                HealthUI[i].SetActive(true);
                HealthUI[4].SetActive(false);
        }
        else if (TempNumber >= 25) {
            for (int i = 0; i < 5; i++)
                HealthUI[i].SetActive(true);
        }
        CurrentTowerNumberText.text = "현재 설치된 타워 수 : " + TempNumber.ToString();
    }
    public void FastMode() {
        Image FastImage = FastButton.GetComponent<Image>();
        if (Time.timeScale == 0f) return;
        if (Fastindex == 0)
        {
            Time.timeScale = 2f;
            FastImage.sprite = play2;

        }
        else if (Fastindex == 1)
        {
            Time.timeScale = 3f;
            FastImage.sprite = play3;
        }
        else if (Fastindex == 2) { Fastindex = 0; Time.timeScale = 1f; FastImage.sprite = play1; return; }
        Fastindex++;
    }
    public void pauseGame() {
        if (!ClickPause)
        {
            TempTimeSpeed = Time.timeScale;
            Time.timeScale = 0f;
            ClickPause = true;
        }
        else if (ClickPause) { 
            Time.timeScale = TempTimeSpeed;
            ClickPause = false;
        }
    }

    public void TutorialStart() {
        TutoBackgorund.SetActive(true);
        Tutoindex = 0;
        NextTuto();
    }
    public void NextTuto() {
        if (Tutoindex == 0) TutoImageList[Tutoindex++].SetActive(true);
        else if (Tutoindex < 8) { TutoImageList[Tutoindex - 1].SetActive(false); TutoImageList[Tutoindex++].SetActive(true); }
        else {
            TutoImageList[Tutoindex - 1].SetActive(false);
            TutoBackgorund.SetActive(false);
            SoundManager.instance.StopBGM();
            SoundManager.instance.PlayBGM("NonBoss", true, 0.3f);
        }
    }
    public void PreviousTuto() {
        if (Tutoindex <= 1) { return; }
        else if (Tutoindex < 9) { TutoImageList[--Tutoindex].SetActive(false); TutoImageList[Tutoindex-1].SetActive(true); }
    }

    public void BossSpawnUI() {
        if (!isBossUI)
        {
            CurrentBossImage.sprite = BossImages[currentStage];
            MoveUI.transform.DOMove(bosstransforms[0].position, 1f).SetEase(Ease.OutQuad);
            isBossUI = true;
        }
        else if (isBossUI) {
            MoveUI.transform.DOMove(bosstransforms[1].position, 1f).SetEase(Ease.OutQuad);
            isBossUI = false;
        }
    }
    public void BossStageStart(int i) { 
        BossUI.SetActive(true);
        currentStage = i;
        BossSpawnUI();
        Invoke(nameof(FirstBossUI), 10f);
    }
    public void BossStageEnd() {
        BossUI.SetActive(false);
    }
    public void FirstBossUI() { 
        BossSpawnUI();
    }
}
