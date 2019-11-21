using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject target;

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
        tarpos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, tarpos, 5 * Time.deltaTime);
        //transform.position = tarpos;
    }
}
