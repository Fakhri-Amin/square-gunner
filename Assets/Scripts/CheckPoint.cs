using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Sprite whiteFlag;
    public Sprite blackFlag;
    public bool checkPointReached;
    public GameObject checkPointEffect;
    public int checkReached = 1;

    private SpriteRenderer checkoPointSR;
    // Start is called before the first frame update
    void Start()
    {
        checkoPointSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (checkReached > 0)
            {
                checkoPointSR.sprite = whiteFlag;
                Instantiate(checkPointEffect, transform.position, Quaternion.identity);
                checkPointReached = true;
                checkReached--;
            }

        }
    }
}
