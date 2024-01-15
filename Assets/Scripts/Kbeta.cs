using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kbeta : MonoBehaviour
{
    public float Damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<MovimientoPlayer>().PlayerLife(Damage);
        }
    }
}
