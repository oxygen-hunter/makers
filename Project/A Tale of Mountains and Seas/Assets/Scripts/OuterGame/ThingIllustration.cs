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
        new Tuple<int, int, string, string, string, string>(0, 20, "西山之木", "精卫常衔西山之木石，以堙于东海。", "精卫", "发鸠山"),
        new Tuple<int, int, string, string, string, string>(1, 20, "精卫之羽", "精卫身上的羽毛，熠熠流光。", "精卫", "发鸠山"),
        new Tuple<int, int, string, string, string, string>(2, 30, "神农鼎", "神农昔日炼制百药之古鼎，据说能炼出天界诸神亦无法轻得之旷世神药，并隐藏其他神秘之力量。", "神农", "炎帝部落"),
        new Tuple<int, int, string, string, string, string>(3, 40, "五弦琴", "神农之琴，有宫商角徵羽五弦。", "神农", "炎帝部落"),
        new Tuple<int, int, string, string, string, string>(4, 10, "章莪碧玉", "章莪之山上的瑶玉，似乎是狰的宝贝。", "狰", "章莪山"),
        new Tuple<int, int, string, string, string, string>(5, 20, "畏兽之魂", "柜山畏兽的魂魄，其出现之地大兴土木。", "狸力", "柜山"),
        new Tuple<int, int, string, string, string, string>(6, 20, "火之晶簇", "讹火化灵形成的晶簇，蕴含着毕方的力量。", "毕方", "章莪山"),
        new Tuple<int, int, string, string, string, string>(7, 20, "辟邪丹", "以九尾狐之血炼制而成，可驱万邪。", "九尾狐", "青丘"),
        new Tuple<int, int, string, string, string, string>(8, 10, "九尾之魂", "九尾狐之魂，似有摄人心魄之力。", "九尾狐", "青丘"),

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
