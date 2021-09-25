using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    private float moveDir;

    [Header("Jumping")]
    public float jumpForce;
    public int extraJumpValue;
    private int extraJumps;

    [Header("GroundCheck")]
    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Respawn")]
    public Vector3 respawnPoint;

    [Header("Particle Effect")]
    public GameObject jumpEffect;
    public GameObject deathEffect;
    public GameObject starEffect;
    public GameObject healingEffect;

    [Header("Health")]
    public int maxhealth;
    public int currentHealth;
    public HealthBar healthBar;

    [Header("Get Hit")]
    public Color whiteColor;
    public Color defaultColor;

    private Rigidbody2D rb;
    private LevelManager gameLevelManager;
    private SpriteRenderer spriteRenderer;
    public bool gameComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawnPoint = transform.position;
        gameLevelManager = FindObjectOfType<LevelManager>();
        extraJumps = extraJumpValue;

        // Health
        currentHealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);

        //Sprite
        spriteRenderer.color = defaultColor;

    }

    // Update is called once per frame
    void Update()
    {
        // Input Process
        moveDir = Input.GetAxisRaw("Horizontal");

        // Double Jump / Reset jump value
        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }

        // Input Jumping
        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            SoundManager.PlaySound("jumpSound");
            Jump();
            Instantiate(jumpEffect, transform.position, Quaternion.identity);
            extraJumps--;
        }
        else if (Input.GetButtonDown("Jump") && isGrounded == true && extraJumps == 0)
        {
            SoundManager.PlaySound("jumpSound");
            Jump();
            Instantiate(jumpEffect, transform.position, Quaternion.identity);
        }

        // Jump once
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundLayer);

        //!Die
        // if (currentHealth == 0)
        // {
        //     gameLevelManager.GameOver();
        // }
    }

    private void FixedUpdate()
    {
        // Move
        rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Respawn
        if (other.gameObject.CompareTag("RespawnTrigger"))
        {
            gameLevelManager.Respawn();
            if (currentHealth > 0)
            {
                SoundManager.PlaySound("hitSound");
                TakeDamage(50);
            }
            else
            {
                TakeDamage(0);
            }
        }

        // Respawn at check point
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            SoundManager.PlaySound("checkPointSound");
            respawnPoint = other.transform.position;
        }

        if (other.gameObject.CompareTag("Star"))
        {
            SoundManager.PlaySound("pickUpStarSound");
            Instantiate(starEffect, other.transform.position, Quaternion.identity);
            gameLevelManager.AddScore();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            SoundManager.PlaySound("hitSound");
            spriteRenderer.color = whiteColor;
            Invoke("ResetSprite", 0.1f);
        }

        if (other.gameObject.CompareTag("END"))
        {
            gameComplete = true;
        }

        if (other.gameObject.CompareTag("Health"))
        {
            if (currentHealth < 100)
            {
                int maxDamage = 100 - currentHealth;
                TakeDamage(-maxDamage);
            }
            SoundManager.PlaySound("getHealingSound");
            Instantiate(healingEffect, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            spriteRenderer.color = whiteColor;
            Invoke("ResetSprite", 0.1f);
            if (currentHealth > 0)
            {
                SoundManager.PlaySound("hitSound");
                TakeDamage(15);
            }
            else
            {
                TakeDamage(0);
            }
        }
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        //rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthBar.SetHealth(currentHealth);
    }

    private void ResetSprite()
    {
        spriteRenderer.color = defaultColor;
    }
}
