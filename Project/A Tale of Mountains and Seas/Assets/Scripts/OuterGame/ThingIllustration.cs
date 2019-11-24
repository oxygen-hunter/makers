using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class ThingIllustration : MonoBehaviour
{
    static public ThingIllustration Instance;
    public List<Item> Items;
    public GameObject IllustrationPanel; //拖动赋值
    public Button OpenBtn; //拖动赋值
    public Button CloseBtn; //拖动赋值
    public Image[] ItemImages; //拖动赋值
    public Image DetailImage; //拖动赋值
    public Text DetailDescription; //拖动赋值

    //Item[] ItemDatabase;
    public Dictionary<int, Item> ItemDatabase;

    Tuple<int, int, string, string, string, string>[] AllItem = 
    {
        new Tuple<int, int, string, string, string, string>(0, 20, "西山之木", "精卫常衔西山之木石，以堙于东海。漳水出焉，东流注于河", "精卫", "发鸠山"),
        new Tuple<int, int, string, string, string, string>(1, 20, "精卫之羽", "漂亮的羽毛，似乎有神灵的气息", "精卫", "发鸠山"),
        new Tuple<int, int, string, string, string, string>(2, 30, "神农鼎", "焚山煮海，吸天纳地，神器", "神农", "炎帝部落"),
        new Tuple<int, int, string, string, string, string>(3, 40, "五弦琴", "五弦琴的描述", "神农", "炎帝部落"),
        new Tuple<int, int, string, string, string, string>(4, 10, "章莪碧玉", "章莪碧玉的描述", "狰", "章莪山"),
        new Tuple<int, int, string, string, string, string>(5, 20, "畏兽之魂", "畏兽之魂的描述", "狸力", "柜山"),
        new Tuple<int, int, string, string, string, string>(6, 20, "火之晶簇", "火之晶簇的描述", "毕方", "章莪山"),
        new Tuple<int, int, string, string, string, string>(7, 20, "辟邪丹", "辟邪丹的描述", "九尾狐", "青丘"),
        new Tuple<int, int, string, string, string, string>(8, 10, "九尾之魂", "九尾狐的魂魄，据说只有感动九尾狐的人才有资格拿到", "九尾狐", "青丘"),

    };

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
            OpenBtn.onClick.AddListener(ShowIllustration);
            CloseBtn.onClick.AddListener(HideIllustration);
            //初始化背包物品数据库
            Items = new List<Item>();
            ItemDatabase = new Dictionary<int, Item>();
            InitIllstration();
            //添加监听事件
            for (int i = 0; i < ItemImages.Length; i++)
            {
                int temp = i; //处理delegate的问题，二次赋值
                ItemImages[i].GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    ShowDetail(temp);
                });
            }
            //隐藏图鉴
            HideIllustration();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("illustration start");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShowIllustration()
    {
        if (IllustrationPanel.activeInHierarchy)
        {   //如果开着，就关了
            IllustrationPanel.SetActive(false);
        }
        else
        {   //否则打开物品栏
            IllustrationPanel.SetActive(true);
        }
    }

    void HideIllustration()
    {
        IllustrationPanel.SetActive(false);
    }

    public void ShowDetail(int index)
    {
        if (index < Items.Count)
        {
            Sprite sr = Resources.Load<Sprite>("Backpack/" + Items[index].name);
            DetailImage.sprite = sr;
            DetailDescription.text = "名称: " + Items[index].name + "\n" +
                                     "描述: " + Items[index].description;
        }
    }

    public void InitIllstration()
    {
        for (int i = 0; i < AllItem.Length; i++)
        {
            ItemDatabase[i] = new Item(AllItem[i].Item1, AllItem[i].Item2, AllItem[i].Item3,
                                        AllItem[i].Item4, AllItem[i].Item5, AllItem[i].Item6);
            Items.Add(ItemDatabase[i]);
        }
        UpdateItemImage();
        ShowDetail(0);
    }

    //刷新背包里的图片显示
    public void UpdateItemImage()
    {
        for (int i = 0; i < ItemImages.Length; i++)
        {
            ItemImages[i].sprite = null;
        }

        for (int i = 0; i < Items.Count; i++)
        {
            Sprite sr = Resources.Load<Sprite>("Backpack/" + Items[i].name);
            ItemImages[i].sprite = sr;
        }
    }
}
