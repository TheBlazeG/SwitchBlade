using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossScript : MonoBehaviour
{
    [SerializeField] public int firstBossFollowersLeft, firstBossHealth; 
    [SerializeField] float firstBossFirstPhaceShootTime, appearFollowersTime;
    [SerializeField] GameObject reflectableBullet;
    [SerializeField] GameObject[] followers;
    float firstBossFirstPhaceShootTimeTemp = 0;
    FirstBossManager bossManager;

    private void Start()
    {
        Invoke("AppearFollowers", appearFollowersTime);

        bossManager = GameObject.FindGameObjectWithTag("BossManager").GetComponent<FirstBossManager>();
    }

    private void Update()
    {
        if (firstBossHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (firstBossFollowersLeft <= 0 && firstBossFirstPhaceShootTimeTemp <= 0) 
        {
            ShootReflectableBulletBoss();
            firstBossFirstPhaceShootTimeTemp = firstBossFirstPhaceShootTime;
        }
        firstBossFirstPhaceShootTimeTemp -= Time.deltaTime;
    }

    void ShootReflectableBulletBoss()
    {
        Instantiate(reflectableBullet, transform.position, Quaternion.identity);
    }

    void AppearFollowers()
    {
        Instantiate(followers[0], new Vector2(-9f, -12.3f), Quaternion.identity);
        Instantiate(followers[1], new Vector2(-17f, -12.3f), Quaternion.identity);
        Instantiate(followers[2], new Vector2(-9f, 4.85f), Quaternion.identity);
        Instantiate(followers[3], new Vector2(-17f, 4.85f), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && firstBossFollowersLeft <= 0)
        {
            bossManager.firstBossHealthTemp--;
            firstBossHealth--;
        }
    }
}
