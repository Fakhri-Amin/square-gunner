using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = GameObject.FindObjectOfType<PlayerController>().transform.position;

        Vector3 gunPos = transform.position;
        targetPos.x = targetPos.x - gunPos.x;
        targetPos.y = targetPos.y - gunPos.y;

        float gunAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        if (targetPos.x < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -gunAngle));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, gunAngle));
        }
    }
}
