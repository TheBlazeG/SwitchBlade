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

    [Header("Life")]
    public float MaxLife = 0;
    public float life = 0;
    private bool DeathPlayer;
    

    [Header("DisparoJugador")]
    public Transform constroladorbala;
    public float cooldown;
    public GameObject Flecha;
    public float timelife;
    
    public GameObject Boom;
    public float limitChicken = 5;
    private bool CanShoot = true;
    public bool Chicken;
    public float ChickenMore;

    [Header("PlayerUI")]
    public PlayerUI playerUI;

    [Header("Attack and Shoot")]
    public AudioSource swordAudioSource;  
    public AudioSource shootAudioSource;
    

    private void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DeathPlayer = false;
        ChickenMore = 0;
        life = MaxLife;
        playerUI.SetMaxHealth(MaxLife);
        playerUI.SetMaxAmmo(limitChicken);
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

        if(!agachar)
        {
            
            if(Input.GetButtonDown("Fire1"))
            {
                swordAudioSource.Play();
                StartCoroutine(attack());               
            }
            
            if (Input.GetButtonDown("Fire2") && CanShoot)
            {
                StartCoroutine(Bullet());
                StartCoroutine(CooldownCoroutine(cooldown));
            }

            if(Input.GetKey("g") && CanShoot && ChickenMore > 0)
            {
                StartCoroutine(PETime());
                ChickenMore -= 1;
                playerUI.SetAmmo(-1);
                StartCoroutine(CooldownCoroutine(cooldown));
            }
        }

        if (enSuelo)
        {
            saltosExtraRestantes = saltosExtra;
            Debug.Log(enSuelo);
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

        if (!DeathPlayer)
        {
            Mover(movimientoHorizontal * Time.fixedDeltaTime, salto, agachar);
        }
       
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

        if (enSuelo && salto && !deslizando && !agachar)
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

    public void Dash()
    {
        saltosExtraRestantes -= 1;
        StartCoroutine(dashplayer());
        dash = false;
        RB2D.AddForce(new Vector2(Dashforce, 0f));
    }

    public void NDash()
    {
        StartCoroutine(dashDamage());
        RB2D.AddForce(new Vector2(Dashforce * -1, 200f));
    }   

    private void SaltoPared()
    {
        salto = false;
        enPared = false;
        RB2D.velocity = new Vector2(0, 0);
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
        playerUI.SetHealth(Damage);
        if (life <= 0)
        {
            NDash();
            DeathPlayer = true;
            animator.SetBool("Death", true); 
            StartCoroutine(Muerte());
        }
        else
        {
            NDash();
        }
    }

    public void PlayerCures(float Health)
    {
        if ((life + Health) > MaxLife)
        {
            life = MaxLife;
            playerUI.SetHealth(-MaxLife);
        } 
        else 
        {
            life += Health;
            playerUI.SetHealth(-Health);
        }
    }

    IEnumerator attack()
    {
        animator.SetBool("Ataque", true);
        yield return new WaitForSeconds(.0f);
        animator.SetBool("Ataque", false);
       
    }

    IEnumerator dashplayer()
    {
        animator.SetBool("Dash", true);
        yield return new WaitForSeconds(.0f);
        animator.SetBool("Dash", false);
    }

    IEnumerator dashDamage()
    {
        animator.SetBool("Hurt", true);
        yield return new WaitForSeconds(.4f);
        animator.SetBool("Hurt", false);
    }

    IEnumerator Muerte()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive (false);
    }

    public IEnumerator Bullet()
    {
        animator.SetBool("Bullet", true);
        yield return new WaitForSeconds(.0f);
        Disparo();
        animator.SetBool("Bullet", false);

    }
    
    public IEnumerator PETime()
    {
        animator.SetBool("PE", true);
        yield return new WaitForSeconds(.0f);
        BoomChicken();
        animator.SetBool("PE", false);
    }

    private void Disparo()
    {
        Instantiate(Flecha, constroladorbala.position, constroladorbala.rotation);
        shootAudioSource.Play();
    }

    public void Bomba(float MunChicken)
    {
        ChickenMore += MunChicken;
        playerUI.SetAmmo(MunChicken);
    }

    public void BoomChicken()
    {
        Instantiate(Boom, constroladorbala.position, constroladorbala.rotation);
    }

    IEnumerator CooldownCoroutine(float _time)
    {
        CanShoot = false;
        yield return new WaitForSeconds(_time);
        CanShoot = true;
    }
    
    IEnumerator Pollo()
    {
        Chicken = false;
        yield return new WaitForSeconds(2f);
        Chicken = true;
    }
}
