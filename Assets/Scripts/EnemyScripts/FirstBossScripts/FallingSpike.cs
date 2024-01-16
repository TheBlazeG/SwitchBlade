using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    MovimientoPlayer movimientoPlayer;

    private float timer = 15;

    private void Start()
    {
        movimientoPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movimientoPlayer.PlayerLife(1);
        }
    }
}
