using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBeta : MonoBehaviour
{
    private int Life;
    public int BaseLife;
    public float Knockbackforce;
    public Rigidbody2D RB2D;
    public float Push;
    public float Up;

    private void Start()
    {
        Life = BaseLife;
    }

    public void Dao(int dao)
    {
        Life -= dao;
        Knockback();
        if (Life <= 0)
        {
            Muerte();
        }
    }

    public void Muerte()
    {
        gameObject.SetActive(false);
        Life = BaseLife;
    }

    public void Knockback()
    {   
        RB2D.AddForce(new Vector2(Knockbackforce * Push, Up));
    }
}
