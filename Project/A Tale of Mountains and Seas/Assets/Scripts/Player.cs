using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 targetPosition = Vector2.zero;//用来保存目标位置
    public bool isMoving = false;//用来判断是否鼠标移动
    public bool isMovingToNpc = false; //是否正向Npc移动
    public GameObject NpcMovingTo;
    public bool isMovingToPortal = false; //是否正向传送门移动
    private float speed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        if (Input.GetMouseButton(0))
        {
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
                    //targetPosition = hit.point;
                    targetPosition = hit.collider.bounds.ClosestPoint(transform.position);
                }
            }
            
        }
        if (isMoving)
        {
            float distance = Vector2.Distance(targetPosition, transform.position);
            if (distance > 0.2f)
            {   //距离大于0.2移动
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
                    //NpcMovingTo.GetComponent<Npc1>().msgbox.SetActive(true);
                    NpcMovingTo.SendMessage("MeetNpc");
                }
                if (isMovingToPortal)
                {
                    isMovingToPortal = false;

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
