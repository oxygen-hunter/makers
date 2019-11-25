using UnityEngine;

public class Zheng : Boss
{
    // 狰
    protected override void Start()
    {
        // 狰非常凶残，攻击间隔缩短
        sr = GetComponent<SpriteRenderer>();
        HP = 6;
        moveSpeed = 4;
        moveTimeMax = 1.6f;
        attackTimeMax = 0.8f;
        skillCD = 128;
    }

    protected override void Skill()
    {
        if (skillCD - 16 <= skillFlag && skillFlag < skillCD)
        {
            // 狰发射螺旋形弹幕
            if (skillFlag % 4 == 0)
            {
                float skillAngle = (skillFlag - (skillCD - 16) - 8);
                Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, skillAngle + attackAngle));
            }
            canAttack = false;
        }
    }
}