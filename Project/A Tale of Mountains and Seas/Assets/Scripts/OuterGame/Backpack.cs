using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Item
{
    public int id { get; private set; }
    public string name { get; private set; }
    public string description { get; private set; }
    public string icon { get; private set; }
    public string itemType { get; protected set; }

    public Item(int id, string name, string description, string icon)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.icon = icon;
    }
}

public class Backpack : MonoBehaviour
{
    static public Backpack Instance;
    public List<Item> Items;
    public Image[] ItemImages; //拖动赋值

    //Item[] ItemDatabase;
    public Dictionary<int, Item> ItemDatabase;
    string[] ItemName = {
        "西山之木",
        "精卫之羽",
        "神农鼎",
        "五弦琴",
        "章莪碧玉",
        "畏兽之魂",
        "火之晶簇",
        "辟邪丹",
        "九尾之魂"
    };
    string[] ItemDescription = {
        "西山之木des",
        "精卫之羽des",
        "神农鼎des",
        "五弦琴des",
        "章莪碧玉des",
        "畏兽之魂des",
        "火之晶簇des",
        "辟邪丹des",
        "九尾之魂des"
    };
    string[] ItemIcon = {
        "西山之木",
        "精卫之羽",
        "神农鼎",
        "五弦琴",
        "章莪碧玉",
        "畏兽之魂",
        "火之晶簇",
        "辟邪丹",
        "九尾之魂"
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
            //初始化背包
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
            //Instance.gameObject.SetActive(false);
            HideBackpack();
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
        //    //隐藏背包栏
        //    Instance.gameObject.SetActive(false);
        //}
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

    public void ShowBackpack()
    {
        if (GetComponent<CanvasGroup>().alpha == 1 &&
           GetComponent<CanvasGroup>().interactable == true &&
           GetComponent<CanvasGroup>().blocksRaycasts == true)
        {   //如果已经打开了，那么关上
            HideBackpack();
        }
        else
        {
            GetComponent<CanvasGroup>().alpha = 1;
            GetComponent<CanvasGroup>().interactable = true;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    public void HideBackpack()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

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
    //        Backpack.Instance.gameObject.SetActive(true);Canvas a;a.
    //    }
    //}

    //public void HideBackpack()
    //{
    //    Backpack.Instance.gameObject.SetActive(false);
    //}

    public void InitItemDatabase()
    {
        ItemDatabase = new Dictionary<int, Item>();
        for (int id = 0; id < ItemName.Length; id++)
        {
            ItemDatabase[id] = new Item(id, ItemName[id], ItemDescription[id], ItemIcon[id]);
        }
        //Sprite sr = Resources.Load<Sprite>("Backpack/" + ItemIcon[0]);
        //Debug.Log(sr.name);
        //ItemImages[0].sprite = sr;
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
            Sprite sr = Resources.Load<Sprite>("Backpack/" + Items[i].icon);
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
}
