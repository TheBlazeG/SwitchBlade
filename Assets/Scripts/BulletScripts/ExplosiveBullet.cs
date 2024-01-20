using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    Rigidbody2D explosiveBulletRigidbody2D;
    [SerializeField] float explosiveBulletSpeed;
    [SerializeField] private GameObject explotionRadius;
    GameObject explosiveBulletPlayer;
    private float timer = 0.2f, lifeTime = 6f;
    private bool exploted = false;
    Vector2 explosiveBulletDirection;
    Animator animator;
    [SerializeField] AudioClip explosionSound;

    private void Start()
    {
        explosiveBulletRigidbody2D = GetComponent<Rigidbody2D>();
        explosiveBulletPlayer = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();

        explosiveBulletDirection = explosiveBulletPlayer.transform.position - transform.position;

        explotionRadius.gameObject.SetActive(false);
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        if (exploted)
        {
            animator.SetBool("exploted", true);
            SoundController.Instance.PlaySounds(explosionSound);
            timer -= Time.deltaTime;
            if (timer <= 0) 
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        explosiveBulletRigidbody2D.velocity = new Vector2(explosiveBulletDirection.x, explosiveBulletDirection.y).normalized * explosiveBulletSpeed * Time.deltaTime;
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
