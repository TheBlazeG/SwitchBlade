using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class activateBoss : MonoBehaviour
{
    [SerializeField] private GameObject boss, spawn;
    [SerializeField] private int musicTrack;
    public bool bossActivated = false;
    musicController musicController;
    public MovimientoPlayer movimientoPlayer;
    FirstBossManager firstBossManager;
    public ActivateFather activateFather;
    private void Start()
    {
        bossActivated = false;
        musicController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<musicController>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BossDetector")
        {
            activateFather.activeTrigger = false;
            bossActivated = true;
            musicController.musicTrack = musicTrack;
            musicController.changedMusic = true;
            Instantiate(boss, spawn.transform.position, Quaternion.identity);
        }
    }
}
