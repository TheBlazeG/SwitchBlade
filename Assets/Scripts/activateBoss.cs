using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class activateBoss : MonoBehaviour
{
    [SerializeField] private GameObject boss, spawn;
    private bool activatedOnce = false;
    public bool bossActive = true;

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
            
            Instantiate(boss, spawn.transform.position, Quaternion.identity);
        }
        activatedOnce = true;

        gameObject.SetActive(false);
    }
}
