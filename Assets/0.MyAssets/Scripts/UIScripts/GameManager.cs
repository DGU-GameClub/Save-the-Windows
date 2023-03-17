using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int invenCount;
    UIManager uiManager;


    public void setCount(int count)
    {
        this.invenCount = count;
    }
    
    private void Start() {
        StartCoroutine(BlackPannel.instance.FadeOut());
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CountMoney(){}

    public void CountLife(){}

    public void GameOver()
    {
        Fade();
        uiManager.GameOverOn();
    }

    public void GameWin()
    {
        Fade();
        uiManager.GameWinOn();
    }

    void Fade()
    {
        // StartCoroutine(BlackPannel.instance.FadeIn());
        // StartCoroutine(BlackPannel.instance.FadeOut());
    }
}
