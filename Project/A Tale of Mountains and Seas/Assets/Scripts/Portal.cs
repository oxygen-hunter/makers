using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int id;

    public int timeUse;

    public static string[] SceneName = 
    {
        "MainMap",
        "Home",
        "Alter",
        "SubMap1",
        "SubMap2",
        "SubMap3",
        "SubMap4",
        "SubMap5",
        "SubMap6",
        "SubMap7",
        "SubMap8"
    };

    static public Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        
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
        Player.Instance.gameObject.transform.position = new Vector3(0, 0, -1); //重置主角位置

        Clock.Instance.ShortenTime(timeUse); //消耗时间
        SceneManager.LoadSceneAsync(SceneName[id]);
    }
}
