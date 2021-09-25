using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public float respawnDelay;
    private PlayerController gamePlayer;
    public GameObject CheckPointEffect;
    public GameObject CompleteLevelUI;
    public GameObject gameOver;

    public int stars;
    public TextMeshProUGUI textStars;

    private bool gameHasEnded = false;
    private Gun gun;
    private Shooting shooting;

    // Start is called before the first frame update
    void Start()
    {
        gamePlayer = FindObjectOfType<PlayerController>();
        gun = FindObjectOfType<Gun>();
        shooting = FindObjectOfType<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }

    public IEnumerator RespawnCoroutine()
    {
        gamePlayer.gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnDelay);
        gamePlayer.transform.position = gamePlayer.respawnPoint;
        Instantiate(CheckPointEffect, gamePlayer.respawnPoint, Quaternion.identity);

        gamePlayer.gameObject.SetActive(true);
    }

    public void AddScore()
    {
        stars++;
        textStars.text = "X " + stars.ToString();
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
        }
    }
}
