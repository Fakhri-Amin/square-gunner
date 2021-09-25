using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static bool gameIsOver = false;
    public GameObject gameOverUI;

    private PlayerController pc;
    private Gun gun;
    private Shooting shooting;
    private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        gun = FindObjectOfType<Gun>();
        shooting = FindObjectOfType<Shooting>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.currentHealth == 0)
        {
            GameOvers();
        }
    }

    void GameOvers()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsOver = true;

        gun.enabled = false;
        shooting.enabled = false;
    }
}
