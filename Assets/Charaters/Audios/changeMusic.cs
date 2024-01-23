using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMusic : MonoBehaviour
{
    private int musicTrack = 1;
    musicController musicController;

    private void Start()
    {
        musicController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<musicController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            musicController.musicTrack = musicTrack;
            musicController.changedMusic = true;
        }
    }
}
