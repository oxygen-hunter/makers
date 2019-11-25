using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    static public Help Instance;
    public GameObject HelpPanel; //拖动赋值
    public Button OpenBtn; //拖动赋值
    public Button CloseBtn; //拖动赋值

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            //打开按钮监听事件
            OpenBtn.onClick.AddListener(ShowHelp);
            CloseBtn.onClick.AddListener(HideHelp);
            //一开始不隐藏帮助
            ShowHelp();
            //HideHelp();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowHelp()
    {
        if (HelpPanel.activeInHierarchy)
        {   //如果开着，就关了
            HelpPanel.SetActive(false);
        }
        else
        {   //否则打开物品栏
            HelpPanel.SetActive(true);
        }
    }

    void HideHelp()
    {
        HelpPanel.SetActive(false);
    }
}
