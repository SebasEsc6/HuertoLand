using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direccion;
    private Animator anim;
    private CinemachineVirtualCamera cam;
    private Vector2 direccionMovimiento;


    [Header("Estadisticas")]
    public float velocidadDeMovimiento = 10;
    public float fuerzaDeSalto = 5;
    public float velocidadRodar = 20;
    public float velocidadDeslizar;

    [Header("Booleans")]
    public bool poderMoverse = true;
    public bool enSuelo = true;
    public bool puedeRodar;
    public bool haciendoDash;
    public bool pisoTocado;
    public bool haciendoShake;
    public bool estaAtacando;
    public bool enMuro;
    public bool muroDerecho;
    public bool muroIzquierda;
    public bool agarrarse;
    public bool saltarDeMuro;
   

    [Header("Coliciones")]
    public LayerMask layerPiso;
    public Vector2 abajo, izquierda, derecha;
    public float radioDeColision;

    private void Awake()
    {
         rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        Agarres();
    }

    private void Atacar(Vector2 direccion)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!estaAtacando && !haciendoDash)
            {
                estaAtacando= true;
                anim.SetFloat("AtaqueX", direccion.x);
                anim.SetFloat("AtaqueY", direccion.y);

                anim.SetBool("Atacar", true);
            }
        }
    }

    public void FinalizarAtaque()
    {
        anim.SetBool("Atacar", false);
        estaAtacando = false;
    }

    private Vector2 DireccionAtaque(Vector2 direccionMovimeinto, Vector2 direccion)
    {
        if (rb.velocity.x == 0 && direccion.y !=0)
            return new Vector2(0,direccion.y);

        return new Vector2(direccionMovimeinto.x,direccion.y);
    }

    private IEnumerator AgitarCamara()
    {
        haciendoShake = true;
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 5;
        yield return new WaitForSeconds(0.3f);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        haciendoShake = false;
    }    
    private IEnumerator AgitarCamara(float tiempo)
    {
        haciendoShake = true;
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 5;
        yield return new WaitForSeconds(tiempo);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        haciendoShake = false;
    }





    private void Rodar(float x, float y)
    {
        anim.SetBool("Rodar", true);
        Vector3 PosicionJugador = Camera.main.WorldToViewportPoint(transform.position);
       
        StartCoroutine(AgitarCamara());
        
        puedeRodar = true;
        rb.velocity = Vector2.zero;
        rb.velocity += new Vector2(x, y).normalized * velocidadRodar;
        StartCoroutine(PrepararRodar());
    }
    
    private IEnumerator PrepararRodar()
    {
        StartCoroutine(rodarSuelo());
        rb.gravityScale = 0;
        haciendoDash = true;

        yield return new WaitForSeconds(0.3f);

        rb.gravityScale = 3;
        haciendoDash = false;
        FinalizarRodar();    
    }

    private IEnumerator rodarSuelo()
    {
        yield return new WaitForSeconds(0.15f);
        if (enSuelo)
            puedeRodar = false;
    }

    public void FinalizarRodar()
    {
        anim.SetBool("Rodar",false);
    }

    public void TocarPiso()
    {
        puedeRodar = false;
        haciendoDash = false;
        anim.SetBool("Saltar", false);
    }    

    private void Movimiento()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");


        direccion = new Vector2 (x, y);
        Vector2 direccionRaw = new Vector2(xRaw, yRaw);

        Caminar();
        Atacar(DireccionAtaque(direccionMovimiento, direccionRaw));

        if(enSuelo && !haciendoDash)
        {
            saltarDeMuro = false;
        }

        agarrarse = enMuro && Input.GetKey(KeyCode.LeftShift);

        if(enMuro)
        {
            anim.SetBool("Escalar", true);
            if (rb.velocity == Vector2.zero)
            {
                anim.SetFloat("Velocidad", 0);
            }
            else
            {
                anim.SetFloat("Velocidad", 1);
            }
        }else
        {
            anim.SetBool("Escalar", false);
            anim.SetFloat("Velocidad", 0);
        }

        if(agarrarse && !haciendoDash)
        {
            rb.gravityScale = 0;
            if (x > 0.2f || x < -0.2f)
                rb.velocity = new Vector2(rb.velocity.x, 0);

            float modificadorVelocidad = y > 0 ? 0.5f : 1;
            rb.velocity = new Vector2(rb.velocity.x, y * (velocidadDeMovimiento * modificadorVelocidad));

            if(muroIzquierda && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if(muroDerecho && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            rb.gravityScale = 3;
        }
        if(enMuro && !enSuelo)
        {
            if(x != 0 && !agarrarse)
            {
                DeslizarPared();
            }
        }
        
                        
        MejorarSalto();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(enSuelo)
            {
                anim.SetBool("Saltar", true);
                Saltar();
            }
            if(enMuro & !enSuelo)
            {
                anim.SetBool("Escalar", false);
                anim.SetBool("Saltar", true);
                SaltarDesdeMuro();
            }
           
        }
        if(Input.GetKeyDown(KeyCode.X) && !haciendoDash)
        {
            if(xRaw != 0 || yRaw != 0)
                Rodar(xRaw, yRaw);
        }

        if(enSuelo && !pisoTocado)
        {
            TocarPiso();
            pisoTocado = true;
        }
        if (!enSuelo && pisoTocado)
            pisoTocado = false;

        float velocidad;
        if (rb.velocity.y > 0)
            velocidad = 1;

        else 
            velocidad = -1;

        if (!enSuelo)
        {

            anim.SetFloat("VelocidadVertical", velocidad);
        }
        else
        {
            if(velocidad == -1)
                FinalizarSalto();   
        }

    }


    public void DeslizarPared()
    {
        if (poderMoverse)

            rb.velocity = new Vector2(rb.velocity.x, -velocidadDeslizar); 
    }

    private void SaltarDesdeMuro()
    {
        StopCoroutine(DeshabilitarMovimiento(0));
        StartCoroutine(DeshabilitarMovimiento(0.1f));

        Vector2 direccionMuro= muroDerecho ? Vector2.left : Vector2.right;
        if(direccion.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }else if(direccion.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        anim.SetBool("Saltar", true);
        anim.SetBool("Escalar", false);

        Saltar((Vector2.up + direccionMuro), true);

        saltarDeMuro = true;
    }

    private IEnumerator DeshabilitarMovimiento(float tiempo)
    {
        poderMoverse = false;
        yield return new WaitForSeconds(tiempo);
        poderMoverse = true;
    }


    public void FinalizarSalto()
    {
        anim.SetBool("Saltar",false);
      

    }

    private void Saltar()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * fuerzaDeSalto;

    }
    private void Saltar(Vector2 direccionSalto, bool muro)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += direccionSalto * fuerzaDeSalto;
    }

    private void MejorarSalto()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (2.5f - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (2.0f - 1) * Time.deltaTime;
        }

    }

    private void Agarres()
    {
        enSuelo = Physics2D.OverlapCircle((Vector2)transform.position + abajo, radioDeColision, layerPiso );

        muroDerecho = Physics2D.OverlapCircle((Vector2)transform.position + derecha, radioDeColision, layerPiso);
        muroIzquierda = Physics2D.OverlapCircle((Vector2)transform.position + izquierda, radioDeColision, layerPiso);

        enMuro = muroDerecho || muroIzquierda;
            

    }

    private void Caminar()
    {
        if (poderMoverse && !haciendoDash)
        {
            if (saltarDeMuro)
            {
                rb.velocity = Vector2.Lerp(rb.velocity,
                    (new Vector2(direccion.x * velocidadDeMovimiento, rb.velocity.y)), Time.deltaTime / 2 );
            }
            else
            {
                if (direccion != Vector2.zero && !agarrarse)
                {
                    if (!enSuelo)
                    {
                        anim.SetBool("Saltar", true);
                    }
                    else
                    {
                        anim.SetBool("Caminar", true);
                    }

                    rb.velocity = (new Vector2(direccion.x * velocidadDeMovimiento, rb.velocity.y));
                    if (direccion.x < 0 && transform.localScale.x > 0)
                    {
                        direccionMovimiento = DireccionAtaque(Vector2.left, direccion);
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    }
                    else if (direccion.x > 0 && transform.localScale.x < 0)
                    {
                        direccionMovimiento = DireccionAtaque(Vector2.right, direccion);
                        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    }
                }
                else
                {
                    if (direccion.y > 0 && direccion.x == 0)
                    {
                        direccionMovimiento = DireccionAtaque(direccion, Vector2.up);
                    }
                    anim.SetBool("Caminar", false);
                }
            }
        }
         
           

    }
   
}

