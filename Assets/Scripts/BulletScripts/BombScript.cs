using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private Collider2D explotionArea;
    [SerializeField] private float explotionTimer, explotionAreaActiveTime;
    private bool active = false;
    private void Update()
    {
        if (active)
        {
            explotionTimer -= Time.deltaTime;
        }

        if (explotionTimer <= 0 ) 
        {
            explotionAreaActiveTime -= Time.deltaTime;
        }

        if(explotionAreaActiveTime <= 0 ) 
        {
            explotionArea.enabled = true;
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Boss")
        {
            active = true; return;
        }
    }
}
