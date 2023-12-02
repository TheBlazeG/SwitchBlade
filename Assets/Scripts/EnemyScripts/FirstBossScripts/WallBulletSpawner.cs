using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject wallBullet;
    [SerializeField] float wallBulletSpawnTime, wallBulletFirstSpawnTime;
    void Start()
    {
        InvokeRepeating("SpawnWallBullet", wallBulletFirstSpawnTime, wallBulletSpawnTime);
    }

    void SpawnWallBullet()
    {
        Instantiate(wallBullet, transform.position, transform.rotation);
    }
}
