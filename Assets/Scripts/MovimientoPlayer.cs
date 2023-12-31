using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    private Rigidbody2D RB2D;

    [Header("Animacion")]
    private Animator animator;

    [Header("Movimiento")]
    private float inputX;
    private float movimientoHorizontal = 0f;
    public float velocidadDeMovimiento;
    [Range(0,0.5f)] public float suavizadoDeMovimiento;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;
    public Vector2 input;

    [Header("Salto")]
    public float fuerzaDeSalto;
    public LayerMask queEsSuelo;
    public Transform controladorSuelo;
    public Vector3 dimensionesCaja;
    public bool enSuelo;
    private bool salto = false; 

    [Header("DobleSalto")]
    public int saltosExtraRestantes;
    public int saltosExtra;
    public float Dashforce;
    public bool dash = false;

    [Header("SaltoPared")]
    public Transform controladorPared;
    public Vector3 dimensionesCajaPared;
    private bool enPared;  
    private bool deslizando;
    public float velocidadDeslizar;
    public float fuerzaSaltoParedX;
    public float fuerzaSaltoParedY;
    public float tiempoSaltoPared;
    private bool saltandoDePared;

    [Header("Agachar")]
    public Transform controladorTecho;
    public float radioTecho;
    public float multiplicadorVelocidadAgachado;
    public Collider2D colisionadorAgachado;
    private bool estabaAgachado = false;
    private bool agachar = false;

    [Header("Attack")]
    public AudioSource swordAudioSource;
    private bool Attack = false;

    [Header("Life")]
    public float life;

    private void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        movimientoHorizontal = inputX * velocidadDeMovimiento;
        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));
        input.y = Input.GetAxisRaw("Vertical");

        animator.SetBool("enPared", deslizando);
        

        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }

        if(saltosExtraRestantes > 0 && Input.GetButtonDown("Dash"))
        {
            dash = true;
            Dash();
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("pp");
            swordAudioSource.Play();
            StartCoroutine(attack());
        }

        if (enSuelo)
        {
            saltosExtraRestantes = saltosExtra;
        }

        if (!enSuelo && enPared && inputX != 0)
        {
            deslizando = true;
        }
        else 
        {
            deslizando = false;
        }

    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        animator.SetBool("enSuelo", enSuelo);
        animator.SetBool("Agachar", estabaAgachado);
        
        enPared = Physics2D.OverlapBox(controladorPared.position, dimensionesCajaPared, 0f, queEsSuelo);

        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto, agachar);


        if(deslizando)
        {
            RB2D.velocity = new Vector2(RB2D.velocity.x, Mathf.Clamp(RB2D.velocity.y, -velocidadDeslizar, float.MaxValue));
        }

        if(input.y < 0)
        {
            agachar = true;
        }
        else
        {
            agachar = false;
        }
    }

    private void Mover(float mover, bool salto, bool agachar)
    {
        if (!agachar)
        {
            if(Physics2D.OverlapCircle(controladorTecho.position, radioTecho, queEsSuelo))
            {
                agachar = true;
            }
        }

        if (agachar)
        {
            if(!estabaAgachado)
            {
                estabaAgachado = true;
            }

            mover *= multiplicadorVelocidadAgachado;

            colisionadorAgachado.enabled = false;
        }
        else
        {
            colisionadorAgachado.enabled = true;
            if (estabaAgachado)
            {
                estabaAgachado = false;
            }
        }

        if(!saltandoDePared)
        {
            Vector3 velocidadObjetivo = new Vector2(mover, RB2D.velocity.y);
            RB2D.velocity = Vector3.SmoothDamp(RB2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);
        }

        if(mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if(mover < 0 && mirandoDerecha)
        {
            Girar();
        }

        if (enSuelo && salto && !deslizando)
        {
            Salto();
        }

        if (salto && enPared && deslizando)
        {
            SaltoPared();
        }
    }

    private void Salto()
    {
        salto = false;
        RB2D.AddForce(new Vector2(0f, fuerzaDeSalto));

    }

    private void Dash()
    {
        saltosExtraRestantes -= 1;
        animator.Play("Dash");
        dash = false;
        RB2D.AddForce(new Vector2(Dashforce, 0f));

    }

    private void SaltoPared()
    {
        salto = false;
        enPared = false;
        RB2D.AddForce(new Vector2(fuerzaSaltoParedX * -inputX, fuerzaSaltoParedY));
        // Espere
        StartCoroutine(CambioSaltoPared());
    }

    IEnumerator CambioSaltoPared()
    {
        saltandoDePared = true;
        yield return new WaitForSeconds(tiempoSaltoPared);
        saltandoDePared = false;
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Dashforce*=-1;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);

        Gizmos.DrawWireCube(controladorPared.position, dimensionesCajaPared);

        Gizmos.DrawWireSphere(controladorTecho.position, radioTecho);
    }

    public void PlayerLife(float Damage)
    {
        life -= Damage;
        if (life <= 0)
        {
            Murision();
        }
    }

    public void Murision()
    {
        gameObject.SetActive(false);
    }
    IEnumerator attack()
    {
        animator.SetBool("Ataque", true);
        yield return new WaitForSeconds(.0f) ;
        animator.SetBool("Ataque", false);
       
    }
}
