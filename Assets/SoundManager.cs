using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip jumpSound, explodedSound, getHealingSound, hitSound, pickUpStarSound, shootSound, checkPointSound;
    private static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        jumpSound = Resources.Load<AudioClip>("jumpSound");
        explodedSound = Resources.Load<AudioClip>("explodedSound");
        getHealingSound = Resources.Load<AudioClip>("getHealingSound");
        hitSound = Resources.Load<AudioClip>("hitSound");
        pickUpStarSound = Resources.Load<AudioClip>("pickUpStarSound");
        shootSound = Resources.Load<AudioClip>("shootSound");
        checkPointSound = Resources.Load<AudioClip>("checkPointSound");

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jumpSound":
                audioSource.PlayOneShot(jumpSound);
                break;
            case "explodedSound":
                audioSource.PlayOneShot(explodedSound);
                break;
            case "getHealingSound":
                audioSource.PlayOneShot(getHealingSound);
                break;
            case "hitSound":
                audioSource.PlayOneShot(hitSound);
                break;
            case "pickUpStarSound":
                audioSource.PlayOneShot(pickUpStarSound);
                break;
            case "shootSound":
                audioSource.PlayOneShot(shootSound);
                break;
            case "checkPointSound":
                audioSource.PlayOneShot(checkPointSound);
                break;
        }
    }
}
