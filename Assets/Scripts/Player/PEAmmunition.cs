using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEAmmunition : MonoBehaviour
{
    private Animator animator;
    private float Carga = 1;

    // Update is called once per frame
    private void Update()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Hit");
            GetComponent<BoxCollider2D>().enabled = false;
            other.GetComponent<MovimientoPlayer>().Bomba(Carga);
            StartCoroutine(IdleChicken());
            
        }
    }

    IEnumerator IdleChicken()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
