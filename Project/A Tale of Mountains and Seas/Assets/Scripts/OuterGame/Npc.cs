using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public enum Turn { Npc, Player, GameStart, GameWin, GameLose, End, GiveYou, Choose };

public class Npc : MonoBehaviour
{
    static protected bool meet = false; //应该在player身上，否则这里会销毁
    //static protected bool isWin = false;

    protected string NpcName; //最好读文件
    protected int timeUse; //手动指定
    protected string[] ItemName;
    protected string[] ItemGiveName;

    //对话框系统
    public GameObject dialog; //拖动赋值
    public Image HeadImage; //拖动赋值
    public Text content; //拖动赋值
    public Button ConfirmBtn; //拖动赋值
    public Button CancelBtn; //拖动赋值
    public Text ConfirmBtnText; //拖动赋值
    public Text CancelBtnText; //拖动赋值
    public Sprite NpcHead; //load进来
    public Sprite PlayerHead; //load进来
    public Tuple<Turn, string, string, string>[] talkContent;
    public int contentIndex; //讲话下标
    
    protected virtual void Awake()
    {

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        ConfirmBtn.onClick.AddListener(ClickConfirmBtn);
        CancelBtn.onClick.AddListener(ClickCancelBtn);

        //隐藏对话框，预加载对话内容，重置对话下标
        dialog.SetActive(false);
        contentIndex = 0;
    }
    

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected virtual void MeetNpc()
    {
        //禁止主角移动
        Player.Instance.allowAction = false;
        meet = true;
        dialog.SetActive(true);
        //预加载第0条对话
        contentIndex = 0;
        UpdateDialog();
    }

    protected virtual bool HaveWin()
    {
        return true;
    }

    protected virtual int NpcGame()
    {
        Player.Instance.lastSceneIdx = SceneManager.GetActiveScene().buildIndex;
        return 1;
    }

    //确定按钮的槽函数，根据index，更新text/开始游戏/结束游戏根据返回值更新text
    protected void ClickConfirmBtn()
    {
        contentIndex++;

        //所有对话结束
        if (contentIndex >= talkContent.Length || talkContent[contentIndex].Item1 == Turn.End)
        {
            contentIndex = 0;
            //关闭对话框
            ClickCancelBtn();
            return;
        }
        else if (talkContent[contentIndex].Item1 == Turn.GameStart)
        {   //TODO:启动游戏
            Debug.Log("npc game start");
            //第一次到这启动游戏，第二次到这？？
            int result = NpcGame();
            Debug.Log("npc game end");

            if (result == 1)
            {   //获胜
                Debug.Log("npc game win");
                //不可重复获得道具
                if (!HaveWin())
                {
                    foreach (var it in ItemName)
                    {
                        Backpack.Instance.AddItem(it);
                    }
                }
                contentIndex = JumpToContent(Turn.GameWin);

                //消耗时间计时
                Clock.Instance.ShortenTime(timeUse);
            }
            else if (result == 2)
            {   //失败
                Debug.Log("npc game lose");
                contentIndex = JumpToContent(Turn.GameLose);

                //消耗时间计时
                Clock.Instance.ShortenTime(timeUse);
            }
        }
        else if (talkContent[contentIndex].Item1 == Turn.GiveYou)
        {   //神农赠礼，不花时间
            if (!HaveWin())
            {
                foreach (var it in ItemGiveName)
                {
                    Backpack.Instance.AddItem(it);
                }
            }
        }
        else if (talkContent[contentIndex].Item1 == Turn.Choose)
        {   //接受九尾赠礼，花时间
            if (!HaveWin())
            {
                foreach (var it in ItemGiveName)
                {
                    Backpack.Instance.AddItem(it);
                }
            }
            contentIndex = JumpToContent(Turn.GameLose);
            //消耗时间计时
            Clock.Instance.ShortenTime(timeUse);
        }

        //显示对话框
        UpdateDialog();
    }

    protected void ClickCancelBtn()
    {
        if (contentIndex + 1 < talkContent.Length && talkContent[contentIndex+1].Item1 == Turn.Choose)
        {   //拒绝九尾赠礼，获得辟邪丹
            if (!HaveWin())
            {
                foreach (var it in ItemName)
                {
                    Backpack.Instance.AddItem(it);
                }
            }
            contentIndex = JumpToContent(Turn.GameWin);
            //消耗时间计时
            Clock.Instance.ShortenTime(timeUse);
            UpdateDialog();
            return;
        }

        //index清零，隐藏对话框
        contentIndex = 0;
        dialog.SetActive(false);
        //允许移动
        Player.Instance.allowAction = true;
    }

    protected int JumpToContent(Turn t)
    {
        //跳转到胜利/失败/结束的语句
        for (int i = contentIndex; i < talkContent.Length; i++)
        {
            if (talkContent[i].Item1 == t)
            {
                return i;
            }
        }
        return talkContent.Length - 1;
    }

    protected void UpdateDialog()
    {
        //显示对话框
        switch (talkContent[contentIndex].Item1)
        {
            case (int)(Turn.Npc):
                HeadImage.sprite = NpcHead;
                break;
            case Turn.Player:
                HeadImage.sprite = PlayerHead;
                break;
            default:
                HeadImage.sprite = NpcHead;
                break;
        }
        //HeadImage.sprite = talkContent[contentIndex].Item1 == Turn.Npc ? NpcHead : PlayerHead;
        content.text = talkContent[contentIndex].Item2;
        ConfirmBtnText.text = talkContent[contentIndex].Item3;
        CancelBtnText.text = talkContent[contentIndex].Item4;
    }

    public void JudgeBattleResult()
    {
        dialog.SetActive(true);
        int result = Player.Instance.battleResult;
        if (result == 1)
        {   //获胜
            Debug.Log("npc game win");
            //不可重复获得道具
            if (!HaveWin())
            {
                foreach (var it in ItemName)
                {
                    Backpack.Instance.AddItem(it);
                }
            }
            contentIndex = JumpToContent(Turn.GameWin);
            Clock.Instance.ShortenTime(timeUse);
        }
        else
        {   //失败
            Debug.Log("npc game lose");
            contentIndex = JumpToContent(Turn.GameLose);
            Clock.Instance.ShortenTime(timeUse);
        }
        UpdateDialog();
    }
}
