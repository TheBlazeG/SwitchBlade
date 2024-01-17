using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int Fuerza;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EnemigoBeta"))
        {
            other.GetComponent<EnemigoBeta>().Dao(Fuerza);
        }
        if(other.CompareTag("EnemyAir"))
        {
            other.GetComponent<EnemigoBeta>().Dao(Fuerza);
        } 


    }
}
