using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defeatPlayer : MonoBehaviour
{
    MovimientoPlayer player;
    [SerializeField] private GameObject scenario, boss;
    musicController musicController;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>();
        musicController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<musicController>();
    }

    private void Update()
    {
        if (player.DeathPlayer)
        {
            musicController.musicTrack = 1;
            musicController.changedMusic = true;
            Destroy(scenario);
            Destroy(boss);
        }
    }
}
