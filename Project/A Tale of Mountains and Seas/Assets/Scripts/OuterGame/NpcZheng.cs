using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class NpcZheng : Npc
{
    static bool isWin;

    protected override void Start()
    {
        base.Start();

        NpcName = "狰";
        timeUse = 2;
        ItemName = new string[] { "章莪碧玉" };

        NpcHead = Resources.Load<Sprite>("Npc/" + NpcName + "头像");
        PlayerHead = Resources.Load<Sprite>("Npc/" + "主角头像");

        talkContent = new Tuple<Turn, string, string, string>[] {
            new Tuple<Turn, string, string, string>(Turn.Npc, "（章莪山，大地广漠，寸草不生，还有许多怪兽出没其中。一只狰出现在你的视野里，其声如击石般铿锵，后有五尾，全身赤红，身形似豹。狰眼睛一转，发现了你，对你发起了攻击。）", "战", "逃"),
            new Tuple<Turn, string, string, string>(Turn.GameStart, "", "", ""),

            new Tuple<Turn, string, string, string>(Turn.GameWin,"获取道具：章莪碧玉×1","继续","取消"),
            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

            new Tuple<Turn, string, string, string>(Turn.GameLose, "在狰发动必杀技的前一刻，南木护主，将你传送至了安全点。", "继续", "取消"),
            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),

            new Tuple<Turn, string, string, string>(Turn.End, "", "", ""),
        };
    }

    protected override int NpcGame()
    {
        Player.Instance.lastSceneIdx = SceneManager.GetActiveScene().buildIndex;
        Player.Instance.lastBossName = "Zheng";
        Player.Instance.gameObject.SetActive(false);
        MainUI.Instance.gameObject.SetActive(false);
        SceneManager.LoadScene("BossZheng");
        return 0;// Player.Instance.battleResult;
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
