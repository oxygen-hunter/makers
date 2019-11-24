using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject target;

    private float xBound = 33;

    private float yBound = 23;

    // Start is called before the first frame update
    void Start()
    {
        //if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 tarpos = target.transform.position;
        float x = tarpos.x;
        float y = tarpos.y;
        float z = transform.position.z;
        if (Mathf.Abs(x) < xBound && Mathf.Abs(y) < yBound)
        {   //不准出界
            tarpos.z = z;
            transform.position = Vector3.Lerp(transform.position, tarpos, 5 * Time.deltaTime);
            //transform.position = tarpos;
        }
    }
}
