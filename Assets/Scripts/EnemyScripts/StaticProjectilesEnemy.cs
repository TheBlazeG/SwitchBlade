using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticProjectilesEnemy : MonoBehaviour
{
    [SerializeField] GameObject reflectableBullets;
    [SerializeField] int bulletHitsLeft;
    [SerializeField] float staticEnemyShootTime, staticEnemyFirstShootTime;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootReflectableBulletToPlayer", staticEnemyFirstShootTime, staticEnemyShootTime);
    }

    private void Update()
    {
        if (bulletHitsLeft <= 0)
        {
            Destroy(gameObject);        
        }
    }

    void ShootReflectableBulletToPlayer()
    {
        Instantiate(reflectableBullets, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            bulletHitsLeft--;
        }
    }
}
