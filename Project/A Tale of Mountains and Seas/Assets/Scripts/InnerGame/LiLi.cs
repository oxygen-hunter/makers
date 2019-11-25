using UnityEngine;

public class LiLi : Boss
{
    // 狸力
    protected override void Start()
    {
        // 狸力，生命值更高
        HP = 8;
        moveSpeed = 3;
        moveTimeMax = 1.6f;
        attackTimeMax = 1.2f;
        skillCD = 128;
        HPStrip.value = HPStrip.maxValue = HP;
    }

    private float skillAngle;

    protected override void Skill()
    {
        if (skillCD - 25 <= skillFlag && skillFlag < skillCD)
        {
            if (skillFlag == skillCD - 25)
                skillAngle = attackAngle;

            // 狸力发射蛇形弹幕
            if(skillFlag == skillCD - 20)
                Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, skillAngle - 3));
            else if (skillFlag == skillCD - 25 || skillFlag == skillCD - 15 || skillFlag == skillCD - 5)
                Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, skillAngle));
            else if(skillFlag == skillCD - 10)
                Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, skillAngle + 3));
            canAttack = false;
        }
    }
}
