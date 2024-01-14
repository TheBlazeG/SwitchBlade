using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikeSpawner : MonoBehaviour
{
    [SerializeField] GameObject FallingSpike;
    [SerializeField] float fallingSpikeSpawnTime, fallingSpikeFirstSpawnTime;
    void Start()
    {
        InvokeRepeating("SpawnFallingSpike", fallingSpikeFirstSpawnTime, fallingSpikeSpawnTime);
    }

    private void Update()
    {
        
    }

    void SpawnFallingSpike()
    {
        Instantiate(FallingSpike, transform.position, transform.rotation);
    }

}
