using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public bool isPlayerBullet;

    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        // Debug.Log("huaQ");
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        switch (collision.tag)
        {
            case "Air":
                // 子弹打在空气墙上，子弹销毁
                Destroy(gameObject);
                break;
            case "Player":
                if (!isPlayerBullet)
                {
                    collision.SendMessage("GetHurt");
                    Destroy(gameObject);
                }
                break;
            case "Boss":
                if (isPlayerBullet)
                {
                    collision.SendMessage("GetHurt");
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }
}
