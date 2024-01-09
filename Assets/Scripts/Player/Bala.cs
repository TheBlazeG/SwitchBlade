using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad;

    public int dao;

    public float lifetime;
    private void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        StartCoroutine(LifetimeCoroutine(lifetime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemigoBeta"))
        {
            other.GetComponent<EnemigoBeta>().Dao(dao);
            Destroy(gameObject);
        }

    }

    IEnumerator LifetimeCoroutine(float _time)
    {
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }
}
