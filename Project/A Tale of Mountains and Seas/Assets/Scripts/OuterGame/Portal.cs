using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int nowSceneId;

    public int nextSceneId;

    public int timeUse;

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

    Dictionary<string, int> SceneDic = new Dictionary<string, int>();

    //记录主角应该到的场景中的位置，初始为部落0，大地图0, 各个submap传送门
    //穿过一个传送门后将本场景的位置更新为穿过时人物的位置
    static public Vector3[] nextPosition = {
        new Vector3(0,0,-1),
        new Vector3(0,0,-1),
        new Vector3(0,0,-1),
        new Vector3(0, -0.24f, -1),
        new Vector3(-4, -0.24f, -1),
        new Vector3(-4, -0.24f, -1),
        new Vector3(-4, -0.24f, -1),
        new Vector3(-4, -0.24f, -1),
        new Vector3(-4, -0.24f, -1),
        new Vector3(-4, -0.24f, -1),
        new Vector3(-4, -0.24f, -1),
    };

    // Start is called before the first frame update
    void Start()
    {
        nowSceneId = SceneManager.GetActiveScene().buildIndex - 2; //0号是welcome
        InitSceneDic();
        string nextSceneName = gameObject.name;
        int prefixLength = "Portal".Length;
        nextSceneName = gameObject.name.Substring(prefixLength, nextSceneName.Length - prefixLength);
        nextSceneId = SceneDic[nextSceneName];

        Debug.Log(nextSceneName);
        Debug.Log(nextSceneId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PassPortal()
    {
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

    void InitSceneDic()
    {
        for (int i = 0; i < SceneName.Length; i++)
        {
            SceneDic.Add(SceneName[i], i);
        }
    }
}
