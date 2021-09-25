using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolGround : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    [Header("GroundCheck")]
    public float groundDistance;
    public Transform groundDetection;

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

        // Check for ground
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
        Debug.DrawRay(groundDetection.position, Vector2.down, Color.yellow);
        if (groundInfo.collider == null)
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
