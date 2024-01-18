using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDamage : MonoBehaviour
{
    Attack attack;
    public int Ultra = 2;
    public AudioClip soundObject;

    private void Start()
    {
        attack = GameObject.FindGameObjectWithTag("PlayerSword").GetComponent<Attack>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundController.Instance.PlaySounds(soundObject);
            attack.Fuerza += Ultra;
            Destroy(gameObject);
        }
    }
}
