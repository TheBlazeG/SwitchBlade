using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocks : MonoBehaviour
{
    public AudioClip soundObject;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundController.Instance.PlaySounds(soundObject);
            other.GetComponent<MovimientoPlayer>().ShootUnlockPP();
            Destroy(gameObject);
        }
    }
}


