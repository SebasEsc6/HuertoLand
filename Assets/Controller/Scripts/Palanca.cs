using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    public Sprite newImage;
    public Canvas canvasToToggle;
    public PlayerMovement playerMovement; // Referencia al script que controla el movimiento del jugador

    private bool canvasVisible = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (spriteRenderer != null && newImage != null)
            {
                spriteRenderer.sprite = newImage;
            }

            // Mostrar u ocultar el canvas
            if (canvasToToggle != null)
            {
                canvasVisible = !canvasVisible;
                canvasToToggle.gameObject.SetActive(canvasVisible);
            }

            // Iniciar la corrutina para deshabilitar el movimiento del jugador por 3 segundos
            if (playerMovement != null)
            {
                StartCoroutine(DisableMovementTemporarily());
            }
        }
    }

    private IEnumerator DisableMovementTemporarily()
    {
        playerMovement.enabled = false; // Deshabilitar el movimiento
        yield return new WaitForSeconds(3); // Esperar 3 segundos
        playerMovement.enabled = true; // Habilitar el movimiento nuevamente
    }
}