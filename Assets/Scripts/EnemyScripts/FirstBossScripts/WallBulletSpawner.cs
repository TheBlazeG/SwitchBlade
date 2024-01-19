using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject wallBullet;
    [SerializeField] float wallBulletSpawnTime, wallBulletFirstSpawnTime;
    FirstBossManager firstBossManager;
    void Start()
    {
        firstBossManager = GameObject.FindGameObjectWithTag("BossManager").GetComponent<FirstBossManager>();
        InvokeRepeating("SpawnWallBullet", wallBulletFirstSpawnTime, wallBulletSpawnTime);
    }

    private void Update()
    {
        if (firstBossManager.bossDefeated)
        {
            Destroy(gameObject);
        }
    }
    void SpawnWallBullet()
    {
        Instantiate(wallBullet, transform.position, transform.rotation);
    }

}
