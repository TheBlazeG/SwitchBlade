using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float velocidad;

    public float dao;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EnemigoBeta"))
        {
            other.GetComponent<EnemigoBeta>().Dao(dao);
            Destroy(gameObject);
        }

        if(other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }   
    }
}
