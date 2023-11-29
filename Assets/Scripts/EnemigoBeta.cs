using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBeta : MonoBehaviour
{
    public float vida;
    public void Daño(float daño)
    {
        vida -= vida;
        if(vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }
}
