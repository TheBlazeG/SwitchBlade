using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    public Transform constroladorbala;

    public GameObject Flecha;

    private void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            //Flecha
            Disparo();
        }
    }

    private void Disparo()
    {
        Instantiate(Flecha, constroladorbala.position, constroladorbala.rotation);

    }
}
