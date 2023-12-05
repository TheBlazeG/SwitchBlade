using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    public Transform constroladorbala;
    public float cooldown;
    public GameObject Flecha;
    public float timelife;

    private bool canShoot = true;

    private void Update()
    {
        if(Input.GetButtonDown("Fire2") && canShoot)
        {
            //Flecha
            Disparo();
            StartCoroutine(CooldownCoroutine(cooldown));

        }
    }

    private void Disparo()
    {
        Instantiate(Flecha, constroladorbala.position, constroladorbala.rotation);
    }

    IEnumerator CooldownCoroutine(float _time)
    {
        canShoot = false;
        yield return new WaitForSeconds(_time);
        canShoot = true;
    }
    
}
