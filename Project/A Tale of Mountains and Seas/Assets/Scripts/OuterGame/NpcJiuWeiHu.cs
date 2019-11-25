using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NpcJiuWeiHu : Npc
{
    static public bool isWin;

    protected override void Start()
    {
        base.Start();

        NpcName = "九尾狐";
        timeUse = 0;
        ItemName = new string[] { "辟邪丹" };
        ItemGiveName = new string[] { "九尾之魂" };

        NpcHead = Resources.Load<Sprite>("Npc/" + NpcName + "头像");
        PlayerHead = Resources.Load<Sprite>("Npc/" + "主角头像");

        talkContent = new Tuple<Turn, string, string, string>[] {
            new Tuple<Turn, string, string, string>(Turn.Npc, "（青丘国，有九尾狐，鸣声如婴儿啼哭，其肉辟邪。）", "继续", "离开"),
            new Tuple<Turn, string, string, string>(Turn.Npc, "真是俊俏的小哥，来找我有什么事？", "聊聊天", "离开"),
            new Tuple<Turn, string, string, string>(Turn.Player, "听闻九尾一族能驱南方之邪气，在下为求辟邪丹而来。", "继续", "离开"),
            new Tuple<Turn, string, string, string>(Turn.Npc, "（听着你讲述族里变故，九尾狐一改初见时的轻佻之色，愈发凝重。）", "继续", "离开"),
            new Tuple<Turn, string, string, string>(Turn.Npc, "天地不仁，以万物为刍狗。奉身为人之事都是有代价的。回想这千百年，我所爱之人欺我、负我，因见了我的真身而畏我……我原以为人类都是那样的自私，没想到也有令母这样的人物。我愿献上自己的灵魂，用于你最后的祭祀。", "接受", "拒绝"),
            new Tuple<Turn, string, string, string>(Turn.Choose, "", "", ""),

            new Tuple<Turn, string, string, string>(Turn.GameLose, "获得道具：九尾之魂×1", "欣然收下", "转身离开"),
            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

            new Tuple<Turn, string, string, string>(Turn.GameWin, "获得道具：辟邪丹×1", "惊讶收下", "转身离开"),
            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

            new Tuple<Turn, string, string, string>(Turn.ReMeet, "（青丘国，有九尾狐，鸣声如婴儿啼哭，其肉辟邪。）", "感谢馈赠", "潇洒离去"),
            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

        };
    }

    protected override int NpcGame()
    {
        //TODO:精卫抽积木游戏
        return 1;
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
