using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NpcShenNong : Npc
{
    static bool isWin;

    protected override void Start()
    {
        base.Start();

        NpcName = "神农";
        timeUse = 1;
        ItemName = new string[]{ "神农鼎" };
        ItemGiveName = new string[] { "神农鼎", "五弦琴" };

        NpcHead = Resources.Load<Sprite>("Npc/" + NpcName + "头像");
        PlayerHead = Resources.Load<Sprite>("Npc/" + "主角头像");

        if (Backpack.Instance.Contain("精卫之羽"))
        {
            talkContent = new Tuple<Turn, string, string, string>[] {
                new Tuple<Turn, string, string, string>(Turn.Npc, "（姜姓部落首领，善用火，称炎帝，又号神农氏，尝百草。）", "继续", "离开"),
                new Tuple<Turn, string, string, string>(Turn.Player, "拜见神农氏，在下乃独龙族新任族长，为求神农鼎而来。", "继续", "离开"),
                new Tuple<Turn, string, string, string>(Turn.Npc, "这个气息……你是否去过北方发鸠山？", "是的", "离开"),
                new Tuple<Turn, string, string, string>(Turn.Player, "我去过。（你将在发鸠山发生的事情告诉了神农）","继续", "取消"),

                new Tuple<Turn, string, string, string>(Turn.GiveYou, "没想到小女还与你有过这样一段缘分。神灵有生之悯，我愿助你一臂之力。\n\n获取道具：神农鼎×1 五弦琴×1","继续","取消"),
                new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),
            };
        }
        else
        {
            talkContent = new Tuple<Turn, string, string, string>[] {
                new Tuple<Turn, string, string, string>(Turn.Npc, "（姜姓部落首领，善用火，称炎帝，又号神农氏，尝百草。）", "继续", "离开"),
                                new Tuple<Turn, string, string, string>(Turn.Player, "拜见神农氏，在下乃独龙族新任族长，为求物而来。", "继续", "离开"),
                new Tuple<Turn, string, string, string>(Turn.Npc, "昔日我炼制百药，神农鼎便积聚千年来无数灵药之气。其能炼出天界诸神亦无法轻得之旷世神药，并隐藏着其他的神祕力量。这样的神武可不能轻易被借出，请你说说你的故事。", "继续", "离开"),
                new Tuple<Turn, string, string, string>(Turn.Player, "（讲述缘由）","继续", "离开"),
                new Tuple<Turn, string, string, string>(Turn.Npc, "原来如此，天灾降世，生灵苦难……我愿意将此鼎借于你，但催动此鼎需要耗费巨大的精力，否则可能焚心烬骨，你得先完成一个试炼。", "前往试炼","离开"),

                new Tuple<Turn, string, string, string>(Turn.GameStart, "", "", ""),

                new Tuple<Turn, string, string, string>(Turn.GameWin, "你的力量虽然不大，但也足够催动此鼎。神农鼎在你手里能够发挥三成威力，请务必小心使用。", "继续", "离开"),
                new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

                new Tuple<Turn, string, string, string>(Turn.GameLose, "你无法催动此鼎。", "继续", "离开"),
                new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),
            };
        }

        
    }

    protected override int NpcGame()
    {
        //TODO:神农八卦游戏
        return 1;
    }

    protected override bool HaveWin()
    {
        if (isWin == false)
        {
            isWin = true;
            return false;
        }
        else
        {
            return isWin;
        }
    }
}

//public class JingWei : MonoBehaviour
//{
//    static public JingWei Instance; 
//    public string NpcName = "精卫"; //最好读文件
//    public bool meet = false; //应该在player身上，否则这里会销毁
//    static public bool isWin = false;
//    private int timeUse = 3; //手动指定
//    private string[] ItemName = { "西山之木", "精卫之羽" };

//    public GameObject dialog; //拖动赋值
//    public Image HeadImage; //拖动赋值
//    public Text content; //拖动赋值
//    public Text ConfirmBtnText;
//    public Text CancelBtnText;
//    public Sprite NpcHead; //load进来
//    public Sprite PlayerHead;
//    Tuple<Turn, string, string, string>[] talkContent;
//    public int contentIndex; //讲话下标

//    //string[] contents; //讲话内容
//    private void Awake()
//    {
//        Instance = this;
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        talkContent = new Tuple<Turn, string, string, string>[] {
//            new Tuple<Turn, string, string, string>(Turn.Npc, "（北方发鸠山，柘木葱茏，参天的古木上有鸟族少女驻足，远眺东方，其唇如玉，其足为赤，常发“精卫”之声，似乎在呼喊着什么。）", "继续", "取消"),
//            new Tuple<Turn, string, string, string>(Turn.Player, "柘木林中涌动着生命的能量，是不可多得的良材。", "砍伐柘木", "离开"),
//            new Tuple<Turn, string, string, string>(Turn.Player, "（你掏出随身携带的斧头，准备砍下，突然感觉天旋地转，回过神来，你被之前看到的鸟族少女带上了高空，脚下便是万丈层林，山色这样好，你却无心欣赏。）","继续", "取消"),
//            new Tuple<Turn, string, string, string>(Turn.Npc, "大胆！柘木有灵，不可随意损伤！","继续", "取消"),
//            new Tuple<Turn, string, string, string>(Turn.Player, "我错了错了，再也不敢了，仙女姐姐快放我下来！","继续", "取消"),
//            new Tuple<Turn, string, string, string>(Turn.Npc, "说吧，你为什么要砍树？","继续", "取消"),
//            new Tuple<Turn, string, string, string>(Turn.Player, "这些年来，天灾不断，水浪滔天，凶兽作乱，无数生灵惨遭其害，我要这灵木去救人。（转瞬间你又落在了平地。）","继续", "取消"),
//            new Tuple<Turn, string, string, string>(Turn.Npc, "原来如此，水浪之灾，东海何时才能堙为平地，不再为祸……（少女似乎陷入了痛苦的回忆中）如若是为了救人，你可以带走柘木枝，但我需要考验你是否是心诚之人。","进入考验", "取消"),

//            new Tuple<Turn, string, string, string>(Turn.GameStart, "", "", ""),

//            new Tuple<Turn, string, string, string>(Turn.GameWin,"你果然是心诚之人，这段柘木枝你拿去。我身上的这片羽毛也给你，希望它能保佑你，也愿人间不再有海水之灾。","继续","取消"),
//            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

//            new Tuple<Turn, string, string, string>(Turn.GameLose, "你杂念太多，浮躁之气傍身，还是改日再来吧。", "继续", "取消"),
//            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

//            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),
//        };
//        NpcHead = Resources.Load<Sprite>("Npc/" + NpcName + "头像");
//        PlayerHead = Resources.Load<Sprite>("Npc/" + "主角头像");

//        //隐藏对话框，预加载对话内容，重置对话下标
//        if (dialog)
//        {
//            dialog.SetActive(false);
//            contentIndex = 0;
//        }
//        else
//        {
//            Debug.Log("can't find" + "msgbox");
//        }

//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    public void MeetNpc()
//    {
//        //禁止主角移动
//        Player.Instance.allowAction = false;
//        meet = true;
//        dialog.SetActive(true);
//        //预加载第0条对话
//        contentIndex = 0;
//        UpdateDialog();
//    }

//    public int NpcGame()
//    {
        
//        return 1;
//    }

//    //确定按钮的槽函数，根据index，更新text/开始游戏/结束游戏根据返回值更新text
//    public void ClickConfirmBtn()
//    {
//        contentIndex++;
        
//        //所有对话结束
//        if (contentIndex >= talkContent.Length || talkContent[contentIndex].Item1 == Turn.End)
//        {
//            contentIndex = 0;
//            //关闭对话框
//            ClickCancelBtn();
//            return;
//        }
//        else if (talkContent[contentIndex].Item1 == Turn.GameStart)
//        {   //TODO:启动游戏
//            Debug.Log("npc game start");
//            int result = NpcGame();
//            Debug.Log("npc game end");

//            if (result == 1)
//            {   //获胜
//                Debug.Log("npc game win");
//                //不可重复获得道具
//                if (isWin == false)
//                {
//                    isWin = true;
//                    foreach (var it in ItemName)
//                    {
//                        Backpack.Instance.AddItem(it);
//                    }
//                }
//                contentIndex = JumpToContent(Turn.GameWin);
//            }
//            else
//            {   //失败
//                Debug.Log("npc game lose");
//                contentIndex = JumpToContent(Turn.GameLose);
//            }
            
//            //消耗时间计时
//            Clock.Instance.ShortenTime(timeUse);
//        }

//        //显示对话框
//        UpdateDialog();
//        ////显示对话框
//        //switch (talkContent[contentIndex].Item1)
//        //{
//        //    case (int)(Turn.Npc):
//        //        HeadImage.sprite = NpcHead;
//        //        break;
//        //    case Turn.Player:
//        //        HeadImage.sprite = PlayerHead;
//        //        break;
//        //    default:
//        //        HeadImage.sprite = NpcHead;
//        //        break;
//        //}
//        ////HeadImage.sprite = talkContent[contentIndex].Item1 == Turn.Npc ? NpcHead : PlayerHead;
//        //content.text = talkContent[contentIndex].Item2;
//        //ConfirmBtnText.text = talkContent[contentIndex].Item3;
//        //CancelBtnText.text = talkContent[contentIndex].Item4;
//    }

//    public void ClickCancelBtn()
//    {
//        //index清零，隐藏对话框
//        contentIndex = 0;
//        dialog.SetActive(false);
//        //允许移动
//        Player.Instance.allowAction = true;
//    }

//    int JumpToContent(Turn t)
//    {
//        //跳转到胜利/失败/结束的语句
//        for (int i = contentIndex; i < talkContent.Length; i++)
//        {
//            if (talkContent[i].Item1 == t)
//            {
//                return i;
//            }
//        }
//        return talkContent.Length - 1;
//    }

//    void UpdateDialog()
//    {
//        //显示对话框
//        switch (talkContent[contentIndex].Item1)
//        {
//            case (int)(Turn.Npc):
//                HeadImage.sprite = NpcHead;
//                break;
//            case Turn.Player:
//                HeadImage.sprite = PlayerHead;
//                break;
//            default:
//                HeadImage.sprite = NpcHead;
//                break;
//        }
//        //HeadImage.sprite = talkContent[contentIndex].Item1 == Turn.Npc ? NpcHead : PlayerHead;
//        content.text = talkContent[contentIndex].Item2;
//        ConfirmBtnText.text = talkContent[contentIndex].Item3;
//        CancelBtnText.text = talkContent[contentIndex].Item4;
//    }
//}
