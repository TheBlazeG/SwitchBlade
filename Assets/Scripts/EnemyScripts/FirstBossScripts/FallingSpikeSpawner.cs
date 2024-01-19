using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikeSpawner : MonoBehaviour
{
    [SerializeField] GameObject FallingSpike;
    [SerializeField] float fallingSpikeSpawnTime, fallingSpikeFirstSpawnTime;
    FirstBossManager firstBossManager;
    void Start()
    {
        firstBossManager = GameObject.FindGameObjectWithTag("BossManager").GetComponent<FirstBossManager>();
        InvokeRepeating("SpawnFallingSpike", fallingSpikeFirstSpawnTime, fallingSpikeSpawnTime);
    }

    private void Update()
    {
        if (firstBossManager.bossDefeated)
        {
            Destroy(gameObject);
        }
    }

    void SpawnFallingSpike()
    {
        Instantiate(FallingSpike, transform.position, transform.rotation);
    }

}
