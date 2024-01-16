using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad;
    public int dao;
    public float lifetime;
    public AudioClip Impact;
    public AudioClip Fly;


    private void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        StartCoroutine(LifetimeCoroutine(lifetime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemigoBeta"))
        {
            SoundController.Instance.PlaySounds(Impact);
            other.GetComponent<EnemigoBeta>().Dao(dao);
            Destroy(gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator LifetimeCoroutine(float _time)
    {
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }
}
