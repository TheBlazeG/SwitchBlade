using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    public float Recovery;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MovimientoPlayer>().PlayerCures(Recovery);
            Destroy(gameObject);
        }
    }
}
