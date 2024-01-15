using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSaltos : MonoBehaviour
{
    [SerializeField] private List<float> bossSaltosSpeed, walkingTime, walkingSpeed, bombsCadence;
    [SerializeField] private List<int> jumpsPhase;
    [SerializeField] private List<GameObject> enemySpawn;
    [SerializeField] private GameObject bombs, enemies;
    [SerializeField] private int health;
    [SerializeField] private float enemySpawnTimer;
    private bool thrusting = false, dropBombs = false, firstJump = true;
    private int phase = 0, jumpsLeft = 0, thrustOrientationX = 1, thrustOrientationY = -1, enemySpawnIndex = 0;
    private float walkingTimeTimer = 0, bombsCadenceTimer = 0, enemySpawnTimerFake = 0;
    private Rigidbody2D rbBossSaltos;
    MovimientoPlayer movimientoPlayer;

    private void Start()
    {
        rbBossSaltos = GetComponent<Rigidbody2D>();
        movimientoPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>();

        walkingTimeTimer = walkingTime[phase];
    }

    private void Update()
    {
        //debug zone

        if (health > 100)
        {
            phase = 0;
        }
        else if (health > 50)
        {
            phase = 1;
        }
        else if(health > 0)
        {
            phase = 2;
        }
        else
        {
            Destroy(gameObject);
        }

        if (jumpsLeft <= 0 && thrusting)
        {
            thrusting = false;
            walkingTimeTimer = walkingTime[phase];
        }

        if (walkingTimeTimer <= 0 && jumpsLeft <= 0) 
        {
            if (firstJump) 
            {
                firstJump = false;
                rbBossSaltos.AddForce(new Vector2(0, 1000));
            }
            thrusting = true;
            jumpsLeft = jumpsPhase[phase];
        }

        if (dropBombs && phase > 0) 
        {
            if(bombsCadenceTimer <= 0)
            {
                Instantiate(bombs, transform.position, transform.rotation);
                bombsCadenceTimer = bombsCadence[phase];
            }
        }

        bombsCadenceTimer -= Time.deltaTime;

        if (phase == 2 && thrusting)
        {
            enemySpawnTimerFake -= Time.deltaTime;
            if (enemySpawnTimerFake <= 0) 
            {
                enemySpawnIndex = Random.Range(0, 2);
                Instantiate(enemies, enemySpawn[enemySpawnIndex].transform.position, enemySpawn[enemySpawnIndex].transform.rotation);
                enemySpawnTimerFake = enemySpawnTimer;
            }
        }
    }

    private void FixedUpdate()
    {
        if (thrusting)
        {
            rbBossSaltos.velocity = new Vector2(bossSaltosSpeed[phase] * Time.deltaTime * thrustOrientationX, bossSaltosSpeed[phase] * Time.deltaTime * thrustOrientationY);
            dropBombs = true;
        }
        else
        {
            rbBossSaltos.velocity = new Vector2(walkingSpeed[phase] * Time.deltaTime * thrustOrientationX, rbBossSaltos.velocity.y);
            walkingTimeTimer -= Time.deltaTime;
            dropBombs = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "impactWallX")
        {
            thrustOrientationX *= -1;
            if (thrusting)
            {
                jumpsLeft--;
            }
        }

        if (collision.gameObject.tag == "impactWallY")
        {
            thrustOrientationY *= -1;
            if (thrusting)
            {
                jumpsLeft--;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            thrustOrientationX *= -1;
            movimientoPlayer.PlayerLife(1);
        }
    }
}
