using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class activateBoss : MonoBehaviour
{
    [SerializeField] private GameObject boss, spawn;
    [SerializeField] private int musicTrack;
    private bool activatedOnce = false;
    public bool bossActive = true;
    musicController musicController;

    private void Start()
    {
        musicController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<musicController>();
    }

    private void Update()
    {
        if (bossActive && activatedOnce)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            musicController.musicTrack = musicTrack;
            musicController.changedMusic = true;
            Instantiate(boss, spawn.transform.position, Quaternion.identity);
        }
        activatedOnce = true;

        gameObject.SetActive(false);
    }
}
