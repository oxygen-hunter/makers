using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Introduction : MonoBehaviour
{
    public Button nextBtn;

    public GameObject[] Pages;

    int id;

    // Start is called before the first frame update
    void Start()
    {
        id = 0;
        nextBtn.onClick.AddListener(NextPage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NextPage()
    {
        if (id < Pages.Length)
        {
            Pages[id].SetActive(false);
            id++;
            if (id >= Pages.Length)
            {
                SceneManager.LoadScene("MainMap");
            }
            else
            {
                Pages[id].SetActive(true);
            }
        }
        else
        {
            SceneManager.LoadScene("MainMap");
        }
    }
}
