using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectableBullet : MonoBehaviour
{
    Rigidbody2D reflectableBulletRigidbody2D;
    [SerializeField] float reflectableBulletSpeed;
    GameObject reflectableBulletPlayer;
    int reflectableBulletOrientation = 1;
    private float timer = 10;
    Vector2 reflectableBulletDirection;

    private void Start()
    {
        reflectableBulletRigidbody2D = GetComponent<Rigidbody2D>();
        reflectableBulletPlayer = GameObject.FindGameObjectWithTag("Player");

        reflectableBulletDirection = reflectableBulletPlayer.transform.position - transform.position;
        
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        reflectableBulletRigidbody2D.velocity = new Vector2(reflectableBulletDirection.x, reflectableBulletDirection.y).normalized * reflectableBulletSpeed * reflectableBulletOrientation * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerSword")
        {
            gameObject.tag = "Bullet";
            reflectableBulletOrientation = -1;
        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
