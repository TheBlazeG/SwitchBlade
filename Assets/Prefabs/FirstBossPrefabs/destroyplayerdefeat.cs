using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyplayerdefeat : MonoBehaviour
{
    MovimientoPlayer player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>();
    }

    private void Update()
    {
        if(player.DeathPlayer)
        {
            Destroy(gameObject);
        }
    }
}
