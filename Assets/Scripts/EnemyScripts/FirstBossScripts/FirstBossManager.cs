using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossManager : MonoBehaviour
{
    [SerializeField] int firstBossHealth;
    [SerializeField] private List<GameObject> firstBossPhases;
    [SerializeField] private GameObject projectilesUpgrade, upgradeSpawn;
    public int firstBossCurrentPhase = 0, firstBossHealthTemp;
    public bool bossDefeated = false;

    private void Start()
    {
        firstBossHealthTemp = firstBossHealth;
    }

    private void Update()
    {

        if (firstBossHealthTemp <= 0 && firstBossCurrentPhase <= 1)
        {
            Instantiate(firstBossPhases[firstBossCurrentPhase + 1]);
            firstBossHealthTemp = firstBossHealth;
            firstBossCurrentPhase++;
        }
        
        if (bossDefeated) 
        {
            Instantiate(projectilesUpgrade, upgradeSpawn.transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet") 
        {
            firstBossHealth--;
        }
    }
}
