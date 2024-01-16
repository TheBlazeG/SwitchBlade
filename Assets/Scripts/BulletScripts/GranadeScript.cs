using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeScript : MonoBehaviour
{
    Rigidbody2D explosiveBulletRigidbody2D;
    [SerializeField] float granadeSpeed;
    [SerializeField] private GameObject explotionRadius;
    private float timer = 0.2f;
    private bool exploted = false;

    private void Start()
    {
        explosiveBulletRigidbody2D = GetComponent<Rigidbody2D>();

        explotionRadius.gameObject.SetActive(false);
    }

    private void Update()
    {

        if (exploted)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        explosiveBulletRigidbody2D.velocity = new Vector2(granadeSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall")
        {
            explotionRadius.gameObject.SetActive(true);
            exploted = true;
        }
    }
}
