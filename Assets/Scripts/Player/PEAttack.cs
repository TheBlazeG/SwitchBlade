using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEAttack : MonoBehaviour
{
    public int Boom;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemigoBeta"))
        {
            other.GetComponent<EnemigoBeta>().Dao(Boom);
        }

        if (other.CompareTag("EnemyAir"))
        {
            other.GetComponent<EnemigoBeta>().Dao(Boom);
        }
    }
}
