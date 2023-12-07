using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossShoot : MonoBehaviour
{
    [SerializeField] List<GameObject> bulletType;
    [SerializeField] float firstBossShootTime;
    [SerializeField] int currentPhase;
    FirstBossScript firstBossScript;
    FirstBossManager bossManager;
    private int bulletTypeIndex = 0;
    private float firstBossShootTimeTemp = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        firstBossScript = GetComponent<FirstBossScript>();
        bossManager = GameObject.FindGameObjectWithTag("BossManager").GetComponent<FirstBossManager>();
    }

    // Update is called once per frame
    void Update()
    {
        firstBossShootTimeTemp -= Time.deltaTime;

        if (firstBossScript.firstBossAttacking && firstBossShootTimeTemp <= 0)
        {
            Shoot();
            firstBossShootTimeTemp = firstBossShootTime;
        }
    }

    void Shoot()
    {
        Instantiate(bulletType[bulletTypeIndex], transform.position, Quaternion.identity);
        if (currentPhase > 1) 
        {
            if (bulletTypeIndex == 0)
            {
                bulletTypeIndex = 1;
            }
            else
            {
                bulletTypeIndex = 0;
            }
        }
    }

}
