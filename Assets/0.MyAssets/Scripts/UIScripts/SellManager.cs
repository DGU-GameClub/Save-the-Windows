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
    public void SettingSellMode() {
        if (!ClickOn)
        {
            
            BackGround.SetActive(true);
            GameManagers.instance.SellMode = true;
            ClickOn = true;
            StartCoroutine(StartAlpha());
            
        }
        else if (ClickOn)
        {
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
}
