using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleBehaviour : MonoBehaviour
{

    float dirX;
    [SerializeField] float moveSpeed = 3f;
    Rigidbody2D rb;
    bool facingRight = true;

    Vector3 localScale;

    // Use this for initialization
    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        dirX = -1f;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (CompareTag("EnemyAir")) rb.velocity = new Vector2(rb.velocity.x, localScale.y * moveSpeed);
        else rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    void LateUpdate()
    {
        CheckWhereToFace();
    }

    void CheckWhereToFace()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Wall":
                if (CompareTag("EnemyAir"))
                {
                    if (localScale.y < 0 || localScale.y > 0)
                    {
                        localScale.y *= -1f;
                    }
                    transform.localScale = localScale;
                    //print("WALL");
                }
                else
                {
                    //print("WALL");
                    if (transform.localScale.x < 0)
                    {
                        dirX = 1f;
                    }
                    else
                    {
                        if (transform.localScale.x > 0)
                        {
                            dirX = -1f;
                        }
                    }
                }
                break;
            case "Obstacle":
                rb.AddForce(Vector2.up * 500f);
                //print("obstaculo");
                break;

            case "Platform":
                rb.AddForce(Vector2.up * 450f);
                //print("platform");
                break;
            
        }
    }

}