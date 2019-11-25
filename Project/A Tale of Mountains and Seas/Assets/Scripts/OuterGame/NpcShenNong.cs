using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NpcShenNong : Npc
{
    static public bool isWin;

    protected override void Start()
    {
        base.Start();

        NpcName = "神农";
        timeUse = 2;
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
                new Tuple<Turn, string, string, string>(Turn.Npc, "原来如此，天灾降世，生灵苦难……我愿意将此鼎借于你，但催动此鼎需要耗费巨大的精力，否则可能焚心烬骨，你得先完成一个试炼。\n\n（试炼会花费 " + timeUse + " 个时辰）", "前往试炼","离开"),

                new Tuple<Turn, string, string, string>(Turn.GameStart, "", "", ""),

                new Tuple<Turn, string, string, string>(Turn.GameWin, "你的力量虽然不大，但也足够催动此鼎。神农鼎在你手里能够发挥三成威力，请务必小心使用。", "感谢借鼎", "道谢离去"),
                new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

                new Tuple<Turn, string, string, string>(Turn.GameLose, "你无法催动此鼎。", "再试一次", "落寞离开"),
                new Tuple<Turn, string, string, string>(Turn.ReGame, "", "", ""),

                new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

                new Tuple<Turn, string, string, string>(Turn.ReMeet, "（姜姓部落首领，善用火，称炎帝，又号神农氏，尝百草。）", "感谢援手", "道谢离去"),
                new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

            };
        }

        
    }

    protected override bool GetWin()
    {
        return isWin;
    }

    protected override void SetWin(bool b)
    {
        isWin = b;
    }
}