using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 10f;
    private Rigidbody2D rb;
    private bool enSuelo;
    private bool quiereSaltar;
    public Transform puntoSuelo;
    public float radioSuelo;
    public LayerMask capaSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Comprobar la entrada para el salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            quiereSaltar = true;
        }
    }

    void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapCircle(puntoSuelo.position, radioSuelo, capaSuelo);

        float movimiento = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movimiento * velocidadMovimiento, rb.velocity.y);

        if (quiereSaltar && enSuelo)
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
            quiereSaltar = false; // Resetear el flag después de saltar
        }
    }
}
