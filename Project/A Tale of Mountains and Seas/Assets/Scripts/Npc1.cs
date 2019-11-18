using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc1 : MonoBehaviour
{
    static public Npc1 Instance; 

    public GameObject msgbox;

    string[] contents;

    public int index;

    public Text content;

    public bool meet = false;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //隐藏对话框，预加载对话内容，重置对话下标
        //msgbox = GameObject.FindGameObjectWithTag("Msgbox");
        if (msgbox)
        {
            msgbox.SetActive(false);
            contents = new string[3];
            contents[0] = "hello";
            contents[1] = "i am npc1";
            contents[2] = "who r u";
            index = 0;
        }
        else
        {
            Debug.Log("can't find" + "msxbox");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        
    }

    public void MeetNpc()
    {
        meet = true;
        msgbox.SetActive(true);
    }

    public void ClickConfirmBtn()
    {
        //确定按钮的槽函数，根据index，更新text/开始游戏/结束游戏根据返回值更新text
        content.text = contents[index];
        index++;
        if (index == contents.Length)
        {   //TODO:启动游戏
            index = 0;
            Debug.Log("npc game start");
        }
    }

    public void ClickCancelBtn()
    {
        //index清零，隐藏对话框
        index = 0;
        msgbox.SetActive(false);
    }
}
