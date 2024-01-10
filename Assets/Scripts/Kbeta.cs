using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kbeta : MonoBehaviour
{
    public float Damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MovimientoPlayer>().PlayerLife(Damage);
        }
    }
}
