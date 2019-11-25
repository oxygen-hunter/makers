using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public int nowSceneId;

    public int nextSceneId;

    public int timeUse;

    public GameObject Dialog; //对话框

    public Text NextDescription; //名称

    public Button PassBtn; //前往XX

    public Button CancelBtn; //取消

    public Text PassBtnText; //前往XX

    public static string[] SceneName = 
    {
        "MainMap",
        "Home",
        "Altar",
        "SubMap1",
        "SubMap2",
        "SubMap3",
        "SubMap4",
        "SubMap5",
        "SubMap6",
        "SubMap7",
        "SubMap8"
    };

    public static string[] PassBtnTexts =
    {
        "主地图",
        "部落家中",
        "部落祭坛",
        "发鸠山",
        "章莪山",
        "柜山",
        "炎帝部落",
        "青丘",
        "黄山",
        "玉山",
    };

    public static string[] Descriptions =
    {
        "一道神奇的传送门，似乎可以把你传送回 [主地图]",
        "一道神奇的传送门，似乎可以把你送回 [部落的家中]",
        "一道神奇的传送门，似乎可以把你送到 [部落的祭坛]",
        "一道神奇的传送门，似乎可以把你送到 [发鸠山]，需要花费2个时辰",
        "一道神奇的传送门，似乎可以把你送到 [章莪山]，需要花费2个时辰",
        "一道神奇的传送门，似乎可以把你送到 [柜山]，需要花费2个时辰",
        "一道神奇的传送门，似乎可以把你送到 [炎帝部落]，需要花费2个时辰",
        "一道神奇的传送门，似乎可以把你送到 [青丘]，需要花费2个时辰",

        "一道神奇的传送门，似乎可以把你送到 [黄山]，需要花费2个时辰",
        "一道神奇的传送门，似乎可以把你送到 [玉山]，需要花费2个时辰",

    };

    Dictionary<string, int> SceneDic = new Dictionary<string, int>();

    //记录主角应该到的场景中的位置，初始为部落0，大地图0, 各个submap传送门
    //穿过一个传送门后将本场景的位置更新为穿过时人物的位置
    static public Vector3[] nextPosition = {
        new Vector3(-8.5f,2.2f,-1),         //主地图
        new Vector3(0,-3.7f,-1),            //家
        new Vector3(0,0,-1),                //祭坛
        new Vector3(-4, 0f, -1),            //发鸠山
        new Vector3(-4, -0.24f, -1),        //章莪山
        new Vector3(-4, -0.24f, -1),        //柜山
        new Vector3(4.6f, -1f, -1),         //炎帝部落
        new Vector3(-1.8f, 0.76f, -1),      //青丘

        new Vector3(-4, -0.24f, -1),
        new Vector3(-4, -0.24f, -1),
    };

    // Start is called before the first frame update
    void Start()
    {
        //初始化传送去的地方
        nowSceneId = SceneManager.GetActiveScene().buildIndex - 2; //0号是welcome,1号是introduction
        InitSceneDic();
        string nextSceneName = gameObject.name;
        int prefixLength = "Portal".Length;
        nextSceneName = gameObject.name.Substring(prefixLength, nextSceneName.Length - prefixLength);
        nextSceneId = SceneDic[nextSceneName];

        Debug.Log(nextSceneName);
        Debug.Log(nextSceneId);

        //初始化对话框，并隐藏
        NextDescription.text = "一道神奇的传送门，可以把你送到 ["+ PassBtnTexts[nextSceneId]+"]\n"
                                + (timeUse == 0 ? "" : "需要花费" + timeUse + "个时辰");//Descriptions[nextSceneId];
        PassBtnText.text = "前往" + PassBtnTexts[nextSceneId];
        PassBtn.onClick.AddListener(PassPortal);
        CancelBtn.onClick.AddListener(CancelPortal);
        Dialog.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //走到传送门口，显示弹窗，禁止移动
    void MeetPortal()
    {
        Dialog.SetActive(true);
        Player.Instance.allowAction = false;
    }

    //传送到下一场景，允许移动
    public void PassPortal()
    {
        Dialog.SetActive(false);
        Player.Instance.allowAction = true;

        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Player"));
        DontDestroyOnLoad(GameObject.Find("MainUI"));
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Npc"));

        //消耗时间
        if (Clock.Instance.ShortenTime(timeUse))
        {
            //记录离开的位置
            nextPosition[nowSceneId] = Player.Instance.gameObject.transform.position;
            //重置主角位置
            Player.Instance.gameObject.transform.position = nextPosition[nextSceneId];
            //切场景
            SceneManager.LoadSceneAsync(SceneName[nextSceneId]);
        }
    }

    //取消传送，允许移动
    void CancelPortal()
    {
        Player.Instance.allowAction = true;
        Dialog.SetActive(false);
    } 

    void InitSceneDic()
    {
        for (int i = 0; i < SceneName.Length; i++)
        {
            SceneDic.Add(SceneName[i], i);
        }
    }

    
}
