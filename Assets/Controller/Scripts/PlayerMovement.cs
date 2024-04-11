using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool activar_inv;

    void Update()
    {
        // Movimiento horizontal
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Activar animación de correr
        animator.SetBool("Caminar", moveInput != 0);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Semilla"))
        {
            Debug.Log("Choque con la semilla");
            for (int i = 0; i < Bag.Count; i++)
            {
                if (Bag[i].GetComponent<Image>().enabled == false)
                {
                    Bag[i].GetComponent<Image>().enabled = true;
                    break;
                }
            }
        }
    }
}

