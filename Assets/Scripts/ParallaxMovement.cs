using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{
    [SerializeField]
    private Transform cameras;
    [SerializeField]
    private float moveSpeed = 0f;
    [SerializeField]
    private float offsetByX = 13f;
    private float directionX;

    private void Start()
    {
        cameras = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    // Update is called once per frame
    void Update()
    {
        directionX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        transform.position = new Vector2(transform.position.x + directionX, transform.position.y);

        if (transform.position.x - cameras.position.x < -offsetByX)
        {
            transform.position = new Vector2(cameras.position.x + offsetByX, transform.position.y);
        }
        else if (transform.position.x - cameras.position.x > offsetByX)
        {
            transform.position = new Vector2(cameras.position.x - offsetByX, transform.position.y);
        }
    }
}
