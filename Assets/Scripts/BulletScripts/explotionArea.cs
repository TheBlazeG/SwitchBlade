using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explotionArea : MonoBehaviour
{
    MovimientoPlayer movimientoPlayer;

    private void Start()
    {
        movimientoPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movimientoPlayer.PlayerLife(1);
        }
    }
}
