using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleShootingChaseStraight : MonoBehaviour
{
    private enum State
    {
        Idling,
        Shooting,
    }

    [Header("Shooting")]
    public float startBtwShots;
    private float timeBtwShots;
    public GameObject enemyBullet;
    public float targetRange;
    public Transform firePoint;

    private Vector3 startingPosition;
    private State state;
    private Transform player;

    private void Awake()
    {
        state = State.Idling;
    }

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Idling:
                FindTarget();
                break;

            case State.Shooting:

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
                break;
        }
    }

    private void FindTarget()
    {
        if (Vector2.Distance(transform.position, player.position) < targetRange)
        {
            state = State.Shooting;
        }
    }
}
