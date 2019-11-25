using UnityEngine;

public class BiFang : Boss
{
    // 毕方
    protected override void Start()
    {
        // 毕方非常敏捷，移动速度快
        HP = 6;
        moveSpeed = 7;
        moveTimeMax = 0.8f;
        attackTimeMax = 1.2f;
        skillCD = 80;
    }

    protected override void Skill()
    {
        if (skillFlag == skillCD - 1)
        {
            // 毕方发射三发弹幕
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, attackAngle - 5));
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, attackAngle));
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, attackAngle + 5));
            canAttack = false;
        }
    }
}
