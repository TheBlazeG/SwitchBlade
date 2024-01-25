using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private GameObject explotionArea;
    [SerializeField] private float explotionTimer, explotionAreaActiveTime;
    private bool active = false;
    Animator animator;
    [SerializeField] AudioClip explosionSound;
    public SoundController explotion;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (active)
        {
            explotionTimer -= Time.deltaTime;
        }

        if (explotionTimer <= 0 ) 
        {
            animator.SetBool("exploted", true);
            explotionAreaActiveTime -= Time.deltaTime;
            explotionArea.gameObject.SetActive(true);
            explotion.PlaySounds(explosionSound);
        }

        if(explotionAreaActiveTime <= 0 ) 
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Boss")
        {
            active = true; return;
        }
    }
}
