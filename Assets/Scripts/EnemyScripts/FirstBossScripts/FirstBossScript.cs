using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossScript : MonoBehaviour
{
    [SerializeField] public int firstBossFollowersLeft, firstBossHealth; 
    [SerializeField] private float appearFollowersTime;
    [SerializeField] private GameObject[] followers;
    private FirstBossManager bossManager;
    public bool firstBossAttacking = false;

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

        if (firstBossFollowersLeft <= 0) 
        {
            firstBossAttacking = true;
        }
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
