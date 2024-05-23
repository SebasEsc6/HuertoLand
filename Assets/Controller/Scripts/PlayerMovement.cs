using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 7.0f;
    public Animator animator;
    public Rigidbody2D rb;

    private float moveInput;
    private bool isGrounded;

    public List<GameObject> Bag = new List<GameObject>();
    public GameObject inv;
    public bool activarInv;

    private void Update()
    {
        // Movimiento horizontal
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Activar animación de correr
        animator.SetBool("Caminar", moveInput != 0);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }

        // Actualizar el parámetro IsGrounded en el Animator
        animator.SetBool("IsGrounded", isGrounded);

        // Girar el personaje de acuerdo a la dirección
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered with: " + collision.gameObject.tag);  // Debug message

        if (collision.CompareTag("SorpresaEnfermo"))
        {
            Debug.Log("Enfermo trigger detected");  // Debug message
            // Activar animación de enfermo
            animator.SetBool("isEnfermo", true);
            StartCoroutine(HandleEnfermo());
        }
        else if (collision.CompareTag("SorpresaSaludable"))
        {
            Debug.Log("Saludable trigger detected");  // Debug message
            // Activar animación de saludable
            animator.SetBool("isSaludable", true);
            StartCoroutine(HandleSaludable());
        }
    }

    private IEnumerator HandleEnfermo()
    {
        float originalSpeed = speed;
        speed = originalSpeed / 2;  // Reducir la velocidad a la mitad
        yield return new WaitForSeconds(5);  // Duración del efecto
        speed = originalSpeed;
        animator.SetBool("isEnfermo", false);
    }

    private IEnumerator HandleSaludable()
    {
        float originalSpeed = speed;
        float originalJumpForce = jumpForce;
        speed = originalSpeed * 1.5f;  // Aumentar la velocidad
        jumpForce = originalJumpForce * 1.5f;  // Aumentar la fuerza de salto
        yield return new WaitForSeconds(5);  // Duración del efecto
        speed = originalSpeed;
        jumpForce = originalJumpForce;
        animator.SetBool("isSaludable", false);
    }
}



