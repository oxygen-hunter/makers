using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerPlayer : MonoBehaviour
{
    // 玩家的移动速度
    public const float moveSpeed = 4;
    // 子弹的射击间隔
    public float attackTimeVal;
    // 子弹的射击方向
    public Vector3 bulletEulerAngles;
    // 子弹的预制件
    public GameObject bulletPrefab;

    public int HP;
    
    // 主角的Sprite列表
    private SpriteRenderer sr;
    public Sprite[] playerSprites;//下 上 左 右


    // Start is called before the first frame update
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        HP = 4;   
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        Vector3 direction = boss.transform.position - transform.position;
        bulletEulerAngles = new Vector3(0,0,Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
    }

    private void FixedUpdate()
    {
        Move();
        Attack();
    }

    void Move()
    {
        float V = Input.GetAxisRaw("Vertical");
        float H = Input.GetAxisRaw("Horizontal");

        float v = 0f, h = 0f;
        if (V != 0f || H != 0f)
        {
            v = V / Mathf.Sqrt(V * V + H * H);
            h = H / Mathf.Sqrt(V * V + H * H);
        }

        // Debug.Log("V:" + v.ToString() + ", H:" + h.ToString());
        transform.Translate(Vector3.up * v * moveSpeed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.right * h * moveSpeed * Time.deltaTime, Space.World);

        if (v < 0)
            sr.sprite = playerSprites[0];
        else if (v > 0)
            sr.sprite = playerSprites[1];

        if (h < 0)
            sr.sprite = playerSprites[2];
        else if (h > 0)
            sr.sprite = playerSprites[3];
    }

    void Attack()
    {
        if (attackTimeVal >= 0.5f && Input.GetKey(KeyCode.Space))
        {
            //子弹产生的角度：bulletEulerAngles
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(bulletEulerAngles));
            attackTimeVal = 0;
        }
        else
        {
            attackTimeVal += Time.fixedDeltaTime;
        }
    }

    void GetHurt()
    {
        // Player受伤损血
        if (HP == 0)
        {
            GameObject mapCreation = GameObject.Find("MapCreation");
            mapCreation.SendMessage("Defeat");
            //GameObject.Destroy(gameObject);
        }
        else
            HP--;
        // Destroy Player也意味着失败
    }

}
