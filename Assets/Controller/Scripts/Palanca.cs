using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Sprite newImage;
    public Canvas canvasToToggle;

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
        }
    }
}