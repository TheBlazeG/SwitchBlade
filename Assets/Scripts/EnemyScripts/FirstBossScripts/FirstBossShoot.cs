using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossShoot : MonoBehaviour
{
    [SerializeField] List<GameObject> bulletType;
    [SerializeField] GameObject bulletSpawnPlace;
    [SerializeField] private float firstBossShootTime;
    [SerializeField] private int currentPhase;
    [SerializeField] private float shootAnimationTimer;
    FirstBossScript firstBossScript;
    FirstBossManager bossManager;
    private int bulletTypeIndex = 0;
    private float firstBossShootTimeTemp = 0, shootAnimationTimerFake = 0;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        firstBossScript = GetComponent<FirstBossScript>();
        bossManager = GameObject.FindGameObjectWithTag("BossManager").GetComponent<FirstBossManager>();
        animator = GetComponent<Animator>();

        shootAnimationTimerFake = shootAnimationTimer;
    }

    // Update is called once per frame
    void Update()
    {
        firstBossShootTimeTemp -= Time.deltaTime;
        

        Debug.Log(shootAnimationTimerFake);

        if (firstBossScript.firstBossAttacking && firstBossShootTimeTemp <= 0)
        {
            animator.SetBool("shooting", true);
            shootAnimationTimerFake -= Time.deltaTime;
            if (shootAnimationTimerFake <= 0) 
            {
                Shoot();
            }
            
        }
    }

    void Shoot()
    {
        Instantiate(bulletType[bulletTypeIndex], bulletSpawnPlace.transform.position, Quaternion.identity);
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
            animator.SetBool("shooting", false);
            shootAnimationTimerFake = shootAnimationTimer;
        }
        animator.SetBool("shooting", false);
        shootAnimationTimerFake = shootAnimationTimer;
        firstBossShootTimeTemp = firstBossShootTime;
    }
}


