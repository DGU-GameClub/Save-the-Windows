using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlackPannel : MonoBehaviour
{
    public GameObject FadePannel;
    public static BlackPannel instance;

    private void Awake() {
        if (instance != null){
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    //페이드 인
    public IEnumerator FadeOut() {
        Color c = FadePannel.GetComponent<Image>().color;
        FadePannel.SetActive(true);
        for (float f = 1f; f > 0; f -= 0.02f) {
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        FadePannel.SetActive(false);
    }
    //페이드 인
    public IEnumerator FadeIn() {
        Color c = FadePannel.GetComponent<Image>().color;
        FadePannel.SetActive(true);
        for (float f = 0f; f < 1; f += 0.02f) {
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.01f);
        }
    }
    //다음 씬으로 이동
    public void NextScene(string nextScene) {
        SceneManager.LoadScene(nextScene);
    }
}
