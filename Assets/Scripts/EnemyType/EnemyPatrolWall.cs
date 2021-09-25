using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolWall : MonoBehaviour
{
    public float moveSpeed;
    public Transform wallDetection;
    public LayerMask layerMask;

    private Transform player;

    private bool movingRight = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement Enemy
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // Check for wall
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.left, 0.1f, layerMask);
        Debug.DrawRay(wallDetection.position, Vector2.left, Color.blue);
        if (wallInfo.collider != null)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = true;
            }
        }

    }
}
