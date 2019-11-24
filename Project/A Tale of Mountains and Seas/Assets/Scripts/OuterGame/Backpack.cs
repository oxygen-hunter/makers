using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Item
{
    public int id { get; private set; }
    public int score { get; private set; }
    public string name { get; private set; }
    public string description { get; private set; }
    public string fromBoss { get; private set; }
    public string fromWhere { get; private set; }


    public Item(int id, int score, string name, string description, string fromBoss, string fromWhere)
    {
        this.id = id;
        this.score = score;
        this.name = name;
        this.description = description;
        this.fromBoss = fromBoss;
        this.fromWhere = fromWhere;
    }
}

public class Backpack : MonoBehaviour
{
    static public Backpack Instance;
    public List<Item> Items;
    public GameObject BackpackPanel; //拖动赋值
    public Button OpenBtn; //拖动赋值
    public Button CloseBtn; //拖动赋值
    public Image[] ItemImages; //拖动赋值

    //Item[] ItemDatabase;
    public Dictionary<int, Item> ItemDatabase;

    Tuple<int, int, string, string, string, string>[] AllItem =
    {
        new Tuple<int, int, string, string, string, string>(0, 20, "西山之木", "西山之木的描述", "精卫", "发鸠山"),
        new Tuple<int, int, string, string, string, string>(1, 20, "精卫之羽", "精卫之羽", "精卫", "发鸠山"),
        new Tuple<int, int, string, string, string, string>(2, 30, "神农鼎", "神农鼎", "神农", "炎帝部落"),
        new Tuple<int, int, string, string, string, string>(3, 40, "五弦琴", "五弦琴的描述", "神农", "炎帝部落"),
        new Tuple<int, int, string, string, string, string>(4, 10, "章莪碧玉", "章莪碧玉的描述", "狰", "章莪山"),
        new Tuple<int, int, string, string, string, string>(5, 20, "畏兽之魂", "畏兽之魂的描述", "狸力", "柜山"),
        new Tuple<int, int, string, string, string, string>(6, 20, "火之晶簇", "火之晶簇的描述", "毕方", "章莪山"),
        new Tuple<int, int, string, string, string, string>(7, 20, "辟邪丹", "辟邪丹的描述", "九尾狐", "青丘"),
        new Tuple<int, int, string, string, string, string>(8, 10, "九尾之魂", "九尾之魂的描述", "九尾狐", "青丘"),

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
            OpenBtn.onClick.AddListener(ShowBackpack);
            CloseBtn.onClick.AddListener(HideBackpack);
            //初始化背包物品数据库
            Items = new List<Item>();
            InitItemDatabase();
            //添加监听事件
            for (int i = 0; i < ItemImages.Length; i++)
            {
                int temp = i; //处理delegate的问题，二次赋值
                ItemImages[i].GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    ItemToAltar(temp);
                });
            }
            //隐藏背包
            HideBackpack();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("backpack start");
        //这部分代码不一定在additem之前调用，然后会出错
        //Items = new List<Item>();
        //InitItemDatabase();
        ////添加监听事件
        //for (int i = 0; i < ItemImages.Length; i++)
        //{
        //    int temp = i; //处理delegate的问题，二次赋值
        //    ItemImages[i].GetComponent<Button>().onClick.AddListener(delegate ()
        //    {
        //        ItemToAltar(temp);
        //    });
        //}
        ////TODO测试祭坛功能
        //AddItem(0);
        //AddItem(1);
        //AddItem(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void ShowBackpack()
    //{
    //    if (GetComponent<CanvasGroup>().alpha == 1 &&
    //       GetComponent<CanvasGroup>().interactable == true &&
    //       GetComponent<CanvasGroup>().blocksRaycasts == true)
    //    {   //如果已经打开了，那么关上
    //        HideBackpack();
    //    }
    //    else
    //    {
    //        GetComponent<CanvasGroup>().alpha = 1;
    //        GetComponent<CanvasGroup>().interactable = true;
    //        GetComponent<CanvasGroup>().blocksRaycasts = true;
    //    }
    //}

    //public void HideBackpack()
    //{
    //    GetComponent<CanvasGroup>().alpha = 0;
    //    GetComponent<CanvasGroup>().interactable = false;
    //    GetComponent<CanvasGroup>().blocksRaycasts = false;
    //}

    //打完npc游戏胜利后调用，或者被祭坛返还物品时调用
    public bool AddItem(int itemId)
    {
        Debug.Log("to add:" + itemId);
        //TODO:根据id添加物品，显示物品图
        Item itemToAdd = ItemDatabase[itemId];
        if (itemToAdd != null)
        {
            Items.Add(itemToAdd);
            UpdateItemImage();
            return true;
        }
        else
        {
            return false;
        }
    }

    //根据名字获得物品
    public bool AddItem(string itemName)
    {
        foreach (var it in ItemDatabase)
        {
            if (it.Value.name == itemName)
            {
                return AddItem(it.Value.id);
            }
        }
        return false;
    }
    
    public void ItemToAltar(int index)
    {
        //在祭坛内时触发
        if (SceneManager.GetActiveScene().name == "Altar")
        {
            //对应格子的物品给祭坛，背包item删，更新图片，祭坛item增，更新图片
            Debug.Log("btn index:" + index);
            if (index < Items.Count)
            {   //格子内得有物品才行
                Item itToReturn = Items[index];
                Items.RemoveAt(index);
                UpdateItemImage();
                Altar.Instance.AddItem(itToReturn.id);
            }
        }
    }

    //public void ShowBackpack()
    //{
    //    if (Backpack.Instance.gameObject.activeInHierarchy)
    //    {   //如果开着，就关了
    //        Backpack.Instance.gameObject.SetActive(false);
    //    }
    //    else
    //    {   //否则打开物品栏
    //        Backpack.Instance.gameObject.SetActive(true);
    //    }
    //}

    //public void HideBackpack()
    //{
    //    Backpack.Instance.gameObject.SetActive(false);
    //}

    public void ShowBackpack()
    {
        if (BackpackPanel.activeInHierarchy)
        {   //如果开着，就关了
            BackpackPanel.SetActive(false);
        }
        else
        {   //否则打开物品栏
            BackpackPanel.SetActive(true);
        }
    }

    public void HideBackpack()
    {
        BackpackPanel.SetActive(false);
    }

    public void InitItemDatabase()
    {
        ItemDatabase = new Dictionary<int, Item>();
        for (int i = 0; i < AllItem.Length; i++)
        {
            ItemDatabase[i] = new Item(AllItem[i].Item1, AllItem[i].Item2, AllItem[i].Item3,
                                        AllItem[i].Item4, AllItem[i].Item5, AllItem[i].Item6);
        }
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

    public bool Contain(string itemName)
    {
        foreach(var it in Items)
        {
            if (it.name == itemName)
            {
                return true;
            }
        }
        return false;
    }

    //清空背包
    public void Clear()
    {
        Items.Clear();
        UpdateItemImage();
    }
}
