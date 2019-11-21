using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Altar : MonoBehaviour
{
    static public Altar Instance;
    public List<Item> Items;
    public Image[] ItemImages; //拖动赋值
    public Button TryBtn; //拖动赋值

    public GameObject EndingPanel; //拖动赋值
    public Button NextBtn; //拖动赋值
    public GameObject[] Endings; //不同结局
    int EndingIndex;

    bool isGameEnd = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
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
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        Items = new List<Item>();
        //添加物品监听事件
        for (int i = 0; i < ItemImages.Length; i++)
        {
            int temp = i; //处理delegate的问题，二次赋值
            ItemImages[i].GetComponent<Button>().onClick.AddListener(delegate ()
            {
                ItemToBackpack(temp);
            });
        }
        //添加招魂按钮监听事件
        TryBtn.onClick.AddListener(delegate ()
        {
            Spiritualism();
        });
        NextBtn.onClick.AddListener(ShowEnding);
        ////测试
        //Items.Add(new Item(1, "", "", ""));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(int itemId)
    {
        //TODO:根据id添加物品，显示物品图
        Item itemToAdd = Backpack.Instance.ItemDatabase[itemId];
        if (itemToAdd != null)
        {
            Items.Add(itemToAdd);
            UpdateItemImage();
        }
    }

    public void ItemToBackpack(int index)
    {
        //对应格子的物品还给背包，祭坛item删，更新图片，背包item增，更新图片
        Debug.Log("btn index:" + index);
        if (index < Items.Count)
        {   //格子内得有物品才行
            Item itToReturn = Items[index];
            Items.RemoveAt(index);
            UpdateItemImage();
            Backpack.Instance.AddItem(itToReturn.id);
        }
    }

    //刷新祭坛里的图片显示
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

    public int Spiritualism()
    {
        Debug.Log("尝试招魂");
        if (isGameEnd)
        {
            return -1;
        }

        if (Items.Count == 0)
        {   //TODO:提示框之类的
            Debug.Log("招魂道具太少，无法招魂");
            return 0;
        }
        else
        {
            //TODO:根据物体的不同组合解锁结局，提示玩家是否进入下一周目
            Debug.Log("招魂成功，解锁结局");
            isGameEnd = true;
            EndingPanel.SetActive(true);
            
            if (Items.Count >= 3)
            {
                EndingIndex = 1;
            }
            else
            {
                EndingIndex = 2;
            }

            foreach (var it in Items)
            {
                if (it.name == "九尾之魂")
                {
                    EndingIndex = 1;
                    break;
                }
            }
            return 1;
        }
    }

    public void ShowEnding()
    {
        EndingPanel.SetActive(false);
        Endings[EndingIndex].SetActive(true);
    }
}
