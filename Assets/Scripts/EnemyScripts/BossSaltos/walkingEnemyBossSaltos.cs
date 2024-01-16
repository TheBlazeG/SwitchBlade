using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class walkingEnemyBossSaltos : MonoBehaviour
{
    [SerializeField] float walkingSpeed;
    Rigidbody2D rbWalkingEnemy;
    int orientation = 1;
    MovimientoPlayer movimientoPlayer;

    private void Start()
    {
        movimientoPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>();

        rbWalkingEnemy = GetComponent<Rigidbody2D>();

        if (transform.rotation.y == 1)
        {
            orientation = 1;
        }
        else
        {
            orientation = -1;
        }
    }
    private void FixedUpdate()
    {
        rbWalkingEnemy.velocity = new Vector2(walkingSpeed * orientation * Time.deltaTime, rbWalkingEnemy.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "PlayerSword")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movimientoPlayer.PlayerLife(1);
            Destroy(gameObject);
        }
    }
}
