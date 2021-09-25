using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEnemyStraight : MonoBehaviour
{
    public float speed;
    public GameObject destroyEffect;

    private Transform player;
    private Vector2 target;

    private Rigidbody2D rb;
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pc = GameObject.FindObjectOfType<PlayerController>();
        target = (pc.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(target.x, target.y);
        Destroy(gameObject, 9f);
        //target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // if (transform.position.x == target.x && transform.position.y == target.y)
        // {
        //     Instantiate(destroyEffect, transform.position, Quaternion.identity);
        //     Destroy(gameObject);
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (pc.currentHealth > 0)
            {
                pc.TakeDamage(10);
            }
            else
            {
                pc.TakeDamage(0);
            }
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
