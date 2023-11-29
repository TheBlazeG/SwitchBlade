using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad;

    public float daño;

    public float tiempodevida;
    private void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EnemigoBeta"))
        {
            other.GetComponent<EnemigoBeta>().Daño(daño);
            Destroy(gameObject);
        }

        if(other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }   
    }


}
