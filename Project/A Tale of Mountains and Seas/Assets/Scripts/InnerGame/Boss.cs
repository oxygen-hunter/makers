using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // 移动
    protected float moveSpeed;    // Boss的移动速度
    protected float moveTimeVal;  // Boss的移动方向改变的计时器
    protected float moveTimeMax;  // Boss的移动方向改变的间隔
    protected float moveAngle;    // Boss的移动方向，角度制

    // 攻击
    protected float attackTimeVal;  // Boss的攻击的计时器
    protected float attackTimeMax;  // Boss的攻击的间隔
    protected float attackAngle;    // Boss的攻击的方向，角度制

    // 技能
    protected int skillFlag;        // Boss的技能计数器
    protected int skillCD;          // Boss的技能冷却时间
    protected bool canAttack;       // Boss的技能冷却未完成（此时可以攻击）

    public int HP;   // Boss的生命值

    // 资源
    public GameObject bulletPrefab;    // 子弹的预制件               
    public Slider HPStrip;             // 血条
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject player = GameObject.Find("InnerPlayer");
        Vector3 direction = player.transform.position - transform.position;
        attackAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
    }

    private void FixedUpdate()
    {
        // Boss的每个时刻尝试干三件事
        // 移动、技能或攻击
        Move();
        Skill();
        Attack();
        skillFlag = (skillFlag + 1) % skillCD;
    }

    protected virtual void Move()
    {
        // Boss 移动
        if (moveTimeVal >= moveTimeMax)
        {
            moveAngle = Random.Range(0, 16) * 360 / 16;
            moveTimeVal = 0;
        }
        else
        {
            moveTimeVal += Time.fixedDeltaTime;
        }

        float v = Mathf.Sin(moveAngle * Mathf.Deg2Rad);
        float h = Mathf.Cos(moveAngle * Mathf.Deg2Rad);

        transform.Translate(Vector3.up * v * moveSpeed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.right * h * moveSpeed * Time.deltaTime, Space.World);
    }

    protected virtual void Attack()
    {
        if (!canAttack)
        {
            canAttack = true;
            return;
        }
        // Boss 攻击
        if (attackTimeVal >= attackTimeMax)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, attackAngle));
            attackTimeVal = 0;
            // 别换了，蠢死了            
            // sr.sprite = BossSprites[index];
            // index = (index + 1) % BossSprites.Length;
        }
        else
        {
            attackTimeVal += Time.fixedDeltaTime;
        }
    }

    protected virtual void Skill()
    {
        // 啥都不干，由派生类重写
    }

    protected virtual void GetHurt()
    {
        HP--;
        HPStrip.value = HP;
        // Boss受伤损血
        if (HP == 0)
        {
            GameObject mapCreation = GameObject.Find("MapCreation");
            mapCreation.SendMessage("Victory");
            //GameObject.Destroy(gameObject);

        }
        // Destroy Boss也意味着胜利
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
