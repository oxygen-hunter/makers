using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class NpcBiFang : Npc
{
    static bool isWin;


    protected override void Start()
    {
        base.Start();

        NpcName = "毕方";
        timeUse = 2;
        ItemName = new string[] { "火之晶簇" };

        NpcHead = Resources.Load<Sprite>("Npc/" + NpcName + "头像");
        PlayerHead = Resources.Load<Sprite>("Npc/" + "主角头像");

        talkContent = new Tuple<Turn, string, string, string>[] {
            new Tuple<Turn, string, string, string>(Turn.Npc, "来者何人？为何事而来？", "继续", "离开"),
            new Tuple<Turn, string, string, string>(Turn.Player, "我乃独龙族新任族长，为救人性命而来。", "继续", "离开"),
            new Tuple<Turn, string, string, string>(Turn.Npc, "我四周多生异火，人们说我衔火作怪，你就不怕？这样的火如何救人？（毕方仰颈长鸣）", "继续", "离开"),
            new Tuple<Turn, string, string, string>(Turn.Player, "焰火之势，犹如生命的活性，给予人光和热。", "继续", "离开"),
            new Tuple<Turn, string, string, string>(Turn.Npc, "那么请接受这个考验，如若你通过，我将献火于你。", "接受考验", "离开"),
            new Tuple<Turn, string, string, string>(Turn.GameStart, "", "", ""),

            new Tuple<Turn, string, string, string>(Turn.GameWin,"火之力，复苏了。\n\n获得道具：火之晶簇×1","继续","离开"),
            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

            new Tuple<Turn, string, string, string>(Turn.GameLose, "我的火，只能带来苦难……", "继续", "离开"),
            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),
        };
    }

    protected override int NpcGame()
    {
        //TODO:游戏
        Player.Instance.lastSceneIdx = SceneManager.GetActiveScene().buildIndex;
        Player.Instance.lastBossName = "BiFang";
        Player.Instance.gameObject.SetActive(false);
        MainUI.Instance.gameObject.SetActive(false);
        SceneManager.LoadScene("BossBiFang");
        return 0; // Player.Instance.battleResult;
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
