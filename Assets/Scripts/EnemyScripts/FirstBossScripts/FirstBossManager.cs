using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossManager : MonoBehaviour
{
    [SerializeField] int firstBossHealth;
    [SerializeField] private List<GameObject> firstBossPhases;
    [SerializeField] private GameObject projectilesUpgrade, upgradeSpawn, bossSpawn;
    public int firstBossCurrentPhase = 0, firstBossHealthTemp;
    public bool bossDefeated = false;

    [Header("Sounds")]
    [SerializeField] AudioClip initialLaugh, changePhase;

    private void Start()
    {
        SoundController.Instance.PlaySounds(initialLaugh);
        firstBossHealthTemp = firstBossHealth;
        Instantiate(firstBossPhases[0], bossSpawn.transform.position, Quaternion.identity);
    }

    private void Update()
    {

        if (firstBossHealthTemp <= 0 && firstBossCurrentPhase <= 1)
        {
            SoundController.Instance.PlaySounds(changePhase);
            Instantiate(firstBossPhases[firstBossCurrentPhase + 1], bossSpawn.transform.position, Quaternion.identity);
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
