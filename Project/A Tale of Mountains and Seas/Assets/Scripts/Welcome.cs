using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnAbout;
    public Button btnExit;

    // Start is called before the first frame update
    void Start()
    {
        btnStart.onClick.AddListener(StartGame);
        btnExit.onClick.AddListener(ExitGame);
        btnSetting.onClick.AddListener(SettingGame);
        btnAbout.onClick.AddListener(AboutGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("MainMap");
    }

    public void SettingGame()
    {

    }

    public void AboutGame()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
