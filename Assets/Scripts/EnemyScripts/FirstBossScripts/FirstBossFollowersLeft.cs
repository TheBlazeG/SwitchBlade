using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossFollowersLeft : MonoBehaviour
{
    [SerializeField] GameObject reflectableBullets;
    [SerializeField] int bulletHitsLeft;
    [SerializeField] float staticEnemyShootTime, staticEnemyFirstShootTime;
    FirstBossScript boss;
    MovimientoPlayer movimientoPlayer;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("ShootReflectableBulletToPlayer", staticEnemyFirstShootTime, staticEnemyShootTime);
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<FirstBossScript>();
        movimientoPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>();
    }

    private void Update()
    {
        if (bulletHitsLeft <= 0)
        {
            boss.firstBossFollowersLeft--;
            Destroy(gameObject);
        }
        animator.SetBool("shooting", true);
        StartCoroutine(animateShoot());
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

        if (collision.gameObject.tag == "Player")
        {
            movimientoPlayer.PlayerLife(1);
        }
    }

    IEnumerator animateShoot() 
    {
        yield return new WaitForSeconds(.5f);
        animator.SetBool("shooting", false);
    }
}
