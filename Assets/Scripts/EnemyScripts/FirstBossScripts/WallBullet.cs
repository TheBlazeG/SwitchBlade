using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBullet : MonoBehaviour
{
    [SerializeField] float wallBulletSpeed;
    private Rigidbody2D wallBulletRigidbody2d;
    private float timer = 3.4f;
    MovimientoPlayer movimientoPlayer;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Run", true);
        wallBulletRigidbody2d = GetComponent<Rigidbody2D>();
        movimientoPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        wallBulletRigidbody2d.velocity = Vector2.right * wallBulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movimientoPlayer.PlayerLife(1);
            Destroy(gameObject);
        }
    }
}
