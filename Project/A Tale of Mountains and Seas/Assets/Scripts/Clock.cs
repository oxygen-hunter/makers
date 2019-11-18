using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public static Clock Instance;

    private int timeLeft;

    public Text timeText;

    private void Awake()
    {
        Instance = this;
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
        if (timeLeft < timeUse)
        {
            Debug.Log("时辰不够，返回祭坛");
            return false;
        }
        else
        {
            timeLeft -= timeUse;
            return true;
        }
    }
}
