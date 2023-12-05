using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBeta : MonoBehaviour
{
    private int vida;
    public int baselife;

    private void Start()
    {
        vida = baselife;
    }
    public void Dao(int dao)
    {
        vida -= dao;
        if(vida <= 0)
        {
            Muerte();
        }
    }

    public void Muerte()
    {
        gameObject.SetActive(false);
        vida = baselife;
    }
}
