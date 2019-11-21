using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public int battleResult;
    public int lastSceneIdx;
    public string lastBossName;

    static public Player Instance;
    public Vector2 targetPosition = Vector2.zero;//用来保存目标位置
    public bool isMoving = false;//用来判断是否鼠标移动
    public bool isMovingToNpc = false; //是否正向Npc移动
    public GameObject NpcMovingTo;
    public bool isMovingToPortal = false; //是否正向传送门移动
    public GameObject PortalMovingTo;
    private float speed = 2f;
    public bool allowAction = true;

    private SpriteRenderer sr;
    public Sprite[] playerSprite; //上下左右



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            //初始化在这里
            sr = gameObject.GetComponent<SpriteRenderer>();
        }
        ////同一场景的同一物体删了
        //GameObject sameGo = GameObject.Find(this.gameObject.name);
        //if (sameGo.gameObject != this.gameObject)
        //{
        //    Destroy(this.gameObject);
        //}
        //else
        //{   //否则指定单例
        //    Instance = this;
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("Move", 0, 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        if (battleResult != 0)
        {
            var lastBoss = GameObject.Find(Player.Instance.lastBossName);
            if (lastBoss)
            {
                Debug.Log("7777");
                lastBoss.SendMessage("JudgeBattleResult");
                battleResult = 0;
            }
        }
    }

    void Move()
    {
        //Npc交互等时不准移动
        if (allowAction == false)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("touch ui");
                return;
            }
            Debug.Log("moving!");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            bool isCollider = hit.collider != null;
            if (isCollider)
            {
                Debug.Log("hit");
            }
            if (isCollider)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.tag == "Floor")
                {   //点击了地面，走过去
                    Debug.Log("hit floor");
                    isMoving = true;
                    isMovingToNpc = false;
                    isMovingToPortal = false;
                    //LookAtTarget(hit.point);//使物体朝向目标点
                    targetPosition = hit.point;
                }
                else if (hit.collider.tag == "Npc")
                {   //点击了Npc，调对应Npc的msgbox
                    Debug.Log("hit Npc");
                    //走向npc
                    isMoving = true;
                    isMovingToNpc = true;
                    isMovingToPortal = false;
                    NpcMovingTo = hit.collider.gameObject;
                    //targetPosition = hit.point;
                    targetPosition = hit.collider.bounds.ClosestPoint(transform.position);
                    //TODO:npc身上的脚本名怎么确定
                    //hit.collider.gameObject.GetComponent<Npc1>().msgbox.SetActive(true);
                }
                else if (hit.collider.tag == "Portal")
                {   //点击了传送门，提示，切换地图
                    isMoving = true;
                    isMovingToNpc = false;
                    isMovingToPortal = true;
                    PortalMovingTo = hit.collider.gameObject;
                    //targetPosition = hit.point;
                    targetPosition = hit.collider.bounds.ClosestPoint(transform.position);
                }

                //切换主角的图片，下上左右  
                float v = targetPosition.x - transform.position.x; //左右
                float h = targetPosition.y - transform.position.y; //下上
                if (h < 0 && h < v && h < -v)
                {
                    sr.sprite = playerSprite[0];
                }
                else if (h > 0 && h > v && h > -v)
                {
                    sr.sprite = playerSprite[1];
                }
                else if (v < 0 && h < -v && h > v)
                {
                    sr.sprite = playerSprite[2];
                }
                else
                {
                    sr.sprite = playerSprite[3];
                }
            }
            
        }

        if (isMoving)
        {
            float distance = Vector2.Distance(targetPosition, transform.position);
            if (distance > 0.5f)
            {   //距离大于0.5移动
                //transform.position = Vector3.Lerp(targetPosition, transform.position, 5 * Time.deltaTime);

                float x1 = transform.position.x;
                float y1 = transform.position.y;
                float x2 = targetPosition.x;
                float y2 = targetPosition.y;
                Vector3 dir = new Vector3(x2 - x1, y2 - y1, 0).normalized;
                transform.position += dir * Time.deltaTime * speed;
            }
            else
            {   //否则停止移动
                isMoving = false;
                if (isMovingToNpc)
                {   //到了Npc面前了
                    isMovingToNpc = false;
                    NpcMovingTo.SendMessage("MeetNpc");
                }
                if (isMovingToPortal)
                {   //到了传送门门口
                    isMovingToPortal = false;
                    PortalMovingTo.SendMessage("PassPortal");
                }
            }
        }
    }

    //void LookAtTarget(Vector3 hitPoint)
    //{
    //    targetPosition = hitPoint;
    //    targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
    //    this.transform.LookAt(targetPosition);
    //}
}
