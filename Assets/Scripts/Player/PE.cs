using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PE : MonoBehaviour
{
    public float velocidad;
    public int Explotion;
    public float lifetime;
    private Animator animator;
    private bool Run = true;
    public AudioSource Chicken;
    public AudioClip ChickenFly;
    public AudioClip ChickenDeathBoom;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Chicken.PlayOneShot(ChickenFly);
    }

    private void Update()
    {   
        if (Run)
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        }
        StartCoroutine(LifetimeCoroutine(lifetime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemigoBeta"))
        {   
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        Chicken.PlayOneShot(ChickenDeathBoom);
        Run = false;
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(.8f);
        Destroy(gameObject);
    }

    IEnumerator LifetimeCoroutine(float _time)
    {
        yield return new WaitForSeconds(_time);
        StartCoroutine(Death());
    }
}
