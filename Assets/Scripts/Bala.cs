using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad;

    public float da�o;

    public float tiempodevida;
    private void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EnemigoBeta"))
        {
            other.GetComponent<EnemigoBeta>().Da�o(da�o);
            Destroy(gameObject);
        }

        if(other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }   
    }


}
