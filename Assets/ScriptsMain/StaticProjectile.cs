using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticProjectile : MonoBehaviour
{
    Rigidbody2D reflectableBulletRigidbody2D;
    [SerializeField] float reflectableBulletSpeed;
    int reflectableBulletOrientation = 0;
    [SerializeField] int bulletOrientation;
    private float timer = 10;

    MovimientoPlayer movimientoPlayer;
    // Start is called before the first frame update 
    private void Start()
    {
        reflectableBulletRigidbody2D = GetComponent<Rigidbody2D>();

      

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
        reflectableBulletRigidbody2D.velocity = new Vector2(1, 0).normalized * reflectableBulletSpeed * reflectableBulletOrientation * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerSword")
        {
            gameObject.tag = "Bullet";
            reflectableBulletOrientation = -3;
        }
       

        if (gameObject.tag == "Bullet" && (collision.gameObject.tag == "Boss" || collision.gameObject.tag == "StaticProjectileEnemy"))
        {
            Destroy(gameObject);
        }

        
    }
}

