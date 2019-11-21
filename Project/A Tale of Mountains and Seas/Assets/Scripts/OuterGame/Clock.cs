using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour
{
    public static Clock Instance;

    private int timeLeft;

    public Text timeText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        ////同一场景的同一物体删了
        //GameObject sameGo = GameObject.Find(this.gameObject.name);
        //if (sameGo.gameObject != this.gameObject)
        //{
        //    Destroy(this.gameObject);
        //}
        //else
        //{   //否则指定单例
        //    Instance = this;
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 12;
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = "剩余 " + timeLeft.ToString() + " 时辰";
    }

    public bool ShortenTime(int timeUse)
    {
        if (timeLeft <= timeUse)
        {
            timeLeft = 0;
            Debug.Log("时辰不够，返回祭坛");
            SceneManager.LoadSceneAsync("Altar");
            return false;
        }
        else
        {
            timeLeft -= timeUse;
            return true;
        }
    }
}
