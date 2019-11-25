using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    public Button StartBtn;
    public Button SettingBtn;
    public Button AboutBtn;
    public Button ExitBtn;

    public GameObject AboutPanel;
    public Button AboutCloseBtn;

    // Start is called before the first frame update
    void Start()
    {
        StartBtn.onClick.AddListener(StartGame);
        ExitBtn.onClick.AddListener(ExitGame);
        SettingBtn.onClick.AddListener(SettingGame);
        AboutBtn.onClick.AddListener(ShowAbout);

        AboutCloseBtn.onClick.AddListener(HideAbout);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Introduction");
    }

    public void SettingGame()
    {

    }

    public void ShowAbout()
    {
        if (AboutPanel.activeInHierarchy)
        {
            AboutPanel.SetActive(false);
        }
        else
        {
            AboutPanel.SetActive(true);
        }
    }

    public void HideAbout()
    {
        AboutPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
