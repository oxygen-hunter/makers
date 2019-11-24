using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class NpcLiLi : Npc
{
    static public bool isWin;

    protected override void Start()
    {
        base.Start();

        NpcName = "狸力";
        timeUse = 2;
        ItemName = new string[] { "畏兽之魂" };

        NpcHead = Resources.Load<Sprite>("Npc/" + NpcName + "头像");
        PlayerHead = Resources.Load<Sprite>("Npc/" + "主角头像");

        talkContent = new Tuple<Turn, string, string, string>[] {
            new Tuple<Turn, string, string, string>(Turn.Npc, "（柜山，地面凹凸不平，丘峦层叠。走着走着，你突然被小土堆绊了一跤。小土堆里窜出一只狸力，其外貌似猪，脚上长着鸡距", "继续", "离开"),
            new Tuple<Turn, string, string, string>(Turn.Npc, "狸力汪汪地朝着你叫了两声，似乎对你破坏土堆的行为很不满，狸力对你发起了攻击。\n\n（战斗会花费 " + timeUse + " 个时辰）", "战", "逃"),
            new Tuple<Turn, string, string, string>(Turn.GameStart, "", "", ""),

            new Tuple<Turn, string, string, string>(Turn.GameWin,"你赢得了战斗。\n\n获取道具：畏兽之魂×1","收入囊中","潇洒离去"),
            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

            new Tuple<Turn, string, string, string>(Turn.GameLose, "在狸力发动必杀技的前一刻，南木护主，将你传送至了安全点。", "再次进攻", "快速跑路"),
            new Tuple<Turn, string, string, string>(Turn.ReGame, "", "", ""),

            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

            new Tuple<Turn, string, string, string>(Turn.ReMeet, "小土堆里窜出一只狸力，其外貌似猪，脚上长着鸡距。狸力汪汪地朝着你叫了两声，但似乎在你身上感受到了令它畏惧的气息，狸力缩回了土堆", "胆小鬼", "潇洒离开"),
            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),
        };
    }

    protected override int NpcGame()
    {
        Player.Instance.lastSceneIdx = SceneManager.GetActiveScene().buildIndex;
        Player.Instance.lastBossName = "LiLi";
        Player.Instance.gameObject.SetActive(false);
        MainUI.Instance.gameObject.SetActive(false);
        SceneManager.LoadScene("BossLiLi");
        return 0; // Player.Instance.battleResult;
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
