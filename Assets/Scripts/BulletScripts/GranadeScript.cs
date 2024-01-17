using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeScript : MonoBehaviour
{
    Rigidbody2D explosiveBulletRigidbody2D;
    [SerializeField] float granadeSpeed;
    [SerializeField] private GameObject explotionRadius;
    private int facing = 1;
    private float timer = 0.2f;
    private bool exploted = false;
    BossSaltos boss;
    Animator animator;

    private void Start()
    {
        explosiveBulletRigidbody2D = GetComponent<Rigidbody2D>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossSaltos>();
        animator = GetComponent<Animator>();

        facing = boss.facingPlayer;

        explotionRadius.gameObject.SetActive(false);
    }

    private void Update()
    {

        if (exploted)
        {
            animator.SetBool("exploted", true);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        explosiveBulletRigidbody2D.velocity = new Vector2(granadeSpeed * Time.deltaTime * facing, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall")
        {
            explotionRadius.gameObject.SetActive(true);
            exploted = true;
        }
    }
}
