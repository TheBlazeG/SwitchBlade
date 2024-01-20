using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossScript : MonoBehaviour
{
    [SerializeField] public int firstBossFollowersLeft, firstBossHealth, phase; 
    [SerializeField] private float appearFollowersTime;
    [SerializeField] private GameObject[] followers, spawns;
    private FirstBossManager bossManager;
    public bool firstBossAttacking = false;
    Animator animator;

    [Header("Sounds")]
    [SerializeField] AudioClip Hurt, Die, explosionSound;
    

    private void Start()
    {
        Invoke("AppearFollowers", appearFollowersTime);
        animator = GetComponent<Animator>();

        bossManager = GameObject.FindGameObjectWithTag("BossManager").GetComponent<FirstBossManager>();
    }

    private void Update()
    {
        if (firstBossHealth <= 0 && phase <= 2)
        {
            
            Destroy(gameObject);
        }

        if(firstBossHealth <= 0 && phase == 3) 
        {
            StartCoroutine(death());
        }

        if (firstBossFollowersLeft <= 0) 
        {
            firstBossAttacking = true;
        }
    }


    void AppearFollowers()
    {
        Instantiate(followers[0], spawns[0].transform.position, spawns[0].transform.rotation);
        Instantiate(followers[1], spawns[1].transform.position, spawns[1].transform.rotation);
        Instantiate(followers[2], spawns[2].transform.position, spawns[2].transform.rotation);
        Instantiate(followers[3], spawns[3].transform.position, spawns[3].transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && firstBossFollowersLeft <= 0)
        {
            StartCoroutine(hurt());
            firstBossHealth--;
            bossManager.firstBossHealthTemp--;
        }
    }

    IEnumerator hurt()
    {
        animator.SetBool("hurt", true);
        SoundController.Instance.PlaySounds(Hurt);
        yield return new WaitForSeconds(.7f);
        animator.SetBool("hurt", false);
    }

    IEnumerator death () 
    {
        animator.SetBool("death", true);
        SoundController.Instance.PlaySounds(Die);
        yield return new WaitForSeconds(4f);
        bossManager.bossDefeated = true;
        SoundController.Instance.PlaySounds(explosionSound);
        Destroy(gameObject);
    }
}
