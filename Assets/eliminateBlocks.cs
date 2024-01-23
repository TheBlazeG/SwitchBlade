using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eliminateBlocks : MonoBehaviour
{
    public FirstBossManager boss;
    public GameObject scenario;

    private void Update()
    {
        if(boss.bossDefeated)
        {
            Destroy(scenario);
        }
    }
}
