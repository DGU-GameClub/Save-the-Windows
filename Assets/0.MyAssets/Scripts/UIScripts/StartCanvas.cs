using System.Collections;
using UnityEngine;

public class StartCanvas : MonoBehaviour
{
    public GameObject settingMenu;
    // Start is called before the first frame update
    void Start()
    {

        settingMenu = GameObject.Find("Setting Canvas").transform.Find("Setting Menu").gameObject;
        Screen.SetResolution(1920, 1080, true);
        settingMenu.SetActive(false);
        StartCoroutine(BlackPannel.instance.FadeOut());
        SoundManager.instance.StopBGM();
        SoundManager.instance.PlayBGM("Main", true, 1f);

    }

    public void OnClickStartGame()
    {
        StartCoroutine(GameStart());
    }

    public IEnumerator GameStart()
    {
        yield return StartCoroutine(BlackPannel.instance.FadeIn());
        BlackPannel.instance.NextScene("01. Main");
    }

    public void OnClickSettings()
    {
        settingMenu.SetActive(true);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
