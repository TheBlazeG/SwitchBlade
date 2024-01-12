using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    public Transform constroladorbala;
    public float cooldown;
    public GameObject Flecha;
    public float timelife;
    public AudioSource shootAudioSource;
    public GameObject Boom;


    private bool CanShoot = true;
    public bool Chicken;
    public float ChickenBoom;
    private float ChickenMore;

    void Start()
    {
        ChickenMore = 0;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && CanShoot)
        {
            //Flecha
            Disparo();
            StartCoroutine(CooldownCoroutine(cooldown));

        }

        if (Input.GetKey("g") && CanShoot && ChickenMore > 0)
        {
            //Pollo
            Instantiate(Boom, constroladorbala.position, constroladorbala.rotation);
            StartCoroutine(CooldownCoroutine(cooldown));
        }
    }

    private void Disparo()
    {
        Instantiate(Flecha, constroladorbala.position, constroladorbala.rotation);
        shootAudioSource.Play();
    }

    public void Bomba(float ChickenBoom)
    {
        ChickenMore =+ ChickenBoom;
    }

    IEnumerator CooldownCoroutine(float _time)
    {
        CanShoot = false;
        yield return new WaitForSeconds(_time);
        CanShoot = true;
    }
    
    IEnumerator Pollo()
    {
        Chicken = false;
        yield return new WaitForSeconds(2f);
        Chicken = true;
    }
}
