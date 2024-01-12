using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEAmmunition : MonoBehaviour
{
    private float Chicken = 1;
    private Animator animator;

    // Update is called once per frame
    private void Update()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(IdleChicken());
            other.GetComponent<DisparoJugador>().Bomba(Chicken);
        }
    }

    IEnumerator IdleChicken()
    {
        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
