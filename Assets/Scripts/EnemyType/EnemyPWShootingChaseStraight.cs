using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPWShootingChaseStraight : MonoBehaviour
{
    private enum State
    {
        Patrolling,
        ChaseTarget,
    }
    [Header("Movement")]
    public float moveSpeed;
    public float retreatSpeed;
    public float stoppingDistance;
    public float retreatDistance;

    [Header("Jumping")]
    public float jumpTakeOffSpeed;

    [Header("Wall Check")]
    public Transform wallDetection;
    public LayerMask layerMask;

    [Header("Shooting")]
    public float startBtwShots;
    private float timeBtwShots;
    public GameObject enemyBullet;
    public float targetRange;
    public Transform firePoint;

    private Vector3 startingPosition;
    private PlayerController pc;
    private State state;
    private Transform player;
    private bool movingRight = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        state = State.Patrolling;
    }

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Patrolling:
                // Check for wall
                RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.left, 0.1f, layerMask);
                Debug.DrawRay(wallDetection.position, Vector2.left, Color.blue);

                // Movement Enemy
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

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
                FindTarget();
                break;

            case State.ChaseTarget:
                // Chasing Player
                if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                }
                else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
                {
                    transform.position = this.transform.position;
                }
                else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -retreatDistance * Time.deltaTime);
                }

                if (timeBtwShots <= 0)
                {
                    SoundManager.PlaySound("shootSound");
                    Instantiate(enemyBullet, firePoint.position, Quaternion.identity);
                    timeBtwShots = startBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }

                RaycastHit2D jumpInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, 3f, layerMask);
                if (jumpInfo.collider != null)
                {
                    Jump();
                }
                break;
        }
    }

    private void FindTarget()
    {
        if (Vector2.Distance(transform.position, player.position) < targetRange)
        {
            state = State.ChaseTarget;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpTakeOffSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RespawnTrigger"))
        {
            Destroy(gameObject);
        }
    }

}
