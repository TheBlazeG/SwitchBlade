using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject home;
    public GameObject player;
    [SerializeField] float range;
    public float speed;
    private float distance;
    private float distancePlayer;
    Rigidbody2D rb;
    Vector3 localScale;
    float dirX;
    private Animator animator;

    private void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, home.transform.position);
        distancePlayer = Vector2.Distance(player.transform.position, home.transform.position);
        Vector2 directionPlayer = player.transform.position - transform.position;
        directionPlayer.Normalize();


        if (distance < range && distancePlayer < range)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, home.transform.position, speed * Time.deltaTime);
        }
        //PlayerFound.enabled = true;
    }

    private void LateUpdate()
    {
        CheckWhereToFace();
    }

    void CheckWhereToFace()
    {
        dirX = transform.position.x;

        if ((dirX - player.transform.position.x < 0 && distancePlayer < range) || (distancePlayer > range && dirX < home.transform.position.x))
        {
            localScale.x = 1;
        }
        else
        {
            localScale.x = -1;
        }
        transform.localScale = localScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {

            case "Obstacle":
                rb.AddForce(Vector2.up * 450f);
                animator.Play("Jump");  

                //print("obstaculo");
                break;

            case "Platform":    
                rb.AddForce(Vector2.up * 450f);
                //print("platform");
                break;

            case "Ground":
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, 0f);
                break;
        }
    }
}
