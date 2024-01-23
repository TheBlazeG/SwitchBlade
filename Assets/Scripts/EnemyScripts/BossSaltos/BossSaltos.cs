using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;

public class BossSaltos : MonoBehaviour
{
    [SerializeField] private List<float> bossSaltosSpeed, walkingTime, walkingSpeed, bombsCadence;
    [SerializeField] private List<int> jumpsPhase;
    [SerializeField] private List<GameObject> enemySpawn;
    [SerializeField] private GameObject bombs, enemies, granade, spawn;
    [SerializeField] private int health;
    [SerializeField] private float enemySpawnTimer, granadeCadence, pushForce;
    [SerializeField] private Collider2D colliderA, colliderB;
    private bool thrusting = false, dropBombs = false, changingDirection = false, pushed = false, beingHit = false;
    private int phase = 0, jumpsLeft = 0, thrustOrientationY = -1, thrustOrientationX = 1, enemySpawnIndex = 0;
    public int facingPlayer = 1;
    private float walkingTimeTimer = 0, bombsCadenceTimer = 0, enemySpawnTimerFake = 0, granadeCadenceFake;
    private Rigidbody2D rbBossSaltos;
    private GameObject player;
    MovimientoPlayer movimientoPlayer;
    Animator animator;
    [SerializeField] AudioClip laugh, hurt, explosion, bounce, die;

    private void Start()
    {
        SoundController.Instance.PlaySounds(laugh);
        rbBossSaltos = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        movimientoPlayer = player.GetComponent<MovimientoPlayer>();
        animator = GetComponent<Animator>();

        walkingTimeTimer = walkingTime[phase];
    }

    private void Update()
    {
        //debug zone
        
        

        if (transform.position.x < player.transform.position.x)
        {
            facingPlayer = 1;
        }
        else if (transform.position.x > player.transform.position.x)
        {
            facingPlayer = -1;
        }


        if (health > 40)
        {
            phase = 0;
        }
        else if (health > 20)
        {
            phase = 1;
        }
        else if(health > 0)
        {
            phase = 2;
        }
        else
        {
            StartCoroutine(Die());
        }

        if (jumpsLeft <= 0 && thrusting)
        {
            thrusting = false;
            walkingTimeTimer = walkingTime[phase];
        }

        if (walkingTimeTimer <= 0 && jumpsLeft <= 0) 
        {
            thrusting = true;
            jumpsLeft = jumpsPhase[phase];
        }

        if (dropBombs && phase > 0) 
        {
            if(bombsCadenceTimer <= 0)
            {
                Instantiate(bombs, spawn.transform.position, transform.rotation);
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

        if (!thrusting)
        {
            thrustOrientationY = 1;
        }

        if(rbBossSaltos.velocity.y == 0)
        {
            thrustOrientationY *= -1;
        }

        if (rbBossSaltos.velocity.x == 0)
        {
            thrustOrientationX *= -1;
        }
    }

    private void FixedUpdate()
    {
        if (thrusting)
        {
            colliderB.enabled = true;
            colliderA.enabled = false;
            animator.SetBool("bouncing", true);
            rbBossSaltos.velocity = new Vector2(bossSaltosSpeed[phase] * Time.deltaTime * thrustOrientationX, bossSaltosSpeed[phase] * Time.deltaTime * thrustOrientationY);
            dropBombs = true;
        }
        else
        {
            colliderB.enabled = false;
            colliderA.enabled = true;
            animator.SetBool("bouncing", false);
            if (!pushed)
            {
                rbBossSaltos.velocity = new Vector2(walkingSpeed[phase] * Time.deltaTime * thrustOrientationX, rbBossSaltos.velocity.y);
            }
            if(granadeCadenceFake <= 0)
            {
                Instantiate(granade, spawn.transform.position, transform.rotation);
                granadeCadenceFake = granadeCadence;
            }

            granadeCadenceFake -= Time.deltaTime;
            walkingTimeTimer -= Time.deltaTime;

            dropBombs = false;
        }

        if (!thrusting && transform.localPosition.y < -8.711056)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, -8.711056f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "impactWallX")
        {
            SoundController.Instance.PlaySounds(bounce);
            thrustOrientationX *= -1;
            if (thrusting)
            {
                jumpsLeft--;
            }
        }

        if (collision.gameObject.tag == "impactWallY")
        {
            SoundController.Instance.PlaySounds(bounce);
            thrustOrientationY *= -1;
            if (thrusting && thrustOrientationY == -1)
            {
                jumpsLeft--;
            }
        }

        if (collision.gameObject.tag == "PlayerSword" && thrusting && !changingDirection)
        {
            StartCoroutine(ChangingDirectionBool());
            TakeDamage(3);
            thrustOrientationX *= -1;
        }

        if(collision.gameObject.tag == "PlayerSword" && !thrusting)
        {
            StartCoroutine(pushedBool());
            rbBossSaltos.AddForce(Vector2.right * -facingPlayer * pushForce, ForceMode2D.Impulse);
            TakeDamage(3);
        }

        if (collision.gameObject.tag == "Bullet" && !beingHit)
        {
            StartCoroutine(BeingHit());
            TakeDamage(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && thrusting && !changingDirection)
        {
            StartCoroutine(ChangingDirectionBool());
            thrustOrientationX *= -1;
            thrustOrientationY *= -1;
            movimientoPlayer.PlayerLife(1);
        }
    }

    void TakeDamage(int damage)
    {
        SoundController.Instance.PlaySounds(hurt);
        health -= damage;
        StartCoroutine(animateHurt());
        
        //take damage feedback
    }

    IEnumerator pushedBool()
    {
        pushed = true;
        yield return new WaitForSeconds(.75f);
        pushed = false;
    }

    IEnumerator ChangingDirectionBool()
    {
        changingDirection = true;
        yield return new WaitForSeconds(.2f);
        changingDirection = false;
    }

    IEnumerator animateHurt()
    {
        animator.SetBool("hurt", true);
        colliderB.enabled = false;
        colliderA.enabled = true;
        yield return new WaitForSeconds(.2f);
        colliderB.enabled = false;
        colliderA.enabled = true;
        animator.SetBool("hurt", false);
    }

    IEnumerator BeingHit() 
    {
        beingHit = true;
        yield return new WaitForSeconds(.5f);
        beingHit = false;
    }

    IEnumerator Die()
    {
        bossSaltosSpeed[phase] = 0;
        thrusting = false;
        SoundController.Instance.PlaySounds(die);
        yield return new WaitForSeconds(7f);
        animator.SetBool("died", true);
        SoundController.Instance.PlaySounds(explosion);
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
        SceneManager.LoadScene("winScene");
    }
}
