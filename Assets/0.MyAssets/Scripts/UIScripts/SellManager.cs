using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SellManager : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public bool ClickOn = false;
    public GameObject BackGround;
    public GameObject Healthbar;
    public TextMeshProUGUI CurrentTowerNumberText;
    public GameObject[] HealthUI;
    public TextMeshProUGUI Fasttext;
    int Fastindex = 0;
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
        if (Fastindex == 0)
        {
            Time.timeScale = 2f;
            Fasttext.text = "x 2";
        }
        else if (Fastindex == 1)
        {
            Time.timeScale = 3f;
            Fasttext.text = "x 3";
        }
        else if (Fastindex == 2) { Fastindex = 0; Time.timeScale = 1f; Fasttext.text = "x 1"; return; }
        Fastindex++;
    }
}
