using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevelToCredit : MonoBehaviour
{
    public GameObject completeLevelUI;

    private PlayerController pc;
    private Gun gun;
    private Shooting shooting;
    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        gun = FindObjectOfType<Gun>();
        shooting = FindObjectOfType<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.gameComplete == true)
        {
            CompleteLevels();
        }
    }

    void CompleteLevels()
    {
        completeLevelUI.SetActive(true);
        Time.timeScale = 0f;
        if (Input.GetButtonDown("Fire1"))
        {
            ShowCredits();
        }

        gun.enabled = false;
        shooting.enabled = false;
    }

    void ShowCredits()
    {
        SceneManager.LoadScene("Credit");
    }
}
