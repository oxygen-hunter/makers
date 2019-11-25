using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCreation : MonoBehaviour
{
    // 用来装饰初始化地图所需物体的数组
    // 0.空气墙
    public GameObject[] item;
    public int battleResult; // 0. 仍在进行，1. 主角胜利，2. 主角失败

    private void Awake()
    {
        battleResult = 0;
        InitMap();
    }

    private void Update()
    {
        if (battleResult!=0)
        {   //游戏结束了
            //var player = GameObject.Find("Player");
            Player.Instance.battleResult = battleResult;
            Player.Instance.gameObject.SetActive(true);
            Player.Instance.allowAction = true;
            MainUI.Instance.gameObject.SetActive(true);
            SceneManager.LoadScene(Player.Instance.lastSceneIdx);
        }
    }

    private void InitMap()
    {

        // 初始化空气墙
        for (int x = -8; x <= 8; x++)
        {
            // 上下
            CreateItem(item[0], new Vector3(x, -6, 0), Quaternion.identity);
            CreateItem(item[0], new Vector3(x, 0, 0), Quaternion.identity);
        }

        for (int y = -6; y <= 0; y++)
        {
            // 上下
            CreateItem(item[0], new Vector3(-8, y, 0), Quaternion.identity);
            CreateItem(item[0], new Vector3(8, y, 0), Quaternion.identity);
        }

        // 左边的石头
        CreateItem(item[0], new Vector3(-1.3f, -4.7f, 0), Quaternion.identity);
        CreateItem(item[0], new Vector3(-2.0f, -4.7f, 0), Quaternion.identity);
        CreateItem(item[0], new Vector3(-1.3f, -4.0f, 0), Quaternion.identity);
        CreateItem(item[0], new Vector3(-2.0f, -4.0f, 0), Quaternion.identity);

        // 右边的石头
        CreateItem(item[0], new Vector3(1.7f, -1.7f, 0), Quaternion.identity);
        CreateItem(item[0], new Vector3(1.3f, -2.4f, 0), Quaternion.identity);
        CreateItem(item[0], new Vector3(2.0f, -2.4f, 0), Quaternion.identity);
    }

    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        // itemPositionList.Add(createPosition);
    }

    private void Victory()
    {
        battleResult = 1;
    }

    private void Defeat()
    {
        battleResult = 2;
    }

}
