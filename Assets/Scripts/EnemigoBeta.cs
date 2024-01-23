using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBeta : MonoBehaviour
{
    private int Life;
    public int BaseLife;
    public float Knockbackforce;
    public Rigidbody2D RB2D;
    private int Push = 1;
    public float Up;
    public GameObject player;

    DroppingItems items;

    private void Start()
    {
        Life = BaseLife;
        items = GetComponent<DroppingItems>();
    }

    public void Dao(int dao)
    {
        Life -= dao;
        Knockback();
        
        if (Life <= 0)
        {
            items.ItemsDropped();
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
        if(transform.position.x > player.transform.position.x)
        {
            Debug.Log("derecha");
            RB2D.AddForce(new Vector2(Knockbackforce * Push, Up));
        }
        else
        {
            RB2D.AddForce(new Vector2(Knockbackforce * -Push, Up));
        }
    }
}
